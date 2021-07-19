using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController<TEntity, TDtoShort, TDtoExtended> : ControllerBase 
        where TEntity : IdentifiableEntity 
        where TDtoShort : class, new()
        where TDtoExtended : class, new()
    {
        private readonly ApplicationDbContext _dbContext;
        protected readonly IEntityMapperService _mapper;
        protected readonly IFilterService _filter;

        public BaseController(ApplicationDbContext dbContext, IEntityMapperService mapper, IFilterService filter)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _filter = filter;
        }

        protected DbSet<TEntity> EntitySet { get { return this._dbContext.Set<TEntity>(); } }

        protected virtual IQueryable<TEntity> IncludeRelationships() {
            return this.EntitySet;
        }

        [HttpGet]
        public async Task<IEnumerable<TDtoShort>> Get(string filter)
        {
            var filterOptions = this.CreateFilterObject(filter);
            return this._mapper.MapList<TDtoShort>(await this.EntitySet.Where(this._filter.GetFilter<TEntity>(filterOptions)).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<TDtoExtended> Get(int id)
        {
            return this._mapper.Map<TDtoExtended>(await this.IncludeRelationships().SingleAsync(entity => entity.Id == id));
        }

        protected Filter CreateFilterObject(string filterString)
        {
            var filter = new Filter();

            if (String.IsNullOrEmpty(filterString))
            {
                return filter;
            }

            foreach (var propertyFilter in filterString.Split(","))
            {
                string[] filterOptions = propertyFilter.Split(" ");
                if (filterOptions.Length < 3)
                {
                    throw new Exception("Bad filter.");
                }

                var property = filterOptions[0];
                var type = filterOptions[1] switch
                {
                    "EqualsTo" => FilterType.EqualsTo,
                    "LowerThan" => FilterType.LowerThan,
                    "GreaterThan" => FilterType.GreaterThan,
                    "Like" => FilterType.Like,
                    _ => throw new Exception("Bad filter.")
                };
                var value = filterOptions[2];

                filter.AddFilter(property, type, value);
            }

            return filter;
        }
    }
}

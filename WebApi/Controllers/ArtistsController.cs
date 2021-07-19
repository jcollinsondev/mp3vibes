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
    public class ArtistsController : BaseController<Artist, ArtistDtoShort, ArtistDtoExtended>
    {
        public ArtistsController(ApplicationDbContext dbContext, IEntityMapperService mapper, IFilterService filter)
            : base(dbContext, mapper, filter)
        {
        }

        protected override IQueryable<Artist> IncludeRelationships() {
            return this.EntitySet.Include(artist => artist.Albums);
        }
    }
}

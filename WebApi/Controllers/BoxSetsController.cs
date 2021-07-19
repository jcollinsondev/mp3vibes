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
    public class BoxSetsController : BaseController<BoxSet, BoxSetDtoShort, BoxSetDtoExtended>
    {
        public BoxSetsController(ApplicationDbContext dbContext, IEntityMapperService mapper, IFilterService filter)
            : base(dbContext, mapper, filter)
        {
        }

        protected override IQueryable<BoxSet> IncludeRelationships() {
            return this.EntitySet
                .Include(boxSet => boxSet.BoxSetsAlbums)
                .ThenInclude(boxSetAlbum => boxSetAlbum.Album);
        }
    }
}

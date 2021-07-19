using BatMap;
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
    public class AlbumsController : BaseController<Album, AlbumDtoShort, AlbumDtoExtended>
    {
        public AlbumsController(ApplicationDbContext dbContext, IEntityMapperService mapper, IFilterService filter)
            : base(dbContext, mapper, filter)
        {
        }

        protected override IQueryable<Album> IncludeRelationships() {
            return this.EntitySet
                .Include(album => album.Songs)
                .Include(album => album.BoxSetsAlbums)
                .ThenInclude(boxSetAlbum => boxSetAlbum.BoxSet);
        }
    }
}

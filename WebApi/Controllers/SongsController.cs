using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    public class SongsController : BaseController<Song, SongDtoShort, SongDtoExtended>
    {
        public SongsController(ApplicationDbContext dbContext, IEntityMapperService mapper, IFilterService filter)
            : base(dbContext, mapper, filter)
        {
        }

        /*[HttpGet("filter/{filter}")]
        public async Task<IEnumerable<SongDtoShort>> GetSongs(int id)
        {
            return this._mapper.MapList<SongDtoShort>(
                (await this.EntitySet
                .Include(album => album.Songs)
                .SingleAsync(album => album.Id == id))
                .Songs
            );
        }*/

        [HttpGet("{id}/download")]
        public async Task<HttpResponseMessage> GetFile(int id)
        {
            var song = await this.EntitySet.Include(song => song.File).SingleAsync(song => song.Id == id);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new MemoryStream(song.File.Audio));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = song.Title;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return response;
        }
    }
}

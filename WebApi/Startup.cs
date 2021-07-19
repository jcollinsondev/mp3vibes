using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatMap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Database;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddCors(options => options.AddDefaultPolicy(builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));
			
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration["Sqlite:ConnectionString"]));

            // Entity mapper
            services.AddSingleton<IEntityMapperService, EntityMapperService>(options =>
            {
                var mapper = new MapConfiguration();

                mapper.RegisterMap<Genre, GenreDtoShort>();
                mapper.RegisterMap<Genre, GenreDtoExtended>();
                mapper.RegisterMap<Artist, ArtistDtoShort>();
                mapper.RegisterMap<Artist, ArtistDtoExtended>();
                mapper.RegisterMap<BoxSet, BoxSetDtoShort>();
                mapper.RegisterMap<BoxSet, BoxSetDtoExtended>(
                    builder => builder
                    .MapMember<List<AlbumDtoShort>>(
                        dto => dto.Albums, (entity, mc) => entity.BoxSetsAlbums.Select(boxSetAlbum => new AlbumDtoShort { 
                            Id = boxSetAlbum.Album.Id,
                            Title = boxSetAlbum.Album.Title,
                            Price = boxSetAlbum.Album.Price,
                            Cover = String.Format("{0}/{1}.jpg", Configuration["Paths:Covers"], boxSetAlbum.Album.Id)
                        }).ToList()
                    )    
                );
                mapper.RegisterMap<Album, AlbumDtoShort>(
                    builder => builder
                    .MapMember(
                        dto => dto.Cover, (entity, mc) => String.Format("{0}/{1}.jpg", Configuration["Paths:Covers"], entity.Id)
                    )
                );
                mapper.RegisterMap<Album, AlbumDtoExtended>(
                    builder => builder
                    .MapMember(
                        dto => dto.Cover, (entity, mc) => String.Format("{0}/{1}.jpg", Configuration["Paths:Covers"], entity.Id)
                    )
                );
                mapper.RegisterMap<Song, SongDtoShort>();
                mapper.RegisterMap<Song, SongDtoExtended>();

                return new EntityMapperService(mapper);
            });

            services.AddScoped<IFilterService, FilterService>();

            // Register the Swagger generator
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Music API V1"));

            app.UseHttpsRedirection();

            app.UseRouting();
			
			app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

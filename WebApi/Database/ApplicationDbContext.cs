using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<BoxSet> BoxSets { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<BoxSetAlbum> BoxSetsAlbums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<File> Files { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Genre>()
                .HasMany(genre => genre.Albums)
                .WithOne(album => album.Genre);

            builder.Entity<Artist>()
                .HasMany(artist => artist.Albums)
                .WithOne(album => album.Artist);

            builder.Entity<Album>()
                .HasMany(album => album.Songs)
                .WithOne(song => song.Album);

            builder.Entity<BoxSetAlbum>()
                .HasKey(boxSetAlbum => new {
                    boxSetAlbum.BoxSetId,
                    boxSetAlbum.AlbumId
                });

            builder.Entity<BoxSetAlbum>()
                .HasOne(boxSetAlbum => boxSetAlbum.BoxSet)
                .WithMany(boxSet => boxSet.BoxSetsAlbums)
                .HasForeignKey(boxSetAlbum => boxSetAlbum.BoxSetId);

            builder.Entity<BoxSetAlbum>()
                .HasOne(boxSetAlbum => boxSetAlbum.Album)
                .WithMany(album => album.BoxSetsAlbums)
                .HasForeignKey(boxSetAlbum => boxSetAlbum.AlbumId);

            builder.Entity<Song>()
                .HasOne(song => song.File);
        }
    }
}

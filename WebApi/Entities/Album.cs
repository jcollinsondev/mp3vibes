using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Entities
{
    public class Album : IdentifiableEntity
    {
        [Filter(AllowedFilters = FilterType.EqualsTo | FilterType.Like)]
        public string Title { get; set; }

        [Filter(AllowedFilters = FilterType.EqualsTo | FilterType.GreaterThan | FilterType.LowerThan)]
        public double Price { get; set; }

        public List<BoxSetAlbum> BoxSetsAlbums { get; set; }

        public List<Song> Songs { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}

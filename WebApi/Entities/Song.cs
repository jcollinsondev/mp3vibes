using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Entities
{
    public class Song : IdentifiableEntity
    {
        [Filter(AllowedFilters = FilterType.EqualsTo | FilterType.Like)]
        public string Title { get; set; }

        [Filter(AllowedFilters = FilterType.EqualsTo)]
        public string Format { get; set; }

        [Filter(AllowedFilters = FilterType.EqualsTo | FilterType.GreaterThan | FilterType.LowerThan)]
        public int Duration { get; set; }

        [Filter(AllowedFilters = FilterType.EqualsTo | FilterType.GreaterThan | FilterType.LowerThan)]
        public double Price { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int FileId { get; set; }
        public File File { get; set; }
    }
}

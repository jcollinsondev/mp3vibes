using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Entities
{
    public class BoxSet : IdentifiableEntity
    {
        [Filter(AllowedFilters = FilterType.EqualsTo | FilterType.Like)]
        public string Title { get; set; }

        [Filter(AllowedFilters = FilterType.EqualsTo | FilterType.GreaterThan | FilterType.LowerThan)]
        public double Price { get; set; }

        public List<BoxSetAlbum> BoxSetsAlbums { get; set; }
    }
}

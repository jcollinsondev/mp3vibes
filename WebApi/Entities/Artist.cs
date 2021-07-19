using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Entities
{
    public class Artist : IdentifiableEntity
    {
        [Filter(AllowedFilters = FilterType.EqualsTo | FilterType.Like)]
        public string Name { get; set; }

        public List<Album> Albums { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class GenreDtoShort
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GenreDtoExtended
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<AlbumDtoShort> Albums { get; set; }
    }
}

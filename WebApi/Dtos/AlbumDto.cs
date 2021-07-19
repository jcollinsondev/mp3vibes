using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class AlbumDtoShort
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Cover { get; set; }
    }

    public class AlbumDtoExtended
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Cover { get; set; }

        public List<SongDtoShort> Songs { get; set; } 
    }
}

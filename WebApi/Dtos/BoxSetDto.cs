using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class BoxSetDtoShort
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
    }

    public class BoxSetDtoExtended
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }

        public List<AlbumDtoShort> Albums { get; set; }
    }
}

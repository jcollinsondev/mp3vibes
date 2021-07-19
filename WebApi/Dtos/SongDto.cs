using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class SongDtoShort
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
    }

    public class SongDtoExtended
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Format { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class BoxSetAlbum
    {
        public int BoxSetId { get; set; }
        public BoxSet BoxSet { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}

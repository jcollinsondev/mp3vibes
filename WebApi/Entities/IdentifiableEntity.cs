using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class IdentifiableEntity
    {
        [Key]
        public int Id { get; set; }
    }
}

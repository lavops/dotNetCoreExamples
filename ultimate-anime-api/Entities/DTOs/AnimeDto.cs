using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class AnimeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Episodes { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}

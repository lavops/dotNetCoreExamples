using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Studio
    {
        [Column("StudioId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Studio name is required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Addres is 50 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Country is 20 characters.")]
        public string Country { get; set; }

        public ICollection<Anime> Animes { get; set; }
    }
}

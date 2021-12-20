using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public abstract class StudioForManipulationDto
    {
        [Required(ErrorMessage = "Studio name is required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Studio address is required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Address is 30 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Country is 30 characters.")]
        public string Country { get; set; }
        public IEnumerable<AnimeForCreationDto> Animes { get; set; }
    }
}

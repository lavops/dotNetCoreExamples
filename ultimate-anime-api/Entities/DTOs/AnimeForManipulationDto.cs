using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public abstract class AnimeForManipulationDto
    {
        [Required(ErrorMessage = "Anime name is required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Anime episodes is requried and can't be lower than 0")]
        public int Episodes { get; set; }

        [Required(ErrorMessage = "Release date is required field.")]
        public DateTime ReleaseDate { get; set; }
    }
}

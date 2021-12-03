using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Anime
    {
        [Column("AnimeId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Anime name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Number of episodes is required field.")]
        public int Episodes { get; set; }

        [Required(ErrorMessage = "ReleaseDate is a required field.")]
        public DateTime ReleaseDate { get; set; }

        [ForeignKey(nameof(Studio))]
        public Guid StudioId { get; set; }
        public Studio Studio { get; set; }
    }
}

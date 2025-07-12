using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC4Fun.Models
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Movie ID")]
        public int MovieID { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [Display(Name = "Film")]
        public string? Film { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1950, 2025, ErrorMessage = "Year must be between 1950 and 2025")]
        [Display(Name = "Year")]
        public int? Year { get; set; }

        [Display(Name = "Director")]
        public string? Director { get; set; }
    }
}

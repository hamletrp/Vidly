using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Range(1,20,ErrorMessage = "The Number in stock must be between 1 and 20")]
        [Required]
        [Display(Name = "Number in Stock")]
        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }

    }
}
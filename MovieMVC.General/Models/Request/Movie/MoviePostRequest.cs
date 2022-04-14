using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieMVC.General.Models.Request.Movie
{
    public class MoviePostRequest
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Produced Year")]
        [Required(ErrorMessage = "Produced Year is required")]
        public int ProducedYear { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Movie Description is required")]
        public string Description { get; set; }

        [Display(Name = "Poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required")]
        public string PosterUrl { get; set; }

        [Display(Name = "Movie start date")]
        [Required (ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        public double MoviePrice { get; set; }

        public bool IsActive { get; set; }
   
    }
}

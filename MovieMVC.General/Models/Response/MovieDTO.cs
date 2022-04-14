using System;
using System.Collections.Generic;

namespace MovieMVC.General.Models.Response
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProducedYear { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Subject { get; set; }
        public double MoviePrice { get; set; }
        public bool IsActive { get; set; }
    }
}

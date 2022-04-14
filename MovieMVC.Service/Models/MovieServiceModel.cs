using System;
using System.Collections.Generic;
using System.Text;

namespace MovieMVC.Service.Models
{
    public class MovieServiceModel
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

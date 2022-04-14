using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApi.Web.Models.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProducedYear { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
        public DateTime StartTime { get; set; }
        public double Duration { get; set; }
        public string Subject { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
    }
}

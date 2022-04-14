using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieWebApi.Domain.POCO
{        
    public class Movies
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProducedYear { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Subject { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        //navigation property
        //public List<OrderMovie> Tickets { get; set; }
    }
}

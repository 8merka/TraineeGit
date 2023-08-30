using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TraineeWEB.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Stars { get; set; }

        
        public int FilmId { get; set; }
        [BindNever]
        public Film Film { get; set; } = new Film();
    }
}

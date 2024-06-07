using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TraineeWEB.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Biography { get; set; }

        [JsonIgnore]
        public IEnumerable<ActorRole> ActorRoles { get; set; } = new List<ActorRole>();
    }
}
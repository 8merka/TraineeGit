using System.Text.Json.Serialization;

namespace TraineeWEB.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

        [JsonIgnore]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        [JsonIgnore]
        public ICollection<ActorRole> ActorRoles { get; set; } = new List<ActorRole>();
    }
}

using System.Text.Json.Serialization;

namespace TraineeWEB.Models
{
    public class ActorRole
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int ActorId { get; set; }
        public string CharacterName { get; set; }

        [JsonIgnore]
        public Film Film { get; set; }
        [JsonIgnore]
        public Actor Actor { get; set; }
    }
}

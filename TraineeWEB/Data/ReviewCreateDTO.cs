namespace TraineeWEB.Data
{
    public class ReviewCreateDTO
    {
        public string title { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public int stars { get; set; }
        public int filmId { get; set; } 
    }
}

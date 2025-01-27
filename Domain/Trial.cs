namespace Domain
{
    public class Trial
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int Participants { get; set; } //Should be splitted into another table Participant
        public Status Status { get; set; }
    }
}

using Domain;

namespace Appplication.DTOs.Import.Post
{
    public class TrialDto
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Participants { get; set; }
        public required string Status { get; set; }
    }
}

﻿using Domain;

namespace Appplication.DTOs.Import.Get
{
    public class TrialReadDto
    {
        public required string TrialId { get; set; }
        public required string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Participants { get; set; }
        public int Duration { get; set; }
        public required string Status { get; set; }
    }
}

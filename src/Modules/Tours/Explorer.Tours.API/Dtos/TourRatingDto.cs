﻿namespace Explorer.Tours.API.Dtos
{
    public class TourRatingDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int TouristId { get; set; }
        public int TourId { get; set; }
        public DateTime TourDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string[]? Pictures { get; set; }
    }
}

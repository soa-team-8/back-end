namespace Explorer.Encounters.API.Dtos
{
    public class EncounterDto
    {
        public long AuthorId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XP { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public double? LocationLongitude { get; set; }
        public double? LocationLatitude { get; set; }
        public string? Image { get; set; }
        public double? Range { get; set; }
        public int? RequiredPeople { get; set; }
        public List<int>? ActiveTouristsIds { get; set; }
    }
}

﻿using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Dtos;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class Encounter:Entity
    {
        public long AuthorId { get; init; }
        public string Name { get; init; }    
        public string Description { get; init; }
        public int XP { get; init; }
        public EncounterStatus Status { get; init; }
        public EncounterType Type { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public SocialEncounter? SocialEncounter { get; init; }
        public HiddenLocationEncounter? HiddenLocationEncounter { get; init;}
        public List<CompletedEncounter>? CompletedEncounter { get; init; }

        public Encounter() { }

        public Encounter(long authorId,string name, string description, int xP, EncounterType type,
            double latitude, double longitude,HiddenLocationEncounterDto? hiddenLocation,SocialEncounterDto? socialEncounter)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid name.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Invalid description");
            if (authorId==0) throw new ArgumentException("Invalid author");
            if (xP < 0) throw new ArgumentException("Invalid xP");
            if (latitude < -90 || latitude > 90) throw new ArgumentException("Invalid latitude");
            if(longitude < -180 || longitude>180) throw new ArgumentException("Invalid longitude");

            AuthorId = authorId;
            Name = name;
            Description = description;
            XP = xP;
            Status = EncounterStatus.Draft;
            Type = type;
            Latitude = latitude;
            Longitude = longitude;
            if (hiddenLocation != null) 
                HiddenLocationEncounter = new HiddenLocationEncounter(hiddenLocation.Longitude,hiddenLocation.Latitude,hiddenLocation.Image,hiddenLocation.Range);
            if (socialEncounter != null)
                SocialEncounter = new SocialEncounter(socialEncounter.RequiredPeople, socialEncounter.Range);
        }


    }
    public enum EncounterStatus
    {
        Draft,
        Active,
        Archived
    }
    public enum EncounterType
    {
        Social,
        Location,
        Misc
    }

}

﻿using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using System.Text.Json.Serialization;

namespace Explorer.Encounters.Core.Domain.Encounters
{
    public class SocialEncounter : Encounter
    {
        public int RequiredPeople { get; init; }
        public double Range { get; init; }
        public List<int>? ActiveTouristsIds { get; init; }
        public SocialEncounter() { }

        public SocialEncounter(SocialEncounter socialEncounter)
        {
            //Causes(new SocialEncounterCreated(socialEncounter.Id, DateTime.UtcNow, RequiredPeople, Range));
        }
        public int CheckIfInRange(double touristLongitude, double touristLatitude, int touristId)
        {
            double distance = Math.Acos(Math.Sin(Math.PI/180*(Latitude)) * Math.Sin(Math.PI / 180 * touristLatitude) + Math.Cos(Math.PI / 180 * Latitude) * Math.Cos(Math.PI / 180 * touristLatitude) * Math.Cos(Math.PI / 180 * Longitude - Math.PI / 180 * touristLongitude)) * 6371000;
            if (distance > Range)
            {
                RemoveTourist(touristId);
                return 0;
            }
            else AddTourist(touristId);
            return ActiveTouristsIds.Count();
        }

        private void AddTourist(int touristId)
        {
            if(ActiveTouristsIds != null && !ActiveTouristsIds.Contains(touristId))
                ActiveTouristsIds.Add(touristId);
        }

        private void RemoveTourist(int touristId)
        {
            if (ActiveTouristsIds != null && ActiveTouristsIds.Contains(touristId))
                ActiveTouristsIds.Remove(touristId);
        }
        public bool IsRequiredPeopleNumber()
        {
            int numberOfTourists = ActiveTouristsIds.Count();
            if(numberOfTourists >= RequiredPeople) { ClearActiveTourists(); }
            return numberOfTourists >= RequiredPeople;
        }

        private void ClearActiveTourists()
        {
            ActiveTouristsIds.Clear();
        }
    }
}

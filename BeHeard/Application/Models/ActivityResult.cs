using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class ActivityResult
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int SentenceSet { get; set; }
        public string Decibel { get; set; }
        public int Counter { get; set; } = 0;
        public ActivityLevel Difficulty { get; set; }
        public Syllable Syllable { get; set; }
        public Exercise Exercise { get; set; }
        public Category Category { get; set; }
        public Guid UserProfileId { get; set; }
    }

    public enum ActivityLevel
    {
        Easy, Medium, Hard, Impossible
    }
    public enum Syllable
    {
        A, E, O, U, NONE
    }
    public enum Exercise
    {
        VolumeChasing, Breathing, Phrasing, Rote
    }
    public enum Category
    {
        Cities, Directions, PhoneNumbers, CommonRequests, MealOrders, NONE
    }
}

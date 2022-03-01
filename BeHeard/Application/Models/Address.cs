using System;

namespace BeHeard.Application.Models
{
    public class Address
    {
        public Guid   Id              { get; set; }
        public int    ApartmentNumber { get; set; }
        public int    StreetNumber    { get; set; }
        public string Apartment       { get; set; }
        public string City            { get; set; }
        public string Country         { get; set; }
        public string PostalCode      { get; set; }
        public string State           { get; set; }
        public string Street          { get; set; }
    }
}

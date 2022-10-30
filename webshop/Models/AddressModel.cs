using System;

namespace webshop.Models
{
    public class AddressModel
    {
        public Guid Id { get; set; }

        public string StreetName { get; set; }

        public string Postcode { get; set; }

        public int HouseNumber { get; set; }

        public string HouseAddition { get; set; }

        public string City { get; set; }
    }
}
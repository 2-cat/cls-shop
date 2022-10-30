using System;

namespace webshop.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Guid AddressId { get; set; }

        public  AddressModel Address { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
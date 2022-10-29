using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webshop.Entities
{
    [Table("Addresses")]
    public class AddressEntity : BaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        public string StreetName { get; set; }

        public string Postcode { get; set; }

        public int HouseNumber { get; set; }

        public string HouseAddition { get; set; }

        public string City { get; set; }
    }
}
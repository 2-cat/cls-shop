using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webshop.Entities
{
    /*
     * One-to-One example
     * A customer in this case can only have one address
     */
    [Table("Customers")]
    public class CustomerEntity : BaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        public string EmailAddress { get; set; }

        public virtual AddressEntity Address { get; set; }
    }
}
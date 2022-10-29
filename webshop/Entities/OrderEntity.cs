using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webshop.Entities
{
    /*
     * One-to-Many example
     * An order can only have one customer
     * while a customer can have multple orders
     */
    [Table("Orders")]
    public class OrderEntity : BaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        public int OrderNumber { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public virtual CustomerEntity Customer { get; set; }

        public virtual ICollection<ProductPerOrderEntity> Products { get; set; }
    }
}
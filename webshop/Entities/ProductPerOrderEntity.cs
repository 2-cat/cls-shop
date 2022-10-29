using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webshop.Entities
{
    [Table("ProductsPerOrder")]
    public class ProductPerOrderEntity : BaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public virtual OrderEntity Order { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}
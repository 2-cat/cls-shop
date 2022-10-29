using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webshop.Entities
{
    [Table("Products")]
    public class ProductEntity : BaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<AuthoredByEntity> Authors { get; set; }
    }
}
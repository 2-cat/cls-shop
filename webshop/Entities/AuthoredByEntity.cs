using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webshop.Entities
{
    /*
     * Many-to-Many example
     * Books can have multiple authors
     * while authors can have written multple books
     */

    [Table("AuthoredBy")]
    public class AuthoredByEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }

        public virtual AuthorEntity Author { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }
    }
}
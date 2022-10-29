using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webshop.Entities
{
    [Table("Authors")]
    public class AuthorEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace webshop.Entities
{
    public class BaseEntity
    {

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool IsDeleted { get; set; }
    }
}
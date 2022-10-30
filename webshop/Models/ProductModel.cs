using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webshop.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public IList<AuthorModel> Authors { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
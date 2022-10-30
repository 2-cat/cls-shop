using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using webshop.Entities;

namespace webshop
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductPerOrderEntity> ProductsPerOrder { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<AuthoredByEntity> AuthoredBy { get; set; }
        public ApplicationDbContext()
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
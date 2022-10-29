using SimpleInjector;
using Microsoft.Owin;
using Owin;
using webshop.Entities;
using System;
using System.Linq;
using AutoMapper;
using webshop.Repositories;

[assembly: OwinStartupAttribute(typeof(webshop.Startup))]
namespace webshop
{
    public class Startup
    {
        private Container container;


        public void Configuration(IAppBuilder app)
        {
            ConfigureDatabase();
            ConfigureMapping();
            ConfigureDependencyInjection();
        }

        private void ConfigureDependencyInjection()
        {
            //Configure dependency injection
            container = DependencyContainer.Default.Container;
        }

        /*
         * Here we create some initial data, if nothing exists yet
         * 
         */
        private void ConfigureDatabase()
        {
            using (var context = new ApplicationDbContext())
            {
                var existingCustomer = context.Customers.FirstOrDefault(e => e.FirstName == "Buggs" && e.LastName == "Bunny");
                if(existingCustomer != null)
                {
                    // There's no need to create additional (or duplicate) entries if this step has already been completed before. 
                    return;
                }

                var customerAddress = new AddressEntity()
                {
                    Id = Guid.NewGuid(),
                    City = "Haarlem",
                    StreetName = "Haarlemmerstraat",
                    HouseNumber = 1,
                    Postcode = "1234ZY",
                    DateCreated = DateTime.Now
                };

                context.Addresses.Add(customerAddress);

                var customer = new CustomerEntity()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Buggs",
                    LastName = "Bunny",
                    DateCreated = DateTime.Now,
                    AddressId = customerAddress.Id
                };

                context.Customers.Add(customer);

                var secondAddress = new AddressEntity()
                {
                    Id = Guid.NewGuid(),
                    City = "Utrecht",
                    StreetName = "Drieharingstraat",
                    HouseNumber = 6,
                    Postcode = "9876AB",
                    DateCreated = DateTime.Now
                };

                context.Addresses.Add(secondAddress);

                var secondCustomer = new CustomerEntity()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Daffy",
                    LastName = "Duck",
                    DateCreated = DateTime.Now,
                    AddressId = secondAddress.Id
                };

                context.Customers.Add(secondCustomer);

                var jrrTolkien = new AuthorEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "J.R.R. Tolkien"
                };

                context.Authors.Add(jrrTolkien);

                var christopherTolkien = new AuthorEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "Christopher Tolkien"
                };

                context.Authors.Add(christopherTolkien);

                var warOfTheRing = new ProductEntity()
                {
                    Id = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    Name = "War of the Ring",
                    Description = "The third part of The History of The Lord of the Rings, an enthralling account of the writing of the Book of the Century which contains many additional scenes and includes the unpublished Epilogue in its entirety.",
                    Price = 14.99m
                };

                context.Products.Add(warOfTheRing);

                var warOfTheRingAuthoredByJRR = new AuthoredByEntity()
                {
                    Id = Guid.NewGuid(),
                    AuthorId = jrrTolkien.Id,
                    ProductId = warOfTheRing.Id
                };

                context.AuthoredBy.Add(warOfTheRingAuthoredByJRR);

                var warOfTheRingAuthoredByChristopher = new AuthoredByEntity()
                {
                    Id = Guid.NewGuid(),
                    AuthorId = christopherTolkien.Id,
                    ProductId = warOfTheRing.Id
                };
                context.AuthoredBy.Add(warOfTheRingAuthoredByChristopher);
                context.SaveChanges();
            }
        }

        private void ConfigureMapping()
        {
            Mapper.Initialize(config =>
            {
                ProductsRepository.ConfigureMapping(config);
                AuthorsRepository.ConfigureMapping(config);
            });
        }
    }
}
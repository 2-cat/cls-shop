using SimpleInjector;
using Microsoft.Owin;
using Owin;
using webshop.Entities;
using System;
using System.Linq;
using AutoMapper;
using webshop.Repositories;
using webshop.Models;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(webshop.Startup))]
namespace webshop
{
    public class Startup
    {
        private Container container;


        public void Configuration(IAppBuilder app)
        {
            ConfigureMapping();
            ConfigureDependencyInjection();
            ConfigureDatabase();
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
            var productsRepository = container.GetInstance<IProductsRepository>();
            var authorsRepository = container.GetInstance<IAuthorsRepository>();
            var customerRepository = container.GetInstance<ICustomerRepository>();

            // There's no need to create additional (or duplicate) entries if this step has already been completed before. 
            if (authorsRepository.GetRange().Count > 0)
            {
                return;
            }

            var jrrTolkienId = authorsRepository.Create("J.R.R. Tolkien");
            var christopherTolkienId = authorsRepository.Create("Christopher Tolkien");
            var frankHerbertId = authorsRepository.Create("Frank Herbert");
            var joeAbercrombieId = authorsRepository.Create("Joe Abercrombie");

            var warOfTheRing = new ProductModel()
            {
                Name = "War of the Ring",
                Description = "The third part of The History of The Lord of the Rings, an enthralling account of the writing of the Book of the Century which contains many additional scenes and includes the unpublished Epilogue in its entirety. ",
                Price = 14.99m,
                Authors = authorsRepository.GetByName("Tolkien")
            };

            productsRepository.Create(warOfTheRing);

            var morgothsRing = new ProductModel()
            {
                Name = "Morgoth's Ring",
                Description = "The first of two companion volumes which documents the later writing of The Silmarillion, Tolkien's epic tale of war. ",
                Price = 10m,
                Authors = authorsRepository.GetByName("Christopher Tolkien")
            };
            productsRepository.Create(morgothsRing);

            var dune = new ProductModel()
            {
                Name = "Dune",
                Description = "Set on the desert planet Arrakis, Dune is the story of the boy Paul Atreides, heir to a noble family tasked with ruling an inhospitable world where the only thing of value is the “spice” melange, a drug capable of extending life and enhancing consciousness.",
                Price = 10m,
                Authors = new List<AuthorModel>()
                {
                    authorsRepository.GetById(frankHerbertId)
                }
            };
            productsRepository.Create(dune);

            var theBladeItself = new ProductModel()
            {
                Name = "The Blade itself",
                Description = "Logen Ninefingers, infamous barbarian, has finally run out of luck. Caught in one feud too many, he’s on the verge of becoming a dead barbarian – leaving nothing behind him but bad songs, dead friends, and a lot of happy enemies. ",
                Price = 10m,
                Authors = new List<AuthorModel>()
                {
                    authorsRepository.GetById(joeAbercrombieId)
                }
            };
            productsRepository.Create(theBladeItself);

            var customerAddress = new AddressModel()
            {
                City = "Haarlem",
                StreetName = "Haarlemmerstraat",
                HouseNumber = 1,
                Postcode = "1234ZY"
            };

            var customer = new CustomerModel()
            {
                FirstName = "Buggs",
                LastName = "Bunny",
                EmailAddress = "buggs@bunny.com",
                Address = customerAddress
            };
            customerRepository.Create(customer);

            var secondAddress = new AddressModel()
            {
                City = "Utrecht",
                StreetName = "Drieharingstraat",
                HouseNumber = 6,
                Postcode = "9876AB"
            };

            var secondCustomer = new CustomerModel()
            {
                FirstName = "Daffy",
                LastName = "Duck",
                EmailAddress = "daffy@duck.com",
                Address = secondAddress
            };
            customerRepository.Create(secondCustomer);
        }

        private void ConfigureMapping()
        {
            Mapper.Initialize(config =>
            {
                ProductsRepository.ConfigureMapping(config);
                AuthorsRepository.ConfigureMapping(config);
                CustomerRepository.ConfigureMapping(config);
            });
        }
    }
}
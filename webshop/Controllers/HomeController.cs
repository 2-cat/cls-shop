using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webshop.Models;
using webshop.Repositories;

namespace webshop.Controllers
{
    public class HomeController : Controller
    {
        private IProductsRepository productsRepository { get; set; }
        private IAuthorsRepository authorsRepository { get; set; }
        private ICustomerRepository customerRepository { get; set; }

        public HomeController(IProductsRepository _productsRepository, IAuthorsRepository _authorsRepository, ICustomerRepository _customerRepository)
        {
            productsRepository = _productsRepository;
            authorsRepository = _authorsRepository;
            customerRepository = _customerRepository;
        }

        public ActionResult Index()
        {
            var authors = authorsRepository.GetRange();

            // testing data retrieval
            var products = productsRepository.GetRange(0, 12);
            var ringBooks = productsRepository.GetByName("RiNg");

            var customers = customerRepository.GetRange();


            ViewBag.Title = "Home Page";

            return View();
        }
    }
}

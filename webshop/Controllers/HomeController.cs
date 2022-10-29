using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webshop.Repositories;

namespace webshop.Controllers
{
    public class HomeController : Controller
    {
        private IProductsRepository productsRepository { get; set; }
        private IAuthorsRepository authorsRepository { get; set; }

        public HomeController(IProductsRepository _productsRepository, IAuthorsRepository _authorsRepository)
        {
            productsRepository = _productsRepository;
            authorsRepository = _authorsRepository;
        }

        public ActionResult Index()
        {
            // testing data retrieval
            var products = productsRepository.GetRange();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}

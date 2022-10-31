using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using webshop.Repositories;

namespace webshop.Controllers
{
    public class ProductsController : BaseApiController
    {
        private IProductsRepository productsRepository { get; set; }
        public ProductsController()
        {
            productsRepository = DependencyContainer.Default.Container.GetInstance<IProductsRepository>();
        }

        [System.Web.Http.Route("api/products/GetRange")]
        [System.Web.Http.HttpGet]
        public JsonResult GetRange(int startPage = 0, int count = 12)
        {
            var products = productsRepository.GetRange(startPage, count); ;

            var obj = new { products };
            return JsonResult(obj);
        }

        [System.Web.Http.Route("api/products/GetById/{id}")]
        [System.Web.Http.HttpGet]
        public JsonResult GetById(Guid id)
        {
            var product = productsRepository.GetById(id); ;

            var obj = new { product };
            return JsonResult(obj);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using webshop.Repositories;

namespace webshop.Controllers
{
    public class AuthorsController : BaseApiController
    {
        private IAuthorsRepository authorsRepository { get; set; }
        public AuthorsController()
        {
            authorsRepository = DependencyContainer.Default.Container.GetInstance<IAuthorsRepository>();
        }

        [System.Web.Http.Route("api/authors/GetById/{id}")]
        [System.Web.Http.HttpGet]
        public JsonResult GetById(Guid id)
        {
            var author = authorsRepository.GetById(id); ;

            var obj = new { author };
            return JsonResult(obj);
        }

        [System.Web.Http.Route("api/authors/GetByKeyword/{keyword}")]
        [System.Web.Http.HttpGet]
        public JsonResult GetByKeyword(string keyword)
        {
            var authors = authorsRepository.GetByName(keyword); ;

            var obj = new { authors };
            return JsonResult(obj);
        }

        [System.Web.Http.Route("api/authors/GetRange")]
        [System.Web.Http.HttpGet]
        public JsonResult GetRange(int startPage = 0, int count = 12)
        {
            var authors = authorsRepository.GetRange(startPage, count); ;

            var obj = new { authors };
            return JsonResult(obj);
        }

        [System.Web.Http.Route("api/authors/Create/")]
        [System.Web.Http.HttpPost]
        public JsonResult Create(string value)
        {
            var id = authorsRepository.Create(value);
            return JsonResult(new HttpStatusCodeResult(HttpStatusCode.OK));
        }
    }
}
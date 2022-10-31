using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace webshop.Controllers
{
    public class BaseApiController : ApiController
    {
        // Removes the need to create a new JsonResult object manually. 
        protected JsonResult JsonResult(object data)
        {
            return new JsonResult()
            {
                Data = data,
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }
    }
}
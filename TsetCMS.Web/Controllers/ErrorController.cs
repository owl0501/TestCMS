using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestCMS.Helper.Data;

namespace TsetCMS.Web.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Internal Server Error
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current.Id });
        }
    }
}

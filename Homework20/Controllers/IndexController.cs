using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Homework20.Controllers
{
    public class IndexController : Controller
    {
        /// <summary>
        /// Метод отображения стартовой страницы
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}

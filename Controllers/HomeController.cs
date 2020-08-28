using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.ViewModels;

namespace MovieApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            MoviesViewModel viewModel = new MoviesViewModel();
            return View(viewModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MovieApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            MoviesViewModel viewModel = new MoviesViewModel();
            return View(viewModel);
        }

        [Route("movie/{slug}")]
        public IActionResult getMovieInfo(String title)
        {
           
            MoviesViewModel viewModel = new MoviesViewModel();
            MovieApplication.Models.MovieItem movieInfo = null;

            if(viewModel.MovieItems.Exists(x => x.Title == title))
            {
               movieInfo = viewModel.MovieItems.Find(x => x.Title == title);

               ViewBag.Synopsis = movieInfo.Synopsis;
            }

            return PartialView("_MovieInfo");
        }
    }
}

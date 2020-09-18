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
        public static HashSet<string> filters = new HashSet<string>();

        public IActionResult Index()
        {
            IndexCommonViewModel viewModel = new IndexCommonViewModel();
            viewModel.MoviesVM = new MoviesViewModel();
            viewModel.RegisterVM = new RegisterViewModel();

            return View(viewModel);
        }

        [Route("movie/{slug}")]
        public IActionResult getMovieInfo(string title)
        {
            MoviesViewModel viewModel = new MoviesViewModel();
            MovieApplication.Models.MovieItem movieInfo = null;

            if (viewModel.MovieItems.Exists(x => x.Title == title))
            {
                movieInfo = viewModel.MovieItems.Find(x => x.Title == title);

                ViewBag.Synopsis = movieInfo.Synopsis;
            }

            return PartialView("_MovieInfo");
        }


        [Route("movie/filterMovies")]
        public IActionResult filterMovies(string filter, MoviesViewModel viewModel)
        {
            IndexCommonViewModel commonViewModel = new IndexCommonViewModel();
            commonViewModel.MoviesVM = viewModel;

            //Add/Remove filter
            if (!filters.Add(filter))
            {
                filters.Remove(filter);
            }

            //Generate CSV string of filters
            string enabledFilters = "";
            foreach (string f in filters) {
                enabledFilters += f + ',';
            }
            enabledFilters = enabledFilters.TrimEnd(',');

            if (String.IsNullOrEmpty(enabledFilters))
            {
                commonViewModel.MoviesVM = new MoviesViewModel();
                return PartialView("_MovieBrowse", commonViewModel);
            }
            else
            {
                viewModel.getFilteredMovies(enabledFilters);
                return PartialView("_MovieBrowse", commonViewModel);
            }

        }

        [HttpPost]
        [Route("HomeController/submitForm")]
        public IActionResult submitForm(IndexCommonViewModel model)
        {
            if (ModelState.IsValid)
            {
                int result = model.RegisterVM.addClient();
                //return Content($"User {model.RegisterVM.FirstName} added!");
            }
                

            model.MoviesVM = new MoviesViewModel();
            return View("Index", model);
            
               

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ApplicationUser = MovieApplication.Models.ApplicationUser;


namespace MovieApplication.Controllers
{
    public class HomeController : Controller
    {
        public static HashSet<string> filters = new HashSet<string>();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

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

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(IndexCommonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { FirstName = model.RegisterVM.FirstName,
                                                   Surname = model.RegisterVM.Surname,
                                                   Email = model.RegisterVM.Email,
                                                   DateOfBirth = model.RegisterVM.DateOfBirth};
                var result = await _userManager.CreateAsync(user, model.RegisterVM.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    AddErrorsToModelState(result);
                }
            }
            model.MoviesVM = new MoviesViewModel();
            return View("Index", model);

        }

        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(IndexCommonViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var result = await _signInManager.PasswordSignInAsync(model.LoginVM.Email, 
                    model.LoginVM.Password, model.LoginVM.RememberMe,lockoutOnFailure:false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User signed in");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogWarning("User failed login");
                    ModelState.AddModelError(string.Empty,"Email or password is incorrect");
                }
            }
            model.MoviesVM = new MoviesViewModel();
            return View("Index", model);


        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out");
            return RedirectToAction("Index", "Home");
        }
        
        public void AddErrorsToModelState(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty,error.Description);
            }
        }
    }
}

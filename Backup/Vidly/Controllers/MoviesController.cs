using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles = RoleName.canManageMovies)]
        public ViewResult New()
        {
            var genres = _context.Genre.ToList();

            var viewModel = new MovieFormViewModel()
            {
                Genre = genres
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genre = _context.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;

                _context.Movies.Add(movie);
            }

            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ViewResult Index()
        {
            //its not necesary to use this list because we are using data-tables
            //var movies = _context.Movies.Include(m => m.Genre).ToList();
            //return View(movies);

            if (User.IsInRole(RoleName.canManageMovies))
                return View("List");
            else

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(movie);
            }
        }

        [Authorize(Roles = RoleName.canManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genre = _context.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }

        // GET: Movies/Random
        //public ActionResult Random()
        //{
        //    //var movie = new Movie() { Name = "Shrek!" };

        //    //var customers = new List<Customer>
        //    //{
        //    //    new Customer {Name = "customer 1"},
        //    //    new Customer {Name = "customer2"}
        //    //};

        //    //var viewModel = new RandomMovieViewModel
        //    //{
        //    //    Movie = movie,
        //    //    Customer = customers
        //    //};


        //   // return View(movieViewModel);

        //    //return Content("Hello world ..... ");

        //    //return HttpNotFound();

        //    //return new EmptyResult();

        //    //return RedirectToAction("Index", "Home", new { page = 1, sortby = "name"});  // When we want to redirect the user from one page to another we need to pass a argument to the target action. In the third argument we have to use anonimous Objet {}
            
        //}

        //public ActionResult Edit(int Id)
        //{
        //    return Content("id = " + Id);
        //}
      
        //// movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex = {0}&sortBy = {1}", pageIndex, sortBy));
        //}

        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]// Route customed
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content( year + "/" + month);

        //}


        //[Route("Movies")]
        //public ActionResult Movies()
        //{
        //    var movie = new List<Movie>
        //    {
        //        new Movie {Name = "Shrek!"},
        //        new Movie {Name = "Wall-e"}
        //    };

        //    var movieViewModel = new MoviesViewModel()
        //    {
        //        Movie = movie
        //    };



        //    return View(movieViewModel);

        //}
    }

}
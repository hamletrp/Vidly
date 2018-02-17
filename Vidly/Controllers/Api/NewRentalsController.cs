using System;
using System.Linq;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]   //Using Optimistic approach to handle different EDGE CASE
        public IHttpActionResult CreateNewRentals(NewRentalDTO newRental)
        {
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(
                m => newRental.MoviesIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is no available");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }

        //Using defensive approach to validate different EDGE CASE
        //[System.Web.Http.HttpPost]
        //public IHttpActionResult CreateNewRentals(NewRentalDTO newRental)
        //{
        //    if (newRental.MoviesId.Count == 0)
        //        return BadRequest("No Movies IDs have been given");

        //    var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
        //    if (customer == null)
        //        return BadRequest("CustomerID is not valid");

        //    var movies = _context.Movies.Where(m => newRental.MoviesId.Contains(m.Id)).ToList();
        //    if (movies.Count != newRental.MoviesId.Count)
        //        return BadRequest("One or more MoviesIDS are Invalid");


        //    foreach (var movie in movies)
        //    {
        //        if (movie.NumberAvailable == 0)
        //            return BadRequest("Movie is no available");

        //        movie.NumberAvailable--;

        //        var rental = new Rental
        //        {
        //            Customer = customer,
        //            Movie = movie,
        //            DateRented = DateTime.Now
        //        };

        //        _context.Rentals.Add(rental);
        //    }

        //    _context.SaveChanges();

        //    return Ok();
        //}
    }
}

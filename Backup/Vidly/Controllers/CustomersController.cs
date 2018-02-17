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
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipType.ToList();

            var viewModel = new CustomerFormViewModel()
            {
                MembershipType = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            // This point use model state property to get access to validation data.
            // you can use to chance the application flow
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipType = _context.MembershipType.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //TryUpdateModel(customerInDb);// this method update all property with value pair but this method has some security issues
                //Mapper.Map(customer, customerInDb);// this method map all property in customerInDb with customer 
                customerInDb.Name = customer.Name;
                customerInDb.LastName = customer.LastName;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSuscribedToNewsLetter = customer.IsSuscribedToNewsLetter;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ViewResult Index()
        {
            // we dont need to use this list because data-table do it for us
            // var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customers);

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).ToList().SingleOrDefault(c => c.Id == id);
           // var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            //if (customer.BirthDate.HasValue)
            //{
            //    return View(customers);
            //}
            

            if (customer == null)
                return HttpNotFound();

            return  View (customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipType = _context.MembershipType.ToList() 
            };
            
            return View("CustomerForm", viewModel);
        }
    }
        // GET: Customer
        //[Route("Customers")]
        //public ActionResult Customers()
        //{
        //    var customer = new List<Customer>
        //    {
        //        new Customer { Name = "Natali", LastName = "Bueno"},
        //        new Customer { Name = "Wilkin", LastName = "Valdez"}
        //    };

        //    if (customer == null)
        //    {
        //        return new HttpNotFoundResult();
        //    }

        //    var customerViewModel = new CustomersViewModel()
        //    {
        //        Customer = customer
        //    };

        //    return View(customerViewModel);
        //}
}
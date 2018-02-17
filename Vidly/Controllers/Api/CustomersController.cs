using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.Models;
using Vidly.DTOs;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /Api/Customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customerQuery = _context.Customers
                .Include(c => c.MembershipType);

            if (!String.IsNullOrWhiteSpace(query)) //Improving Typeahead
                customerQuery = customerQuery.Where(c => c.Name.Contains(query));
                
            var customerDtos = customerQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>); //here we need to delegate a reference to the customerdto method using autoMapper

            return Ok(customerDtos);
        }

        // GET /Api/Customers/1
        public IHttpActionResult GetCustomers(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return  Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /Api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto); // using autoMapper

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri +  "/" + customer.Id), customerDto);
        }

        //PUT /api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);
            
            _context.SaveChanges();

            return Ok();
        }

        //DELETE /Api/Customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext context;

        public CustomersController()
        {
            context = new ApplicationDbContext();

        }

        //GET api/customers/
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            var customersInDb = context.Customers.ToList();
            return customersInDb;
        }

        [HttpGet]
        public Customer GetCustomer(int id)
        {
            var customerInDb = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            return customerInDb;


        }

        [HttpPost]
        public void CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            var customerInDb = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.Name = customer.Name;
            context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            context.Customers.Remove(customerInDb);
        }
    }
}

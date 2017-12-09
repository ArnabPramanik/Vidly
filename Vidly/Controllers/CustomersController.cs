using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ViewResult Index()
        {
            var customers = GetCustomers();


            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => id == c.Id );
            if(customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>()
            {
               new Customer(){Id = 1 , Name = "Robert" },
               new Customer(){Id = 2 , Name = "Arnab" }

            };

        }


    }
}
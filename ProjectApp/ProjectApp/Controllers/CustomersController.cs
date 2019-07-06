﻿using ProjectApp.Models;
using ProjectApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectApp.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Random()
        {
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"},
                new Customer {Name = "Customer 3"},
                new Customer {Name = "Customer 4"},
                new Customer {Name = "Customer 5"},
                new Customer {Name = "Customer 6"}
            };
            //var customers = new Customer() { Name = "Customer 1"};
            var viewModel = new RandomCarViewModel
            {
                ListCustomers = customers
            };
            return View(viewModel);
        }
    }
}
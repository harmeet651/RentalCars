using ProjectApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectApp.ViewModels
{
    public class CustomerListAndItself
    {
        public List<Car> ListCars { get; set; }
        public List<Customer> ListCustomers { get; set; }
    }
}
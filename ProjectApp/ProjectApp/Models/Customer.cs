using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class CustomerContext : DbContext
    {
        public DbSet<Customer> customer { get; set; }
    }
}
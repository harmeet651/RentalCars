using ProjectApp.Models;
using ProjectApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace ProjectApp.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        List<Customer> listCustomer = new List<Customer>();
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ShowCustomers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds, "Customer");
                dt = ds.Tables["Customer"];
                //List<Customer> listCustomer = new List<Customer>();

                foreach (DataRow dr in dt.Rows)
                {
                    listCustomer.Add(new Customer { Id = (int)dr["Id"], Name = dr["Name"].ToString(), MembershipType = dr["MembershipType"].ToString() });
                }
                //var cars = GetCars();
                return View(listCustomer);
            }
        }

        public ActionResult DisplayName(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ShowCustomers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds, "Customer");
                dt = ds.Tables["Customer"];
                //List<Customer> listCustomer = new List<Customer>();

                foreach (DataRow dr in dt.Rows)
                {
                    listCustomer.Add(new Customer { Id = (int)dr["Id"], Name = dr["Name"].ToString(), MembershipType = dr["MembershipType"].ToString() });
                }
                var customer = listCustomer.SingleOrDefault(c => c.Id == id);
                if (customer == null)
                    return HttpNotFound();
                return View(customer);
            }
        }

        //new User form
        public ActionResult New()
        {
            return View();
        }

        //to add new User
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ViewData["Name"] = collection["Name"];
            return View();
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Mr Bhal" },
                new Customer { Id = 2, Name = "Mr Ravi" }
            };
        }
        
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
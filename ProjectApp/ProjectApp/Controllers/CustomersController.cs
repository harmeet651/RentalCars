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

                foreach (DataRow dr in dt.Rows)
                {
                    listCustomer.Add(new Customer { Id = (int)dr["Id"], Name = dr["Name"].ToString(), MembershipType = dr["MembershipType"].ToString(), Owns = dr["Owns"].ToString() });
                }

                DataSet ds2 = new DataSet();
                DataTable dt2 = new DataTable();
                using (SqlConnection con2 = new SqlConnection(connectionString))
                {
                    SqlCommand cmd2 = new SqlCommand("ShowCars", con2);
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    da2.SelectCommand = cmd2;
                    da2.Fill(ds2, "Cars");
                    dt2 = ds2.Tables["Cars"];
                    List<Car> listCar = new List<Car>(); ;

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        listCar.Add(new Car { Id = (int)dr2["Id"], Name = dr2["Name"].ToString(), Color = dr2["Color"].ToString(), ReleaseYear = dr2["ReleaseYear"].ToString() });
                    }

                    var modelNew = new CustomerListAndItself { ListCars = listCar, ListCustomers = listCustomer };
                    return View(modelNew);
                }
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
                    listCustomer.Add(new Customer { Id = (int)dr["Id"], Name = dr["Name"].ToString(), MembershipType = dr["MembershipType"].ToString(), Owns = dr["Owns"].ToString() });
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

        //to Add new User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            //checking if model is Valid
            if (!ModelState.IsValid)
            {
                return View("New", customer);
            }
            //end

            // for assigning correct MembershipType according to MembershipTypeId
            if (customer.MembershipTypeId == 1)
            {
                customer.MembershipType = "Monthly";
            }
            else if(customer.MembershipTypeId == 1)
            {
                customer.MembershipType = "Quaterly";
            }
            else
            {
                customer.MembershipType = "Yearly";
            }
            //end

            //getting count of customers to Assign if for new customer
            int count = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("CountCustomers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet d = new DataSet();
                da.Fill(d);
                con.Open();
                DataTable dt = new DataTable();
                dt = d.Tables["Table"];
                DataRow dr;
                dr = dt.Rows[0];
                count = (int)dr.ItemArray[0];
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertCustomer", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = count+1;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = customer.Name;
                cmd.Parameters.Add("@MembershipType", SqlDbType.VarChar, 250).Value = customer.MembershipType;
                cmd.Parameters.Add("@MembershipTypeId", SqlDbType.Int).Value = customer.MembershipTypeId;
                cmd.Parameters.Add("@IsSubscribedToNewsLetter", SqlDbType.Bit).Value = customer.IsSubscribedToNewsLetter;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Customers");
        }

        [HttpPost]
        public ActionResult AddNewCarToThisCustomer(Customer customer)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddNewcarToThisCustomer", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = customer.Id;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = customer.Owns;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Customers");
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
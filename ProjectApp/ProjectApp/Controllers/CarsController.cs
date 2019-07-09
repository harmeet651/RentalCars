using ProjectApp.Models;
using ProjectApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace ProjectApp.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            string connectionString = "Data Source=52.189.181.41 ;Initial Catalog=Sample; User ID=sa;Password=Lb@zxc)0";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //SqlCommand cmd = new SqlCommand("select * from CustomerCar", con);
                //con.Open();
                SqlConnection objConn = new SqlConnection(connectionString);
                objConn.Open();
                SqlDataAdapter daAuthors = new SqlDataAdapter("Select * From CustomerCar", objConn);
                DataSet dsPubs = new DataSet("Pubs");
                daAuthors.FillSchema(dsPubs, SchemaType.Source, "Authors");
                daAuthors.Fill(dsPubs, "Authors");

                DataTable tblAuthors;
                tblAuthors = dsPubs.Tables["Authors"];

                //foreach (DataRow drCurrent in tblAuthors.Rows)
                //{
                //    Console.WriteLine("{0} {1}",
                //    drCurrent["au_fname"].ToString(),
                //    drCurrent["au_lname"].ToString());
                //}
                Console.ReadLine();
            }
            var cars = GetCars();
            return View(cars);
        }
        public ActionResult Random()
        {
            var cars = new List<Car>
            {
                new Car { Name = "Audi" },
                new Car {Name = "BMW"}
            };
            var viewModel = new RandomCarViewModel
            {
                ListCars = cars
            };
            return View(viewModel);
        }
        private IEnumerable<Car> GetCars()
        {
            return new List<Car>
                {
                new Car {Id = 1, Name = "Audi" },
                new Car {Id =2, Name = "BMW"}
            };
        }
        [Route("cars/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
        public ActionResult Edit(int id)
        {
            return Content("id= " + id);
        }
    }
}     
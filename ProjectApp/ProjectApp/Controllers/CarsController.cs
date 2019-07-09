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
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ShowCars", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds, "Cars");
                dt = ds.Tables["Cars"];
                var car = "";
                foreach (DataRow dr in dt.Rows)
                {
                    car = dr["Name"].ToString();
                }
                //var cars = GetCars();
                return View(car);
            }
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
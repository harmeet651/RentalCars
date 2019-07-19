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
using System.Configuration;

namespace ProjectApp.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            //System.Net.WebClient wc = new System.Net.WebClient();
            //var jsonResponse = wc.DownloadString("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=jsonApiUrl"); // you need to parse your json 
            //dynamic Data = Json.Decode(jsonResponse);
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
                List<Car> listCar = new List<Car>(); ;

                foreach (DataRow dr in dt.Rows)
                {
                    listCar.Add(new Car { Id = (int)dr["Id"], Name = dr["Name"].ToString(), Color = dr["Color"].ToString(), ReleaseYear = dr["ReleaseYear"].ToString() });
                }
                //var cars = GetCars();
                return View(listCar);
            }
        }

        public ActionResult DisplayName(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
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
                List<Car> listCar = new List<Car>(); ;

                foreach (DataRow dr in dt.Rows)
                {
                    listCar.Add(new Car { Id = (int)dr["Id"], Name = dr["Name"].ToString(), Color = dr["Color"].ToString(), ReleaseYear = dr["ReleaseYear"].ToString() });
                }
                var car = listCar.SingleOrDefault(c => c.Id == id);
                if (car == null)
                    return HttpNotFound();
                return View(car);
            }
        }

        private IEnumerable<Car> GetCars()
        {
            return new List<Car>
                {
                new Car {Id = 1, Name = "Audi" },
                new Car {Id =2, Name = "BMW"}
            };
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
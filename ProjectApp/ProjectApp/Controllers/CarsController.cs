using ProjectApp.Models;
using ProjectApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectApp.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        public ActionResult Index(int? pageIndex, string sortBy)
        {
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
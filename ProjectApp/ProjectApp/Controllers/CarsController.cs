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
        public ActionResult Random()
        {
            var car = new Car() { Name = "Audi" };
            var customers = new Customer() { Name = "Customer 1"};
            var viewModel = new RandomCarViewModel
            {
                Car = car,
                //Customers = customers
            };
            return View(viewModel);
        }
        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
        public ActionResult Edit(int id)
        {
            return Content("id= " + id);
        }
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";
            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }
    }
}     
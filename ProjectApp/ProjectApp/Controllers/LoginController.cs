using ProjectApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectApp.Controllers
{
    //[AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Post(Login manager)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("LoginPass", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.VarChar, 250).Value = manager.id;
                cmd.Parameters.Add("@Pass", SqlDbType.VarChar, 250).Value = manager.password;
                con.Open();
                //get true false from DB
                String result = (String)cmd.ExecuteScalar();
                if(result.Equals("false"))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Customers");
        }
    }
}
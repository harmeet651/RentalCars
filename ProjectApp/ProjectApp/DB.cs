using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ProjectApp
{
    public class DB
    {
        static void Main()
        {
            string connectionString ="Data Source=52.189.181.41 ;Initial Catalog=Sample; Integrated Security=SPSI";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from CustomerCar", con);
                con.Open();
                Console.WriteLine(cmd);
            }
        }
    }
}
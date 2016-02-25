using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using LocalDbEFMigrations.Models;

namespace LocalDbEFMigrations.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public String Index()
        {
            var retString = string.Empty;

            var constr = ConfigurationManager.ConnectionStrings["AppDb"].ToString();

            //INSERT
            using (var con = new SqlConnection(constr))
            {
                con.Open();
                var query = new SqlCommand($"INSERT INTO [Widget] (Sum) VALUES ({new Random().Next(100)})", con);
                query.ExecuteNonQuery();
                con.Close();
            }

            //SELECT
            using (var con = new SqlConnection(constr))
            {
                con.Open();
                var query = new SqlCommand($"SELECT * FROM [Widget]", con);
                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        retString += ($"Id={reader["Id"]} \t Sum={reader["Sum"]} <br/>");
                    }
                }
                con.Close();
            }

            //Entity Framework
            var db = new AppDbContext();
            db.Widgets.Add(new Widget { Sum = new Random().Next(100) });
            db.SaveChanges();
            foreach (var w in db.Widgets)
            {
                retString += ($"Id={w.Id} \t Sum={w.Sum} <br/>");
            }

            return retString;
        }
    }
}
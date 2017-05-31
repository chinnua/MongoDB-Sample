using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using mongo.Models;

namespace mongo.Controllers
{
    public class HomeController : Controller
    {
        string ConnectionString = ConfigurationManager.AppSettings["MongoDBConn"].ToString();
     
        public ActionResult Index()
        {
            try
            {
                var mongo = new MongoClient(ConnectionString);
                var db = mongo.GetDatabase("testnew");
                List<Customers> lstCus = new List<Customers>();
                for (int i = 15; i < 100000; i++)
                {
                    Customers c = new Customers();
                    c.CustomerID = i;
                    c.Name = "Customer " + i;
                    c.Address = Guid.NewGuid().ToString();
                    lstCus.Add(c);
                }
                
                var collection = db.GetCollection<Customers>("customers");
                //collection.InsertOne(lstCus[0]);
                collection.InsertMany(lstCus);

                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
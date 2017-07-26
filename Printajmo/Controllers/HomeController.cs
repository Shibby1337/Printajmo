using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Printajmo.Controllers
{
    public class HomeController : Controller
    {
        Models.tiskarne_Entities _db;
        public HomeController()
        {
            _db = new Models.tiskarne_Entities();
        }
        public ActionResult Index()
        {
            var newTiskarna = new Models.tiskarne();
            newTiskarna.naziv = "TEST";
            var dbCtx = new Models.tiskarne_Entities();
                //Add  object into  DBset
                //       dbCtx.tiskarne.Add(newTiskarna);

                // call SaveChanges method to object into database
                // dbCtx.SaveChanges();
                var list = dbCtx.tiskarne.ToList();

                /*foreach (var obj in list)
                {
                    Debug.WriteLine(obj.naziv);
                }*/
            ViewData.Model = _db;
            return View(list);
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
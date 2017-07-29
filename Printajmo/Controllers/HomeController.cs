using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace Printajmo.Controllers
{
    public class HomeController : Controller
    {
        Models.TiskarneEntities _db;
        public HomeController()
        {
           if(_db == null)
                _db = new Models.TiskarneEntities();
        }
        public PagedList.IPagedList<Models.tiskarne> GetModel(string sortOrder, string searchString, string currentFilter, int? page)
        {
           

            ViewBag.nazivSortParm = String.IsNullOrEmpty(sortOrder) ? "naziv_desc" : "";
            ViewBag.A4CBSortParm = sortOrder == "A4CB_desc" ? "A4CB_asc" : "A4CB_desc";
            ViewBag.A4BSortParm = sortOrder == "A4B_desc" ? "A4B_asc" : "A4B_desc";
            ViewBag.A4CBOSortParm = sortOrder == "A4CBO_desc" ? "A4CBO_asc" : "A4CBO_desc";
            ViewBag.A4BOSortParm = sortOrder == "A4BO_desc" ? "A4BO_asc" : "A4BO_desc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tiskarne = from s in _db.tiskarne
                           select s;
            tiskarne = tiskarne.OrderBy(s => s.naziv);
            if (!String.IsNullOrEmpty(searchString))
            {
                tiskarne = tiskarne.Where(s => s.naziv.Contains(searchString)
                                       || s.mesto.Contains(searchString)
                                        || s.ulica.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "naziv_desc":
                    tiskarne = tiskarne.OrderByDescending(s => s.naziv);
                    break;
                case "A4CB_desc":
                    tiskarne = tiskarne.OrderBy(s => s.a4cb);
                    break;
                case "A4CB_asc":
                    tiskarne = tiskarne.OrderByDescending(s => s.a4cb);
                    break;
                case "A4B_desc":
                    tiskarne = tiskarne.OrderBy(s => s.a4barvno);
                    break;
                case "A4B_asc":
                    tiskarne = tiskarne.OrderByDescending(s => s.a4barvno);
                    break;
                case "A4CBO_desc":
                    tiskarne = tiskarne.OrderBy(s => s.a4cboboje);
                    break;
                case "A4CBO_asc":
                    tiskarne = tiskarne.OrderByDescending(s => s.a4cboboje);
                    break;
                case "A4BO_desc":
                    tiskarne = tiskarne.OrderBy(s => s.a4barvnooboje);
                    break;
                case "A4BO_asc":
                    tiskarne = tiskarne.OrderByDescending(s => s.a4barvnooboje);
                    break;
                default:
                    tiskarne = tiskarne.OrderBy(s => s.naziv);
                    break;
            }
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return tiskarne.ToPagedList(pageNumber, pageSize);
        }
        public ActionResult Index(string sortOrder, string currentFilter, string strSearch, int? page)
        {
            return View(GetModel(sortOrder, currentFilter, strSearch, page));
        }

        public ActionResult AjaxTabela(string sortOrder, string currentFilter, string strSearch, int? page)
        {
            return PartialView("_Tabela", GetModel(sortOrder, currentFilter, strSearch, page));
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
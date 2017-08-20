using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace Printajmo.Controllers
{
    public class HomeController : Controller
    {
        Models.tiskarneEntities _db;
        public HomeController()
        {
           if(_db == null)
                _db = new Models.tiskarneEntities();
        }
        public PagedList.IPagedList<Models.tiskarne> GetModel(string sortOrder, string currentFilter, string searchString, int? page)
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
        public PagedList.IPagedList<Models.tiskarne> GetModelForTiskarna()
        {
            var tiskarne = from s in _db.tiskarne
                           select s;
            var id = User.Identity.GetUserId();
            tiskarne = tiskarne.Where(s => s.lastnik.Equals(id));
            tiskarne = tiskarne.OrderBy(s => s.naziv);
            int pageSize = 15;
            return tiskarne.ToPagedList(1, pageSize);
        }
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            return View(GetModel(sortOrder, currentFilter, searchString, page));
        }

        public ActionResult AjaxTabela(string sortOrder, string currentFilter, string searchString, int? page)
        {
            return PartialView("_TabelaPartial", GetModel(sortOrder, currentFilter, searchString, page));
        }
        public ActionResult MojeTiskarne(string selected)
        {
            ViewBag.Selected = selected;
            return PartialView("_TabelaPartial", GetModelForTiskarna());
        }
        public ActionResult DeleteEntry(int id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            _db.tiskarne.Remove(_db.tiskarne.Find(id));
            _db.SaveChanges();
            if (User.IsInRole("Tiskarna")) {
                ViewBag.Selected = "true";
                return PartialView("_TabelaPartial", GetModelForTiskarna());
            }
            else
            {
                return PartialView("_TabelaPartial", GetModel(sortOrder, currentFilter, searchString, page));
            }
        }
        public ActionResult EditEntry(int id, string sortOrder, string currentFilter, string searchString, int? page, string naziv, string email, string telefonska, string ulica, string mesto, int postnast, decimal a4cb, decimal a4barvno, decimal a4cboboje, decimal a4boboje)
        {
            var tiskarna = _db.tiskarne.Find(id);
            tiskarna.naziv = naziv;
            tiskarna.email = email;
            tiskarna.telefonska = telefonska;
            tiskarna.ulica = ulica;
            tiskarna.mesto = mesto;
            tiskarna.postnast = postnast;
            tiskarna.a4cb = a4cb;
            tiskarna.a4barvno = a4barvno;
            tiskarna.a4cboboje = a4cboboje;
            tiskarna.a4barvnooboje = a4boboje;
            _db.Entry(tiskarna).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            if (User.IsInRole("Tiskarna"))
            {
                ViewBag.Selected = "true";
                return PartialView("_TabelaPartial", GetModelForTiskarna());
            }
            else
            {
                return PartialView("_TabelaPartial", GetModel(sortOrder, currentFilter, searchString, page));
            }
        }

        public ActionResult AddEntry(string sortOrder, string currentFilter, string searchString, int? page, string naziv, string email, string telefonska, string ulica, string mesto, int postnast, decimal a4cb, decimal a4barvno, decimal a4cboboje, decimal a4boboje)
        {
            var tiskarna = new Printajmo.Models.tiskarne();
            tiskarna.naziv = naziv;
            tiskarna.email = email;
            tiskarna.telefonska = telefonska;
            tiskarna.ulica = ulica;
            tiskarna.mesto = mesto;
            tiskarna.postnast = postnast;
            tiskarna.a4cb = a4cb;
            tiskarna.a4barvno = a4barvno;
            tiskarna.a4cboboje = a4cboboje;
            tiskarna.a4barvnooboje = a4boboje;
            tiskarna.lastnik = User.Identity.GetUserId();
            _db.tiskarne.Add(tiskarna);
            _db.SaveChanges();
            if (User.IsInRole("Tiskarna"))
            {
                ViewBag.Selected = "true";
                return PartialView("_TabelaPartial", GetModelForTiskarna());
            }
            else
            {
                return PartialView("_TabelaPartial", GetModel(sortOrder, currentFilter, searchString, page));
            }
        }

        public ActionResult GetTiskarnaDelete(int id)
        {
            var model = _db.tiskarne.Find(id);
            return PartialView("_DeletePartial", model);
        }

        public ActionResult GetTiskarnaEdit(int id)
        {
            var model = _db.tiskarne.Find(id);
            return PartialView("_EditPartial", model);
        }
        public ActionResult GetTiskarnaDetails(int id)
        {
            var model = _db.tiskarne.Find(id);
            return PartialView("_DetailsPartial", model);
        }
        public ActionResult AddNewTiskarnaView()
        {
            return PartialView("_AddNewPartial");
        }
        public JsonResult getTiskarneJson() {
            String str= "{\"tiskarne\": [";
            foreach (var item in _db.tiskarne) {
                if(item.latitude != null || item.longitude != null)
                    str += "{\"lat\":\"" + item.latitude + "\",\"lng\":\"" + item.longitude+"\",\"id\":\""+item.idtiskarne+"\",\"title\":\""+item.naziv+"\"},";
            }
            str = str.TrimEnd(',') + "]}";
            return Json(str, JsonRequestBehavior.AllowGet);
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
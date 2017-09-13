using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Printajmo.Controllers
{
    public class AndroidController : Controller
    {
        Models.tiskarneEntities _db;
        public AndroidController()
        {
            if (_db == null)
                _db = new Models.tiskarneEntities();
        }
        public JObject getTiskarneApi()
        {
            //{ "idtiskarne":"1","naziv":"Tisko","a4cb":"4","a4barvno":"6","a4cboboje":"5","ulica":"Smetanova 1 ","postnast":"2000","mesto":"Maribor","email":"tisko@gmail.om","telefonska":"03568745","dodatno":"nekaj!","longitude":"15.725525","latitude":"45.899694"},

            var tiskarne = _db.tiskarne.Select(e => new
            {
                idtiskarne = e.idtiskarne,
                naziv = e.naziv,
                a4cb = e.a4cb,
                a4barvno = e.a4barvno,
                a4cboboje = e.a4cboboje,
                ulica = e.ulica,
                postnast = e.postnast,
                mesto = e.mesto,
                email = e.email,
                telefonska = e.telefonska,
                dodatno = e.dodatno,
                longitude = e.longitude,
                latitude = e.latitude
            }).ToList();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(tiskarne);
            output = "{\"tiskarne\":" + output + ",\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
    }
}

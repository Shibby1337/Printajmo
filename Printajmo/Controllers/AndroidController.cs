using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public JObject getAllEntries()
        {
            var tiskarne = _db.tiskarne.Select(e => new
            {
                idtiskarne = e.idtiskarne,
                rating = e.rating,
                naziv = e.naziv,
                ulica = e.ulica,
                postnast = e.postnast,
                mesto = e.mesto,
                a4cb = e.a4cb,
                a4barvno = e.a4barvno,
                a4cboboje = e.a4cboboje,
                a4barvnooboje = e.a4barvnooboje,
                dodatno = e.dodatno,
                email = e.email,
                telefonska = e.telefonska,
                longitude = e.longitude,
                latitude = e.latitude,
                lastnik = e.lastnik,
                voteNumber = e.voteNumber
            }).ToList();

            var komentarji = _db.Comments.Select(k => new
            {
                idComment = k.idComment,
                comment = k.comment,
                time = k.time,
                parentID = k.parrentID,
                name = k.name,
                img = k.img,
                idTiskarna = k.idTiskarna,
                idUser = k.idUser
            }).ToList();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string tisk = jss.Serialize(tiskarne);
            string kom = jss.Serialize(komentarji);
            String output = "{\"tiskarne\":" + tisk + ", \"komentarji\":"+ kom + ", \"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
        public JObject getTiskarneList()
        {
            var tiskarne = _db.tiskarne.Select(e => new
            {
                idtiskarne = e.idtiskarne,
                rating = e.rating,
                naziv = e.naziv,
                naslov = e.ulica + ", " + e.postnast + " " + e.mesto,
                a4cb = e.a4cb,
                a4barvno = e.a4barvno,
                longitude = e.longitude,
                latitude = e.latitude
            }).ToList();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(tiskarne);
            output = "{\"tiskarne\":" + output + ",\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
        public JObject getMojeTiskarneList(string idUser)
        {
            var tiskarne = _db.tiskarne
                .Where(e => e.lastnik.Equals(idUser))
                .Select(e => new
                {
                    idtiskarne = e.idtiskarne,
                    rating = e.rating,
                    naziv = e.naziv,
                    naslov = e.ulica + ", " + e.postnast + " " + e.mesto,
                    a4cb = e.a4cb,
                    a4barvno = e.a4barvno,
                    longitude = e.longitude,
                    latitude = e.latitude
                }).ToList();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(tiskarne);
            output = "{\"tiskarne\":" + output + ",\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
        public JObject getTiskarneDetails(int idTiskarna)
        {
            var tiskarne = _db.tiskarne
                .Where(e => e.idtiskarne.Equals(idTiskarna))
                .Select(e => new
                {
                    e.idtiskarne,
                    e.rating,
                    e.naziv,
                    e.ulica,
                    e.postnast,
                    e.mesto,
                    e.a4cb,
                    e.a4barvno,
                    e.a4cboboje,
                    e.a4barvnooboje,
                    e.email,
                    e.telefonska,
                    e.dodatno
                }).ToList();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(tiskarne);
            output = "{\"tiskarna\":" + output + ",\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
        public JObject updateTiskarna(string idtiskarne, string naziv, string ulica, string postnast, string mesto, string email, string telefonska, string a4cb, string a4barvno, string a4cboboje, string a4barvnooboje, string dodatno)
        {
            var tiskarna = _db.tiskarne.Find(Convert.ToInt32(idtiskarne));
            tiskarna.naziv = naziv;
            tiskarna.email = email;
            tiskarna.telefonska = telefonska;
            tiskarna.ulica = ulica;
            tiskarna.mesto = mesto;
            tiskarna.postnast = Convert.ToInt32(postnast);
            tiskarna.a4cb = Convert.ToDecimal(a4cb);
            tiskarna.a4barvno = Convert.ToDecimal(a4barvno);
            tiskarna.a4cboboje = Convert.ToDecimal(a4cboboje);
            tiskarna.a4barvnooboje = Convert.ToDecimal(a4barvnooboje);
            tiskarna.dodatno = dodatno;
            _db.Entry(tiskarna).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            String output = "{\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
        public JObject addTiskarna(string naziv, string ulica, string postnast, string mesto, string email, string telefonska, string a4cb, string a4barvno, string a4cboboje, string a4barvnooboje, string dodatno, string idUser)
        {
            var tiskarna = new Printajmo.Models.tiskarne();
            tiskarna.naziv = naziv;
            tiskarna.email = email;
            tiskarna.telefonska = telefonska;
            tiskarna.ulica = ulica;
            tiskarna.mesto = mesto;
            tiskarna.postnast = Convert.ToInt32(postnast);
            tiskarna.a4cb = Convert.ToDecimal(a4cb);
            tiskarna.a4barvno = Convert.ToDecimal(a4barvno);
            tiskarna.a4cboboje = Convert.ToDecimal(a4cboboje);
            tiskarna.a4barvnooboje = Convert.ToDecimal(a4barvnooboje);
            tiskarna.lastnik = idUser;
            tiskarna.dodatno = dodatno;
            tiskarna.latitude = 15.412162;
            tiskarna.longitude = 45.412162;

            _db.tiskarne.Add(tiskarna);
            _db.SaveChanges();

            String output = "{\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
        public JObject deleteTiskarna(string idtiskarne)
        {
            _db.tiskarne.Remove(_db.tiskarne.Find(Convert.ToInt32(idtiskarne)));
            _db.SaveChanges();

            String output = "{\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
        public JObject rate(string tiskarnaID, string ratingValue, string idUser)
        {
            int idTiskarne = Convert.ToInt32(tiskarnaID);
            int ratingVal = (int)Convert.ToDouble(ratingValue);

            var rating = new Printajmo.Models.ratings();
            var ratingDB = from s in _db.ratings
                           where s.idUser == idUser && s.idTiskarna == idTiskarne
                           select s;
            if (ratingDB.Any())
            {
                rating = _db.ratings.Find(ratingDB.First().idRating);
                rating.rating = ratingVal;
                _db.Entry(rating).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                rating.idTiskarna = idTiskarne;
                rating.idUser = idUser;
                rating.rating = ratingVal;
                _db.ratings.Add(rating);

            }
            _db.SaveChanges();
            //adding to tiskarne
            var allRatings = from s in _db.ratings
                             where s.idTiskarna == idTiskarne
                             select s.rating;

            if (allRatings.Any())
            {
                var sum = 0;
                foreach (var value in allRatings)
                {
                    sum += value;
                }
                double result = (double)sum / (double)allRatings.Count();
                var tiskarna = _db.tiskarne.Find(idTiskarne);
                tiskarna.rating = (decimal)result;
                tiskarna.voteNumber = allRatings.Count();
                _db.Entry(tiskarna).State = System.Data.Entity.EntityState.Modified;
            }

            _db.SaveChanges();

            String output = "{\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
        public JObject comment(string idtiskarne, string comment, string name, string time, string idUser, string parentID, string img)
        {
            int idTiskarne = Convert.ToInt32(idtiskarne);
            DateTime dateTime =  DateTime.ParseExact(time, "yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
            int parentIDint = Convert.ToInt32(parentID);

            var commentObj = new Printajmo.Models.Comments();
            commentObj.idTiskarna = idTiskarne;
            commentObj.comment = comment;
            commentObj.name = name;
            commentObj.time = dateTime;
            commentObj.idUser = idUser;
            commentObj.parrentID = parentIDint;
            commentObj.img = img;

            _db.Comments.Add(commentObj);
            _db.SaveChanges();

            String output = "{\"success\":1}";
            JObject json = JObject.Parse(output);
            return json;
        }
    }
}

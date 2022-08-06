using E_Commerce.Models;
using E_Commerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

         public CartViewModel cartList;

        public ActionResult Index()
        {
            var categ = db.Category.ToList();
            var banner = db.Banner.ToList();
            var _cards = new HomeViewmodel()
            {
                Categories = categ,
                Banners=banner

            };
            return View(_cards);
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


       [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var itm = db.Cards.Include(c=>c.Category).Single(g=>g.Id==id);
            ViewBag.Name = itm.Category.Name;
            if (itm == null)
            {
                return HttpNotFound();
            }
            return View(itm);
        }



        public ActionResult Cart()
        {
            var itm = Session["Cart"] as List<CardModel>;
            if (itm == null)
                ViewBag.Citems = 0;
            else
                ViewBag.Citems = itm.Count;
            if (itm == null)
            {
                ViewBag.Citems = 0;
                return View("EmptyCart");
                
            }



            return View(itm);
        }




        public ActionResult CartAdd(int? id)
        {
            var itm = db.Cards.SingleOrDefault(d => d.Id == id);

            if (Session["cart"] == null)
            {
                List<CardModel> li = new List<CardModel>();

                li.Add(itm);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                ViewBag.Citems = li.Count();


                Session["count"] = 1;


            }
            else
            {
                List<CardModel> li = (List<CardModel>)Session["cart"];
                li.Add(itm);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                ViewBag.Citems = li.Count();

                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("Index", "Home");


            
        }


        public ActionResult CartRem(int? id)
        {
            

            List<CardModel> li = (List<CardModel>)Session["cart"];
            foreach (var item in li)
            {
                if (item.Id == id)
                {
                    li.Remove(item);
                    break;
                }
            }
        
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
           
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult CardByCateg(int? id)
        {
           
            var itm = db.Cards.Where(c=>c.Category.Id==id).ToList();
            var catg = db.Category.Find(id);
            ViewBag.Categ = catg.Name;
            var allcateg = db.Category.ToList();
            var vm = new HomeViewmodel
            {
                Cards = itm,
                Categories=allcateg
            };
            return View("CDetails", vm);
        }


        public PartialViewResult NavCateg()
        {
            var itm = db.Category.ToList();
            return PartialView("_CategoryPartial",itm);
        }


        [HttpPost]
        public  ActionResult Contact(ContactUS con)
        {
            var mail = new MailMessage();
            mail.To.Clear();
            mail.To.Add("takedocument@gmail.com");
                  mail.From = new MailAddress("takedocument@gmail.com");
            mail.Subject = con.Subject;
            mail.Body = con.Message + "\n from (:)=>" + con.Email;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new  System.Net.NetworkCredential("takedocument@gmail.com", "Sulaiman#1");
           
            smtp.EnableSsl = true;
            smtp.Send(mail);
            ModelState.Clear();

            return View();
        }

    }
}
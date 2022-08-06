using System.Net;
using E_Commerce.Models;
using E_Commerce.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace E_Commerce.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        ApplicationDbContext db;
        
        public AdminController()
        {
            db = new ApplicationDbContext();

           
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
      
        
        
        
        #region Carding Actions
        public ActionResult Index()
        {
            var cardItems = db.Cards.Include(f=>f.Category).ToList();
            var banners = db.Banner.ToList();
            var categ = db.Category.ToList();
            var CB = new HomeViewmodel()
            {
                Cards = cardItems,
                Banners = banners,
                Categories = categ
            };
            
            return View(CB);
        }


        public ActionResult create()
        {
            var VM = new CardViewModel
            {
               CardModel=new CardModel(),
                Categories = db.Category.ToList()
            };
            return View(VM);
        }


        [HttpPost]
        public ActionResult create(HttpPostedFileBase file, CardViewModel cd)
        {
            string fileName = Path.GetFileName(file.FileName);
            string _fileName =DateTime.Now.ToString("yymmssfff")+fileName;
            string Extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/"), _fileName);
            cd.CardModel.ItemImage = "~/Images/" + _fileName;
            var categ = db.Category.Find(cd.Category_id);
            cd.CardModel.Category = categ;
            
            if(Extension.ToLower()==".jpg" || Extension.ToLower()==".jpeg" || Extension.ToLower() == ".png")
            {
                db.Cards.Add(cd.CardModel);
                if (file.ContentLength <= 1000000)
                {
                    
                    file.SaveAs(path);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.msg = "Added successfully";

                }
                else
                {
                    ViewBag.msg = "File too large";
                }
            }

            var VM = new CardViewModel
            {
                CardModel = new CardModel(),
                Categories = db.Category.ToList()
            };
            return View(VM);
           
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var itm = db.Cards.SingleOrDefault(d => d.Id == id);
            if(itm==null)
            {
                return HttpNotFound();
            }
            var vm = new CardViewModel
            { 
                 CardModel = itm,
                 Categories = db.Category.ToList()

            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, CardViewModel cd)
        {
           
            var dbcontent = db.Cards.SingleOrDefault(g => g.Id == cd.CardModel.Id);
         
            var prev_Image = dbcontent.ItemImage;
            var prev_Image_Path = Server.MapPath(prev_Image);
            System.IO.File.Delete(prev_Image_Path);


            string fileName = Path.GetFileName(file.FileName);
            string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
            string Extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/"), _fileName);
            cd.CardModel.ItemImage = "~/Images/" + _fileName;
            
            
            var categ = db.Category.Find(cd.Category_id);
            cd.CardModel.Category = categ;
            
            
            dbcontent.ItemName = cd.CardModel.ItemName;
            dbcontent.ItemImage = cd.CardModel.ItemImage;
            dbcontent.Description = cd.CardModel.Description;
            dbcontent.Price = cd.CardModel.Price;
            dbcontent.Category = cd.CardModel.Category;
            dbcontent.ProductCode = cd.CardModel.ProductCode;

                
                if (file.ContentLength <= 1000000)
                {
                    file.SaveAs(path);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.msg = "Added successfully";
                }
                else
                {
                    ViewBag.msg = "File too large";
                }
            
            var vm = new CardViewModel
            {
              
                Categories = db.Category.ToList()

            };
            return View(vm);
        }
   
        public ActionResult Delete(int? id)
        {
            var delcontent = db.Cards.Include(c=>c.Category).SingleOrDefault(c=>c.Id==id);
            var path= Server.MapPath( delcontent.ItemImage);
            if (delcontent == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                db.Cards.Remove(delcontent);
                System.IO.File.Delete(path);


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        #endregion


        #region Banner Actions
        public ActionResult createBan()
        {
            return View();
        }
     
        
        [HttpPost]
        public ActionResult createBan(HttpPostedFileBase file, Banners bn)
        {
            string fileName = Path.GetFileName(file.FileName);
            string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
            string path = Path.Combine(Server.MapPath("~/Images/"), _fileName);
            bn.Ban_Img = "~/Images/" + _fileName;
            db.Banner.Add(bn);
            if (file.ContentLength <= 1000000)
            {

                file.SaveAs(path);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.msg = "Added successfully";

            }
            else
            {
                ViewBag.msg = "File too large";
            }
            return View();
        }


        public ActionResult EditBan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var itm = db.Banner.Find(id);
            if (itm == null)
            {
                return HttpNotFound();
            }
            return View("EditBan", itm);
        }
       
        
        [HttpPost]
        public ActionResult EditBan(HttpPostedFileBase file, Banners bn)
        {
            var dbcontent = db.Banner.SingleOrDefault(g => g.id == bn.id);
            string fileName = Path.GetFileName(file.FileName);
            string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
            string path = Path.Combine(Server.MapPath("~/Images/"), _fileName);
            bn.Ban_Img = "~/Images/" + _fileName;

            dbcontent.RelatedName = bn.RelatedName;
            dbcontent.Description = bn.Description;
            dbcontent.Ban_Img = bn.Ban_Img;

            if (file.ContentLength <= 1000000)
            {

                file.SaveAs(path);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.msg = "Added successfully";

            }
            else
            {
                ViewBag.msg = "File too large";
            }
            return View();
        }
    
        
        public ActionResult DeleteBan(int? id)
        {
            var delcontent = db.Banner.Find(id);
            var path = Server.MapPath(delcontent.Ban_Img);
            if (delcontent == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {

                db.Banner.Remove(delcontent);
                System.IO.File.Delete(path);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        #endregion


        #region Category Actions
        public ActionResult CreateCateg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCateg(HttpPostedFileBase file,Category categ)
        {

            if (!ModelState.IsValid)
            {
                return View(categ);
            }
            else
            {
                string fileName = Path.GetFileName(file.FileName);
                string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
                string path = Path.Combine(Server.MapPath("~/Images/"), _fileName);
                categ.Image = "~/Images/" + _fileName;
                file.SaveAs(path);
                db.Category.Add(categ);
                db.SaveChanges();
                ModelState.Clear();
            }
            return View();
        }


        public ActionResult EditCateg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbcateg = db.Category.SingleOrDefault(c => c.Id == id);
            if (dbcateg == null)
            {
                return HttpNotFound();
            }
            return View(dbcateg);
        }
    
        
        [HttpPost]
        public ActionResult EditCateg(HttpPostedFileBase file,Category categ)
        {
            string fileName = Path.GetFileName(file.FileName);
            string _fileName = DateTime.Now.ToString("yymmssfff") + fileName;
            string Extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Images/"), _fileName);
         
            var dbcateg = db.Category.Find(categ.Id);
            dbcateg.Name = categ.Name;
            dbcateg.Image ="~/Images/" + _fileName;
            file.SaveAs(path);
            db.SaveChanges();
            ModelState.Clear();

            return View();
        }
    
        
        public ActionResult DeleteCateg(int? id)
        {
            var delcontent = db.Category.Find(id);
            var path = Server.MapPath(delcontent.Image);
            db.Category.Remove(delcontent);
            System.IO.File.Delete(path);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        #endregion

    }
}
using FirstWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstWebApplication.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: User


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllUser()
        {
            var users = _db.Users.ToList();
            return Json(new { data = users }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserPartial(int? Id)
        {
            var user = _db.Users.Find(Id) ?? new User();
            return PartialView("_CreateOrUpdateUserPartial", user);
        }

        //public ActionResult CreateOrUpdateUser(User user)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _db.Users.Add(user);
        //            _db.SaveChanges();
        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //    {
        //        // Lặp qua các lỗi chi tiết và in chúng ra để tìm hiểu vấn đề
        //        foreach (var validationErrors in ex.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //            }
        //        }

        //        // Xử lý ngoại lệ hoặc trả về thông báo lỗi
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}


        public ActionResult CreateOrUpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                if ( user.Id > 0 )
                {
                    
                    _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    _db.Users.Add(user);
                }

                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id) 
        {
            try
            {
                var user = _db.Users.Find(Id);
                _db.Users.Remove(user);
                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
            
        }





    }
}
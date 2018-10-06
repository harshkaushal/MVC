using MVCCodeFirst.Common.ViewModels;
using MVCCodeFirst.Data.Contract;
using MVCCodeFirst.Data.Repositories.UnitOfWork;
using MVCCodeFirst.Manager;
using MVCCodeFirst.Manager.WebManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCodeFirst.Web.Controllers
{
    public class HomeController : Controller
    {
        public IUnitOfWork uow;
        private readonly CommonManager commonManager;
        private readonly UserInfoManager userInfoManager;

        public HomeController()
        {
            uow = new UnitOfWork();
            commonManager = new CommonManager(uow);
            userInfoManager = new UserInfoManager(uow);
        }
        public ActionResult Index()
        {
            var model = userInfoManager.GetAllUsers();
            return View(model);
        }

        public ActionResult AddUpdateUser(int? uid)
        {
            UserInfoModel model = userInfoManager.GetUserByID(uid ?? 0);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUpdateUser(UserInfoModel model)
        {
            if (ModelState.IsValid)
                userInfoManager.SaveUpdateUser(model);
            return RedirectToAction("Index");
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
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp1.Controllers
{
    public class BaseController : Controller
    {
        public IEmailService EmailService { get; set; }
        public IRepository Repository { get; set; }
        
        public BaseController(IEmailService emailService, IRepository repository)
        {
            EmailService = emailService;
            Repository = repository;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "IOC Example";
            return View();
        }
    }
}
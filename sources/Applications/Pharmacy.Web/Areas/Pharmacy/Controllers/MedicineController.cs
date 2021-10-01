using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Web.Areas.Pharmacy.Controllers
{
    public class MedicineController : Controller
    {
        // GET: Pharmacy/Medicine
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create() 
        {
            return View();
        }
    }
}
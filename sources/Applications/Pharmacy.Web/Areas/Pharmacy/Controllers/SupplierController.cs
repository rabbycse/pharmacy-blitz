using Pharmacy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy.Web.Areas.Pharmacy.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Pharmacy/Supplier

        PharmacyEntities _ctx = new PharmacyEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllSuppliers()
        {
            var suppliers = _ctx.View_GetAllSupplier.ToList();
            var data = (from sp in suppliers
                        select new
                        {
                            sp.Id,
                            sp.Name,
                            sp.ShortName,
                            sp.Mobile
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveSupplier(IEnumerable<Supplier> supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //supplier.CreatedAt = DateTime.Now;
                    //_ctx.Suppliers.Add(supplier);
                    //_ctx.SaveChanges();
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpGet]
        public JsonResult GetSupplierById(int id)
        {
            try
            {
                var supplier = _ctx.View_GetAllSupplier.Find(id);
                var data = new
                {
                    supplier.Id,
                    supplier.Name,
                    supplier.ShortName,
                    supplier.Mobile,
                    supplier.IsActive
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateSupplier(Supplier supplier)
        {
            try
            {
                var existingSupplier = _ctx.Suppliers.Find(supplier.Id);
                if (existingSupplier != null)
                {
                    existingSupplier.Name = supplier.Name;
                    existingSupplier.ShortName = supplier.ShortName;
                    existingSupplier.Mobile = supplier.Mobile;
                    existingSupplier.IsActive = supplier.IsActive;
                    existingSupplier.EditedAt = DateTime.Now;
                    _ctx.SaveChanges();
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
using DigitalAzariBDC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalAzariBDC.Controllers
{
    public class BrandController : Controller
    {
        DigitalAzariEntities dbEntities = new DigitalAzariEntities();
        public ActionResult Index(string Createmessage, string Deletemessage, string updatemessage)
        {
            try
            {
                ViewBag.Createmessage = Createmessage;
                ViewBag.Deletemessage = Deletemessage;
                ViewBag.updatemessage = updatemessage;
                ViewBag.brandlist = dbEntities.tblBrands.ToList();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "Oops there is something wrong." });
            }

        }
        [HttpPost]
        public ActionResult Create(tblBrand brand)
        {
            try
            {
                if (brand.BrandID > 0)
                {
                    using (var context = new DigitalAzariEntities())
                    {
                        context.Entry(brand).State = EntityState.Modified;
                        context.SaveChanges();
                        return RedirectToAction("Index", new { updatemessage = "Brand has been updated successfully." });
                    }
                }
                else
                {
                    using (var context = new DigitalAzariEntities())
                    {
                        brand.CreatedDate = DateTime.Now;
                        context.tblBrands.Add(brand);
                        context.SaveChanges();
                        return RedirectToAction("Index", new { Createmessage = "Brand has been added successfully." });
                    }
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "Oops there is something wrong." });
            }

        }
        [HttpPost]
        public ActionResult Delete([Bind(Include = "BrandID")] tblBrand brand)
        {
            try
            {
                using (var context = new DigitalAzariEntities())
                {
                    var br = context.tblBrands.First(x => x.BrandID == brand.BrandID);
                    context.tblBrands.Remove(br);
                    context.SaveChanges();
                    return RedirectToAction("Index", new { Deletemessage = "Brand has been deleted successfully." });
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "Oops there is something wrong." });
            }

        }
    }
}
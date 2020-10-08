using SPA_BOOKING_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Dynamic;
using System.Runtime.Remoting.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;

namespace SPA_BOOKING_SYSTEM.Controllers
{

    public class HomeController : Controller
    {

        [HttpPost]
        public ActionResult ServiceList()
        {
            return View();
        }

        [Authorize]
        public ActionResult DisplayServiceList()
        {
            return View("ServiceList");
        }
        public ActionResult DisplayServices(string Gender,string Service)
        {
            
            ServicesEntities1 context = new ServicesEntities1();
            if (Gender == "Male")
            {
                if(Service == "Facial")
                {
                    return View("DisplayServices1",context.Table_Facial_Men.ToList());
                }
                else if(Service == "Hair Cut")
                {
                    return View("DisplayServices2", context.Table_Haircut_Men.ToList());
                    
                }
                else if (Service == "Waxing")
                {
                    return View("DisplayServices13", context.Table_Wax_Male.ToList());

                }
                else if (Service == "Threading")
                {
                    return View("DisplayServices14", context.Table_Threading_Male.ToList());

                }
                else if (Service == "Massaging")
                {
                    return View("DisplayServices3", context.Table_Massage.ToList());
                }
                else if (Service == "Manicure")
                {
                    return View("DisplayServices4", context.Table_Manicure_Male.ToList());
                }
                else 
                {
                    return View("DisplayServices5", context.Table_Pedi_Men.ToList());
                }
            }
            else
            {

                if (Service == "Facial")
                {
                    return View("DisplayServices6", context.Table_Facial.ToList());
                }
                else if (Service == "Hair Cut")
                {
                    return View("DisplayServices7", context.Table_Haircut_Female.ToList());
                }
                else if (Service == "Waxing")
                {
                    return View("DisplayServices8", context.Table_Wax.ToList());
                }
                else if (Service == "Threading")
                {
                    return View("DisplayServices9", context.Table_Threading.ToList());
                }
                else if (Service == "Massaging")
                {
                    return View("DisplayServices10", context.Table_Massage.ToList());
                }
                else if (Service == "Manicure")
                {
                    return View("DisplayServices11", context.Table_Manicure_Female.ToList());
                }
                else 
                {
                    return View("DisplayServices12", context.Table_Pedicure.ToList());
                }
            }           
        }
        [Authorize]
        public ActionResult CartList(string name)
        {
            cartEntities1 context = new cartEntities1();
            string userID = User.Identity.GetUserId();
            Table_Cart Item = new Table_Cart { SERVICE_NAME = name, USER_ID = userID };
            context.Table_Cart.Add(Item);
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }            
            return Content("Successfully Added");
        }

        [HttpGet]
        public ActionResult Slot()
        {
            cartEntities1 cartEntities1 = new cartEntities1();
            string uid = User.Identity.GetUserId();
            List<Table_Cart> table_Carts = cartEntities1.Table_Cart.Where(entity => entity.USER_ID == uid).ToList();
            if (table_Carts == null)
            {
                //string DoNotProceedIfTheresNoData = Request.UrlReferrer.ToString();
                return Content("There is no data");
            }
            return View();
        }
       
        public ActionResult Slot(Table_Slot table_Slot)
        {
            slotFixing obj = new slotFixing(); 
            List<Table_Slot> result = obj.Table_Slot.Where(x => x.LOCATION == table_Slot.LOCATION &&  x.DAY == table_Slot.DAY).ToList();
            int count = 0;
            foreach(var item in result)
            {
                if(item.SLOT == table_Slot.SLOT)
                {
                    count++;
                }
            }
            if (count > 5)
            {
                return RedirectToAction("Slot");
            }
            else
            {
                
                obj.Table_Slot.Add(table_Slot);
                obj.SaveChanges();
                string uid = User.Identity.GetUserId();
                cartEntities1 cartEntities = new cartEntities1();
                List<Table_Cart> table_Carts = cartEntities.Table_Cart.Where(x => x.USER_ID == uid).ToList();
                string ListOfServicesOpted = "";
                
                foreach(var item in table_Carts)
                {
                    ListOfServicesOpted = ListOfServicesOpted + item.SERVICE_NAME + " ; ";

                    
                }
                foreach(var item in table_Carts)
                {
                    cartEntities.Table_Cart.Remove(item);

                }
                cartEntities.SaveChanges();
                
                return Content("Thank you for booking! Your Appointment has been booked successfully under the service \n" + ListOfServicesOpted  );
            }
            
        }

        [HttpGet]
        public ActionResult Location()
        {
            
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The webpage will provide you a complete glance about the Spa.";

            return View();
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Any issues kindly contact 9876543210";

            return View();
        }
    }
}
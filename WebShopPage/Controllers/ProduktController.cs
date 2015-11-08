using WebShopPage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopPage.BLL;

namespace WebShopPage.Controllers
{
    public class ProduktController : Controller
    {
        public ActionResult Index()
        {
            LoggetInn(); //setter opp variabler for bruk i viewet
            var bll = new ProduktBLL();
            return View(bll.HentProduktListe());
        }


        public bool LoggetInn()
        {
            if (Session["Innlogget"] == null)
            {
                Session["Innlogget"] = false;
                ViewBag.Innlogget = false;
                Session["Id"] = 0;
            }
            else
            {
                if (!(bool)Session["Innlogget"])
                {
                    ViewBag.Innlogget = false;
                }
                else
                {
                    ViewBag.Innlogget = true;
                }
            }

            return ViewBag.Innlogget;
        }
    }


}

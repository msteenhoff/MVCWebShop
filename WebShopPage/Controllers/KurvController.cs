using WebShopPage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopPage.BLL;

namespace WebShopPage.Controllers
{
    public class KurvController : Controller
    {
        public ActionResult VisKurv()
        {
            if (LoggetInn())
            {
                var sessionKundeId = (int)Session["id"];
                var kundebll = new KundeBLL();
                var viewkurv = kundebll.ViewKurvFraId(sessionKundeId);

                if (viewkurv != null)
                {
                    return View(viewkurv);
                }
            }

            //følgende skjer om ikke logget inn eller om det feiler ved å hente kunde og kurv fra db
            ViewBag.Melding = "Feil ved DB-tilknytning. Er du logget inn? Prøv igjen.";
            return RedirectToAction("Index", "Produkt");
        }

        public ActionResult LeggiKurv(int id)
        {
            if (LoggetInn())
            {
                var sessionKundeId = (int)Session["id"];
                var kurvbll = new KurvBLL();
                var leggiKurv = kurvbll.LeggiKurv(sessionKundeId, id);

                if (leggiKurv)
                {
                    return RedirectToAction("VisKurv");
                }
            }
            ViewBag.Melding = "Kunne ikke legge i handlekurv. Er du logget inn?";
            return RedirectToAction("Index", "Produkter");
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


        public ActionResult SjekkUt()
        {
            if (LoggetInn())
            {
                var sessionKundeId = (int)Session["id"];
                var kundebll = new KundeBLL();
                var viewkurv = kundebll.ViewKurvFraId(sessionKundeId);
                
                if (viewkurv != null)
                {
                    ViewBag.kurv = viewkurv;

                    var vk = kundebll.HentViewKunde(sessionKundeId);

                    if (vk !=null)
                        return View(vk);     
                }
            }
            return RedirectToAction("Index", "Produkt");
        }

        public ActionResult Bestill()
        {

            if (LoggetInn())
            {
                var sessionKundeId = (int)Session["id"];
                var kurvbll = new KurvBLL();
                var ordreOK = kurvbll.PlasserOrdre(sessionKundeId);

                if (ordreOK == true)
                {
                    ViewBag.Melding = "Bestillingen er mottatt!";
                    ViewBag.OK = (bool)true;
                }
                else
                {
                    ViewBag.OK = (bool)false;
                    ViewBag.Melding = "Noe gikk galt. Prøv igjen";
                }
            }
            return View();
        }
    }
}

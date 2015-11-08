using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopPage.Controllers;
using System.Data.Entity;
using WebShopPage.Model;
using WebShopPage.BLL;

namespace WebShopPage.Controllers
{
    
    public class KundeController : Controller
    {
        private IAdminBLL _aBLL;
        private IKundeBLL _kBLL;

        public KundeController()
        {
            _aBLL = new AdminBLL();
            _kBLL = new KundeBLL();
        }
        public KundeController(IAdminBLL stub, IKundeBLL kstub)
        {
            _aBLL = stub;
            _kBLL = kstub;
        }
        public ActionResult Registrer()
        {
            LoggetInn();
            return View();
        }

        [HttpPost]
        public ActionResult Registrer(ViewKunde k)
        {
            LoggetInn();
            var ok = _kBLL.RegistrerKunde(k);

            if (ok)
            {
                ViewBag.Melding = "Konto opprettet! Logg inn med den nye kontoen din her:";
                return RedirectToAction("LoggInn", "Kunde");
            }

            else
            {
                ViewBag.Melding = "Feil ved lagring. Har du fyllt ut alle feltene?";
                return View();

            } 
        }

            
        public ActionResult LoggInn()
        {
            LoggetInn();
            return View();
        }

        [HttpPost]
        public ActionResult LoggInn(LoggInnKunde loggInnKunde)
        {
            var Id = _kBLL.FinnKundeLoggInn(loggInnKunde); // metoden returnerer kundeId - 0 hvis kunden ikke ble funnet
            

            if ( Id == 0 )
            {
                Session["Innlogget"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = true;
                Session["Innlogget"] = true;
                var kunde = _kBLL.FinnKunde(Id);
                Session["LoggInnNavn"] = kunde.fornavn + " " + kunde.etternavn;
                Session["id"] = Id;
            }

            return RedirectToAction("Index", "Produkt");
        }

        public bool LoggetInn()
        {
            if (Session["Innlogget"] == null )
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

        public ActionResult Kundeside()
        {
            if (LoggetInn())
            {
                var Id = (int)Session["Id"];
                ViewKunde vk = _kBLL.HentViewKunde(Id);
                return View(vk);
         
            } //if loggetInn
            else
            {
                ViewBag.Melding = "Du er ikke logget inn.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult EndreKundeside()
        {
            if (LoggetInn())
            {
                var id = (int)Session["Id"];
                var vk = _kBLL.HentViewKunde(id);

                if (vk != null)
                {
                    return View(vk);
                }
                else
                {
                    ViewBag.Melding = "<p class='danger'>Feil ved db-kall.</p>";
                    return RedirectToAction("Index", "Produkt");
                }
            } //if loggetInn
            else
            {
                ViewBag.Melding = "<p class='danger'>Du er ikke logget inn.</p>";
                return RedirectToAction("Index", "Produkt");
            }
        }


        [HttpPost]
        public ActionResult EndreKundeside(ViewKunde vk)
        {
            if (LoggetInn()) //innloggingsjekk
            {
                var id = (int)Session["Id"];
                var ok = _kBLL.EndreKunde(vk, id);

                if (ok)
                {
                    ViewBag.Melding = "Feil ved lagring. Kontakt admin.";
                    return RedirectToAction("Kundeside");
                }
                else
                {
                    ViewBag.Melding = "Feil ved lagring. Kontakt admin.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Melding = "Du er ikke logget inn.";
                return RedirectToAction("Index");
            }

        }

        public ActionResult LoggUt()
        {
            Session["Innlogget"] = false;
            Session["LoggInnNavn"] = "";
            Session["Id"] = 0;
            ViewBag.Innlogget = false;
            ViewBag.Melding = "Du er logget ut. Velkommen tilbake!";

            return RedirectToAction("Index", "Produkt");
        }

        public ActionResult Ordrehistorikk() //vis kundens ordrehistorikk
        {
            if (LoggetInn())
            {
                var id = (int)Session["Id"];
                var ordre = _aBLL.HentOrdre(id);                 
                return View(ordre);
            }
            else
            {
                ViewBag.Melding = "Du er ikke logget inn. Prøv igjen.";
                return RedirectToAction("Index", "Produkt");
            }
        }

        public ActionResult OrdreDetaljer(int id)
        {
            if (LoggetInn())
            {
                var ordre = _aBLL.HentEnkeltOrdre(id);
                ViewBag.Sum = ordre.ordresum; //ordresum til viewet

                return View(ordre.linjer);
            }
            else
            {
                ViewBag.Melding = "Du er ikke logget inn.";
                return RedirectToAction("Index", "Produkt");
            }
        }
    }
}

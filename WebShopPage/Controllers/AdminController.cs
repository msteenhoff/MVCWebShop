using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopPage.BLL;
using WebShopPage.Model;

namespace WebShopPage.Controllers
{
    public class AdminController : Controller
    {
        private IAdminBLL _aBLL;
        private IKundeBLL _kBLL;
        private IProduktBLL _pBLL;

        public AdminController()
        {
            _aBLL = new AdminBLL();
            _kBLL = new KundeBLL();
            _pBLL = new ProduktBLL();
        }
        public AdminController(IAdminBLL stub, IKundeBLL kstub, IProduktBLL pstub)
        {
            _aBLL = stub;
            _kBLL = kstub;
            _pBLL = pstub;
        }

        public ActionResult Index()
        {
            //Admin();
            if (AdminLoggetInn())
            {
                // ViewBag.Feil = TempData["Feil"];
                ViewBag.Melding = TempData["Melding"];
                return View();
            }
            else
                return RedirectToAction("Index", "Produkt");
        } 



        /* --------- Kunder --------- */
        public ActionResult Kundeliste()
        {
            if (AdminLoggetInn())
            {
                ViewBag.Feil = TempData["Feil"];
                ViewBag.Melding = TempData["Melding"];
                return View(_kBLL.HentKundeListe());
            }
            else
            {
                return RedirectToAction("Index", "Produkt");
            }

        }
        public ActionResult NyKunde()
        {
            if (AdminLoggetInn())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Produkt");
            }
        }

        [HttpPost]
        public ActionResult NyKunde(ViewKunde k)
        {
            if (AdminLoggetInn())
            {
                var ok = _kBLL.RegistrerKunde(k);
                if (ok)
                {
                    TempData["Melding"] = "Konto opprettet for: " + k.fornavn + k.etternavn;
                    return RedirectToAction("Kundeliste");
                }
                else
                {
                    TempData["Feil"] = "Feil ved lagring. Har du fyllt ut alle feltene?";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Produkt");
            }
        }

        public ActionResult EndreKunde(int id)
        {
            if (AdminLoggetInn())
            {
                var vk = _kBLL.HentViewKunde(id);

                if (vk != null)
                {
                    return View(vk);
                }
                else
                {
                    TempData["Feil"] = "Feil ved db-kall.";
                    return RedirectToAction("Kundeliste");
                }
            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EndreKunde(ViewKunde k)
        {
            if (AdminLoggetInn()) //innloggingsjekk
            {
                var ok = _aBLL.EndreKunde(k);

                if (ok)
                {
                    TempData["Feil"] = "Kundenr. " + k.kundeId + ": " + k.fornavn + " " + k.etternavn + " ble lagret med endringer";
                    return RedirectToAction("Kundeliste");
                }
                else
                {
                    TempData["Feil"] = "Feil ved lagring. Kontakt admin.";
                    return RedirectToAction("Kundeliste");
                }
            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }

        }

        public ActionResult SlettKunde(int id)
        { // viser kunde som skal slettes for bekreftelse
            if (AdminLoggetInn()) //innloggingsjekk
            {
                var vk = _kBLL.HentViewKunde(id);
                return View(vk);
            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }
        }

        [HttpPost, ActionName("SlettKunde")]
        public ActionResult BekreftSlettKunde(int id)
        {
            if (AdminLoggetInn())
            {
                var slettkundeOK = _kBLL.SlettKunde(id);
                if (slettkundeOK)
                {
                    TempData["Melding"] = "Kunden ble slettet";
                }
                else
                {
                    TempData["Feil"] = "Noe gikk galt ved sletting. Prøv igjen.";
                }

                return RedirectToAction("Kundeliste");
            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }

        }



        /* --------- Produkter --------- */
        public ActionResult Produktliste()
        {
            if (AdminLoggetInn()) //innloggingsjekk
            {
                ViewBag.Feil = TempData["Feil"];
                ViewBag.Melding = TempData["Melding"];
                return View(_pBLL.HentProduktListeView());
            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn som administrator. Logg inn her.";
                return RedirectToAction("LoggInn");
            }
        }

        public ActionResult NyttProdukt()
        {
            if (AdminLoggetInn()) //innloggingsjekk
            {
                return View();
            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn aom administrator. Logg inn her.";
                return RedirectToAction("LoggInn");
            }
        }
        [HttpPost]
        public ActionResult NyttProdukt(ViewProdukt vp)
        {
            if (AdminLoggetInn()) //innloggingsjekk
            {
                var ok = _pBLL.NyttProdukt(vp);
                if (ok)
                {
                    TempData["Melding"] = vp.Navn + " lagret i produktdatabasen!";
                    return RedirectToAction("Produktliste");
                }
                else
                {
                    TempData["Feil"] = "Feil ved lagring i produktdatabasen. Prøv igjen eller kontakt systemadministrator.";
                    return RedirectToAction("Produktliste");
                }
            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn aom administrator. Logg inn her.";
                return RedirectToAction("LoggInn");
            }   
        }

        public ActionResult EndreProdukt(int id)
        {
            if (AdminLoggetInn())
            {
                return View(_pBLL.HentViewProdukt(id));

            } //if loggetInn
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }
        }

        [HttpPost]
        public ActionResult EndreProdukt(ViewProdukt vp)
        {
            if (AdminLoggetInn()) //innloggingsjekk
            {
                var ok = _pBLL.EndreProdukt(vp);

                if (ok)
                {
                    ViewBag.Melding = "Produktnr. " + vp.produktId + ": " + vp.Navn + " ble lagret med endringer";
                    return RedirectToAction("Produktliste");
                }
                else
                {
                    TempData["Feil"] = "Feil ved lagring. Kontakt admin.";
                    return RedirectToAction("Produktliste");
                }
            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }

        }

        public ActionResult SlettProdukt(int id)
        {
            if (AdminLoggetInn())
            {
                return View(_pBLL.HentViewProdukt(id));

            } //if loggetInn
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }
        }


        [HttpPost, ActionName("SlettProdukt")]
        public ActionResult BekreftSlettProdukt(int id)
        {
            if (AdminLoggetInn())
            {
                var slettproduktOK = _pBLL.SlettProdukt(id);
                if (slettproduktOK)
                {
                    TempData["Melding"] = "Produktet ble slettet.";
                }
                else
                {
                    TempData["Feil"] = "Noe gikk galt ved sletting. Prøv igjen.";
                }

                return RedirectToAction("Produktliste");
            } 
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }

        }



        /* --------- Ordre --------- */ 
        public ActionResult Ordreliste()
        {
            if (AdminLoggetInn())
            {
                ViewBag.Feil = TempData["Feil"];
                ViewBag.Melding = TempData["Melding"];
                var ordre = _aBLL.HentOrdre();
                if (ordre == null)
                {
                    ViewBag.DbFeil = "Feil i Ordredatabasen. Kontakt administrator.";
                    ordre = new List<OrdreView>();
                }
                else if (ordre.Count < 1)
                {
                    ordre = new List<OrdreView>();
                    ViewBag.DbInfo = "Ordrelisten er tom. Kanskje mer markedsføring hjelper?";
                }

                return View(ordre);

            }
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }

        }

        public ActionResult Ordredetaljer(int id)
        {
            if (AdminLoggetInn())
            {
                var ordreliste = _aBLL.HentEnkeltOrdre(id);

                ViewBag.Sum = ordreliste.ordresum; //ordresum til viewet

                return View(ordreliste.linjer);

            } //if loggetInn
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }

        }

        public ActionResult SlettOrdre(int id)
        {
            if (AdminLoggetInn())
            {
                return View(_aBLL.HentEnkeltOrdre(id));

            } //if loggetInn
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }
        }


        [HttpPost, ActionName("SlettOrdre")]
        public ActionResult BekreftSlettOrdre(int id)
        {
            if (AdminLoggetInn())
            {
                var slettproduktOK = _aBLL.SlettOrdre(id);
                if (slettproduktOK)
                {
                    TempData["Melding"] = "Ordren ble slettet";
                }
                else
                {
                    TempData["Feil"] = "Noe gikk galt ved sletting av ordre. Prøv igjen.";
                }

                return RedirectToAction("Ordreliste");

            } //if loggetInn
            else
            {
                TempData["Feil"] = "Du er ikke logget inn.";
                return RedirectToAction("LoggInn");
            }

        }



        /* --------- Admin --------- */
        public ActionResult LoggInn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoggInn(LoggInnAdmin input)
        {
            var ok = _aBLL.LoggInn(input);

            if (ok)
            {
                Session["Admin"] = true;
                RedirectToAction("Index");
            }
            else
            {
                Session["Admin"] = false;
                ViewBag.Feil = "Innlogging feilet. Har du tastet riktig brukernavn og passord?";
            }
            return View();
        }

        public ActionResult LoggUt()
        {
            Session["Admin"] = false;
            return RedirectToAction("Index", "Produkt");
        }

        public bool AdminLoggetInn()
        {
            //Session["Admin"] = (bool)true; 

            if (Session["Admin"] == null)
            {
                Session["Admin"] = false;
                return false;
            }
            else if ((bool)Session["Admin"])
            {
                return true;              
            }
            else if (!(bool)Session["Admin"])
                return false;

            return false;
        }
    }
}

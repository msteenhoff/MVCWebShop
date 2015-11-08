using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebShopPage.BLL;
using WebShopPage.Controllers;
using WebShopPage.DAL;
using WebShopPage.Model;

namespace EnhetsTest
{
    [TestClass]
    public class AdminControllerTest
    {
        private TestControllerBuilder _session;
        private AdminController _ctrl;

        public AdminControllerTest()
        {
            _session = new TestControllerBuilder();
            _ctrl = new AdminController(new AdminBLL(new AdminDALStub(), new KundeDALStub()), new KundeBLL(new KundeDALStub()), new ProduktBLL(new ProduktDALStub()));
            // initierer sessionvariablen for innlogging for alle metoder
            // feilaktig innlogging blir testet ved testing av innloggingsmetoden
            _session.InitializeController(_ctrl);
            _ctrl.Session["Admin"] = (bool)true;
        }
        

        [TestMethod]
        public void VisIndex()
        {
            var SessionMock = new TestControllerBuilder();
            var ctrl = new AdminController(new AdminBLL(new AdminDALStub(), new KundeDALStub()), new KundeBLL(new KundeDALStub()), new ProduktBLL(new ProduktDALStub()));
            SessionMock.InitializeController(ctrl);
            ctrl.Session["Admin"] = (bool)true;
            // Act
            var actionResult = (ViewResult)ctrl.Index();
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void VisKundeliste()
        {
            var forventetResultat = new List<Kunde>();

            var p = new byte[8];
            p[0] = new byte();

            var k = new Kunde()
            {
                kundeId = 1,
                fornavn = "ftest",
                etternavn = "etest",
                adresse = "hjemme 3",
                mail = "mail@mail.com",
                passord = p,
                postnummer = "1234",
                tlf = 12341234
            };
            forventetResultat.Add(k);
            forventetResultat.Add(k);
            forventetResultat.Add(k);

            // Act
            var actionResult = (ViewResult)_ctrl.Kundeliste();
            var resultat = (List<Kunde>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].kundeId, resultat[i].kundeId);
                Assert.AreEqual(forventetResultat[i].fornavn, resultat[i].fornavn);
                Assert.AreEqual(forventetResultat[i].etternavn, resultat[i].etternavn);
                Assert.AreEqual(forventetResultat[i].adresse, resultat[i].adresse);
                Assert.AreEqual(forventetResultat[i].mail, resultat[i].mail);
                Assert.AreEqual(forventetResultat[i].passord[0], resultat[i].passord[0]);
                Assert.AreEqual(forventetResultat[i].postnummer, resultat[i].postnummer);
                Assert.AreEqual(forventetResultat[i].tlf, resultat[i].tlf);
            }
        }

        [TestMethod]
        public void VisNyKundeView()
        {
            // Act
            var actionResult = (ViewResult)_ctrl.NyKunde();
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LagNyKundeView()
        {
            var vk = new ViewKunde()
            {
                kundeId = 1,
                fornavn = "ftest",
                etternavn = "etest",
                adresse = "hjemme 3",
                mail = "mail@mail.com",
                passord = "p",
                postnummer = "1234",
                tlf = 12341234
            };

            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.NyKunde(vk);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Kundeliste"); 
        }
        [TestMethod]
        public void VisEndreKundeView()
        {
            // Act
            var actionResult = (ViewResult)_ctrl.EndreKunde(1);
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreKundeOK()
        {
            var vk = new ViewKunde()
            {
                kundeId = 1,
                fornavn = "ftest",
                etternavn = "etest",
                adresse = "hjemme 3",
                mail = "mail@mail.com",
                passord = "p",
                postnummer = "1234",
                tlf = 12341234
            };

            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.EndreKunde(vk);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Kundeliste");
        }

        [TestMethod]
        public void EndreKundeFeil()
        {
            var vk = new ViewKunde()
            {
                kundeId = 1,
                fornavn = "",
                etternavn = "etest",
                adresse = "hjemme 3",
                mail = "",
                passord = "p",
                postnummer = "1234",
                tlf = 12341234
            };

            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.EndreKunde(vk);
            // Assert
            Assert.AreEqual(_ctrl.TempData["Feil"], "Feil ved lagring. Kontakt admin.");
            Assert.AreEqual(actionResult.RouteValues["action"], "Kundeliste");
        }
        [TestMethod]
        public void VisSlettKundeView()
        {
            // Act
            var actionResult = (ViewResult)_ctrl.SlettKunde(1);
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void SlettKundeOK()
        {
            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.BekreftSlettKunde(1);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Kundeliste");
        }
        [TestMethod]
        public void SlettKundeFeil()
        {
            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.BekreftSlettKunde(0);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Kundeliste");
            Assert.AreEqual("Noe gikk galt ved sletting. Prøv igjen.", _ctrl.TempData["Feil"]);          
        }

        /* --------   Produkter --------- */
        [TestMethod]
        public void VisProduktliste()
        {
            var forventetResultat = new List<ViewProdukt>();
            var p = new ViewProdukt()
            {
                Navn = "test",
                produktId = 1,
                Beskrivelse = "testbeskrivelse",
                Pris = 25
            };
            forventetResultat.Add(p);
            forventetResultat.Add(p);
            forventetResultat.Add(p);

            // Act
            var actionResult = (ViewResult)_ctrl.Produktliste();
            var resultat = (List<ViewProdukt>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].Navn, resultat[i].Navn);
                Assert.AreEqual(forventetResultat[i].produktId, resultat[i].produktId);
                Assert.AreEqual(forventetResultat[i].Beskrivelse, resultat[i].Beskrivelse);
                Assert.AreEqual(forventetResultat[i].Pris, resultat[i].Pris);
            }
        }

        [TestMethod]
        public void VisNyProduktView()
        {
            // Act
            var actionResult = (ViewResult)_ctrl.NyttProdukt();
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void LagNyttProduktOK()
        {
            var p = new ViewProdukt()
            {
                Navn = "test",
                produktId = 1,
                Beskrivelse = "testbeskrivelse",
                Pris = 25
            };

            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.NyttProdukt(p);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Produktliste");
        }
        [TestMethod]
        public void LagNyttProduktFeil()
        {
            var p = new ViewProdukt()
            {
                Navn = "",
                produktId = 1,
                Beskrivelse = "",
                Pris = 25
            };

            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.NyttProdukt(p);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Produktliste");
            Assert.AreEqual("Feil ved lagring i produktdatabasen. Prøv igjen eller kontakt systemadministrator.", _ctrl.TempData["Feil"]);
        }
        [TestMethod]
        public void VisEndreProduktView()
        {
            // Act
            var actionResult = (ViewResult)_ctrl.EndreProdukt(1);
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void EndreProduktOK()
        {
            var p = new ViewProdukt()
            {
                Navn = "test",
                produktId = 1,
                Beskrivelse = "testbeskrivelse",
                Pris = 25
            };

            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.EndreProdukt(p);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Produktliste");
        }

        [TestMethod]
        public void EndreProduktFeil()
        {
            var p = new ViewProdukt()
            {
                Navn = "",
                produktId = 1,
                Beskrivelse = "testbeskrivelse",
                Pris = 25
            };

            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.EndreProdukt(p);
            // Assert
            Assert.AreEqual(_ctrl.TempData["Feil"], "Feil ved lagring. Kontakt admin.");
            Assert.AreEqual(actionResult.RouteValues["action"], "Produktliste");
        }

        [TestMethod]
        public void VisSlettProduktView()
        {
            // Act
            var actionResult = (ViewResult)_ctrl.SlettKunde(1);
            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void SlettProduktOK()
        {
            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.BekreftSlettProdukt(1);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Produktliste");
            Assert.AreEqual("Produktet ble slettet.", _ctrl.TempData["Melding"]);
        }
        [TestMethod]
        public void SlettProduktFeil()
        {
            // Act
            var actionResult = (RedirectToRouteResult)_ctrl.BekreftSlettProdukt(0);
            // Assert
            Assert.AreEqual(actionResult.RouteValues["action"], "Produktliste");
            Assert.AreEqual("Noe gikk galt ved sletting. Prøv igjen.", _ctrl.TempData["Feil"]);
        }


        /* --------- Ordre --------- */
        [TestMethod]
        public void VisOrdreliste()
        {
            var forventetResultat = new List<OrdreView>();
            var o = new OrdreView()
            {
                dato = DateTime.Today,
                kundenavn = "Test Navn",
                ordreId = 1,
                sum = 25
            };

            forventetResultat.Add(o);
            forventetResultat.Add(o);
            forventetResultat.Add(o);

            // Act
            var actionResult = (ViewResult)_ctrl.Ordreliste();
            var resultat = (List<OrdreView>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].dato, resultat[i].dato);
                Assert.AreEqual(forventetResultat[i].kundenavn, resultat[i].kundenavn);
                Assert.AreEqual(forventetResultat[i].ordreId, resultat[i].ordreId);
                Assert.AreEqual(forventetResultat[i].sum, resultat[i].sum);
            }
        }
        [TestMethod]
        public void VisEnkeltOrdre()
        {
            var forventet = new OrdreListeView()
            {
                linjer = new List<OrdreLinjeView>(),
                ordresum = 25
            };
            var ol = new OrdreLinjeView()
            {
                antall = 1,
                navn = "test",
                pris = 25
            };

            forventet.linjer.Add(ol);
            forventet.linjer.Add(ol);
            forventet.linjer.Add(ol);

            var actionResult = (ViewResult)_ctrl.Ordredetaljer(1);
            var resultat = (List<OrdreLinjeView>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventet.linjer[i].navn, resultat[i].navn);
                Assert.AreEqual(forventet.linjer[i].antall, resultat[i].antall);
                Assert.AreEqual(forventet.linjer[i].pris, resultat[i].pris);
            }

        }


        /* --------- Admin --------- */
        [TestMethod]
        public void VisLoggInnView()
        {
            var actionResult = (ViewResult)_ctrl.LoggInn();
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void LoggInnOK()
        {
            var input = new LoggInnAdmin()
            {
                mail = "m",
                passord = "p"
            };

            var actionResult = (RedirectToRouteResult)_ctrl.LoggInn(input);

            Assert.AreEqual(actionResult.RouteValues["action"], "Index");
            Assert.AreEqual(_ctrl.Session["Admin"], true);
        }
        [TestMethod]
        public void LoggInnFeil()
        {
            var input = new LoggInnAdmin();
            input.mail = "";
            input.passord = "p";

            var actionResult = (ViewResult)_ctrl.LoggInn(input);

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(_ctrl.Session["Admin"], false);
        }
        [TestMethod]
        public void AdminLoggetInn_NY()
        {
            _ctrl.Session["Admin"] = null;
            var resultat = _ctrl.AdminLoggetInn();

            Assert.AreEqual(resultat, false);
        }
        [TestMethod]
        public void AdminAlleredeLoggetInn()
        {
            _ctrl.Session["Admin"] = true;
            var resultat = _ctrl.AdminLoggetInn();

            Assert.AreEqual(resultat, true);
        }
        [TestMethod]
        public void AdminIkkeLoggetInn()
        {
            _ctrl.Session["Admin"] = false;
            var resultat = _ctrl.AdminLoggetInn();

            Assert.AreEqual(resultat, false);
        }
    }
}

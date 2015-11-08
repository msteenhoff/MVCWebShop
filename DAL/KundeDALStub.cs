using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public class KundeDALStub : IKundebehandler
    {
        //henter kundekurven basert på kundeId, oppretter ny hvis kurven er null
        public List<KurvProduktView> ViewKurvFraId(int id)
        {
            var list = new List<KurvProduktView>();

            var kp = new KurvProduktView()
            {
                antall = 1,
                pris = 25,
                navn = "test"
            };

            list.Add(kp);
            list.Add(kp);
            list.Add(kp);

            return list;
        }

        // henter kunde-id fra db for innlogging
        public int FinnKundeLoggInn(string mail, byte[] passordDb)
        {
            using (var db = new WebshopContext())
            {
                Kunde dbKunde = db.Kunder.FirstOrDefault
                    (k => k.passord == passordDb && k.mail == mail);
                if (dbKunde == null)
                {
                    return 0;
                }
                else
                {
                    return dbKunde.kundeId;
                }
            }
        }

        public Kunde FinnKunde(int id)
        {
            return new Kunde()
            {
                kundeId = id,
                fornavn = "ftest",
                etternavn = "etest",
                adresse ="hjemme 3",
                kurv = new Handlekurv(),
                mail = "mail@mail.com",
                ordre = new List<Ordre>(),
                passord = new byte[8],
                postnummer = "1234",
                tlf = 12341234
            };

        }

        public List<OrdreLinjeView> LagOrdreLinjeView(List<OrdreLinje> lo)
        {
            var linjeView = new List<OrdreLinjeView>();

            foreach (OrdreLinje linje in lo)
            {
                var lv = new OrdreLinjeView();

                lv.navn = linje.produkt.navn;
                lv.antall = linje.antall;
                lv.pris = linje.produkt.pris;

                linjeView.Add(lv);
            }
            return linjeView;
        }


        /* -------- NYTT I OPPGAVE 2 ---------- */
        public List<Kunde> HentKundeListe()
        {
            var liste = new List<Kunde>();

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
            liste.Add(k);
            liste.Add(k);
            liste.Add(k);
            return liste;
        }

        public bool RegistrerKunde(Kunde k)
        {
            if (k.mail != "")
                return true;
            else
                return false;
        }

        public bool EndreKunde(ViewKunde k, int id)
        {
            if (k.mail != "" && id == 1)
                return true;
            else
                return false;
        }

        public bool EndreKundeAdmin(ViewKunde k)
        {
            if (k.mail == "")
                return false;
            else
                return true;
        }

        public bool EndreKundePassord(AdminEndreKundePassordView k, byte[] p)
        {
            if (k.mail != "" && p != null)
                return true;
            else
                return false;

        }

        public bool SlettKunde(int kundeId)
        {
            if (kundeId != 0)
                return true;
            else
                return false;
        }
    }
}

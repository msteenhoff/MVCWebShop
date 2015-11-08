using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public class AdminDALStub : IAdminDAL
    {
        public List<OrdreView> HentOrdre() //henter alle ordre i db
        {
            var ovlist = new List<OrdreView>();

            var ov = new OrdreView()
            {
                dato = DateTime.Today,
                kundenavn = "Test Navn",
                ordreId = 1,
                sum = 25
            };

            ovlist.Add(ov);
            ovlist.Add(ov);
            ovlist.Add(ov);
            return ovlist;
        }

        public List<OrdreView> OrdreViewFraOrdreListe(List<Ordre> list)
        {
            var ovlist = new List<OrdreView>();
            foreach (var l in list)
            {
                var ov = new OrdreView();
                ov.dato = l.dato;
                ov.kundenavn = l.kunde.fornavn + " " + l.kunde.etternavn;
                ov.ordreId = l.ordreId;
                ov.sum = l.sum;
                ovlist.Add(ov);
            }
            return ovlist;
        }

        public List<Ordre> HentOrdre(int kundeid) //henter ordre for én kunde
        {

            var ordre = new List<Ordre>();

            var o = new Ordre()
            {
                dato = DateTime.Now,
                kunde = new Kunde(),
                ordreId = 1,
                ordrelinjer = new List<OrdreLinje>(),
                sum = 25
            };
            ordre.Add(o);
            ordre.Add(o);
            ordre.Add(o);

            return ordre;

        }

        public OrdreListeView HentEnkeltOrdre(int ordreid) //henter ordre for én kunde
        {

            var ordre = new OrdreListeView()
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

            ordre.linjer.Add(ol);
            ordre.linjer.Add(ol);
            ordre.linjer.Add(ol);

            return ordre;
        }

        public bool LoggInnAdmin(AdminBruker a)
        {
            if (a.mail != "")
            {
                return true;
            }
            else
                return false;

        }

        public bool SlettOrdre(int id)
        {
            if (id == 0)
                return false;
            else
                return true;
        }

    }
}

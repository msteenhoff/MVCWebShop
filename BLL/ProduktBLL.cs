using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopPage.DAL;
using WebShopPage.Model;

namespace WebShopPage.BLL
{
    public class ProduktBLL : IProduktBLL
    {
        private IProduktDAL _pDAL;
        public ProduktBLL()
        {
            _pDAL = new ProduktDAL();
        }
        public ProduktBLL(IProduktDAL pstub)
        {
            _pDAL = pstub;
        }

        public List<ViewProdukt> HentProduktListeView()
        {
            var liste = _pDAL.HentProduktListe();
            var viewliste = new List<ViewProdukt>();

            foreach (var p in liste)
            {
                viewliste.Add(ViewProduktfraProdukt(p));
            }
            return viewliste;
        }

        public List<Produkt> HentProduktListe() //brukes av produktcontroller der viewmodeller for produkter ikke ble implementert i oppgave 1
        {
            var liste = _pDAL.HentProduktListe();
            return liste;
        }
        public ViewProdukt HentViewProdukt(int id)
        {
            var p = _pDAL.HentProdukt(id);
            var vp = ViewProduktfraProdukt(p);
            return vp;
        }

        private ViewProdukt ViewProduktfraProdukt(Produkt p)
        {
            var vp = new ViewProdukt() {
                Navn = p.navn,
                Pris = p.pris,
                Beskrivelse = p.beskrivelse,
                produktId = p.produktId
            };
            return vp;
        }

        public bool NyttProdukt(ViewProdukt p)
        {
            return _pDAL.NyttProdukt(p);
        }

        public bool EndreProdukt(ViewProdukt p)
        {
            var ok = _pDAL.EndreProdukt(p);
            return ok;
        }

        public bool SlettProdukt(int id)
        {
            var ok = _pDAL.SlettProdukt(id);
            return ok;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShopPage.DAL;
using WebShopPage.Model;

namespace WebShopPage.BLL
{
    public class KundeBLL : IKundeBLL
    {
        private IKundebehandler _kDAL;
        public KundeBLL()
        {
            _kDAL = new Kundebehandler();
        }
        public KundeBLL(IKundebehandler kstub)
        {
            _kDAL = kstub;
        }

        //henter kundekurven basert på kundeId, oppretter ny hvis kurven er null
        public List<KurvProduktView> ViewKurvFraId(int id)
        {
            return _kDAL.ViewKurvFraId(id);
        }

        // henter kunde-id fra db for innlogging
        public int FinnKundeLoggInn(LoggInnKunde innKunde)
        {
            var sikkerhet = new Sikkerhet();
            byte[] passordDb = sikkerhet.LagHash(innKunde.passord);
            return _kDAL.FinnKundeLoggInn(innKunde.mail, passordDb);
        }

        public Kunde FinnKunde(int id)
        {
            return _kDAL.FinnKunde(id);
        }

        public List<OrdreLinjeView> LagOrdreLinjeView(List<OrdreLinje> lo)
        {
            return _kDAL.LagOrdreLinjeView(lo);
        }

        public bool RegistrerKunde(ViewKunde k)
        {
            var nyKunde = new Kunde();
            var sikkerhet = new Sikkerhet();
            byte[] hashPassord = sikkerhet.LagHash(k.passord);

            nyKunde.passord = hashPassord;
            nyKunde.mail = k.mail;
            nyKunde.fornavn = k.fornavn;
            nyKunde.etternavn = k.etternavn;
            nyKunde.adresse = k.adresse;
            nyKunde.tlf = k.tlf;
            nyKunde.postnummer = k.postnummer;
            nyKunde.ordre = new List<Ordre>();

            return _kDAL.RegistrerKunde(nyKunde);
        }

        public bool SlettKunde(int kundeId)
        {
            var slettkunde = _kDAL.SlettKunde(kundeId);
            return slettkunde;
        }

        public bool EndreKunde(ViewKunde k, int id)
        {          
            return _kDAL.EndreKunde(k, id);
        }

        public ViewKunde HentViewKunde(int id)
        {
            var k = FinnKunde(id);

            if (k != null)
            {
                var vk = new ViewKunde();
                vk.kundeId = k.kundeId;
                vk.mail = k.mail;
                vk.fornavn = k.fornavn;
                vk.etternavn = k.etternavn;
                vk.adresse = k.adresse;
                vk.postnummer = k.postnummer;
                vk.tlf = k.tlf;

                return vk;
            }
            else
                return null;
        }

        public List<Kunde> HentKundeListe()
        {
            return _kDAL.HentKundeListe();
        }

    }
}
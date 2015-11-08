using System.Collections.Generic;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public interface IKundebehandler
    {
        bool EndreKunde(ViewKunde k, int id);
        bool EndreKundeAdmin(ViewKunde k);
        bool EndreKundePassord(AdminEndreKundePassordView k, byte[] p);
        Kunde FinnKunde(int id);
        int FinnKundeLoggInn(string mail, byte[] passordDb);
        List<Kunde> HentKundeListe();
        List<OrdreLinjeView> LagOrdreLinjeView(List<OrdreLinje> lo);
        bool RegistrerKunde(Kunde k);
        bool SlettKunde(int kundeId);
        List<KurvProduktView> ViewKurvFraId(int id);
    }
}
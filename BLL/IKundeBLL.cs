using System.Collections.Generic;
using WebShopPage.Model;

namespace WebShopPage.BLL
{
    public interface IKundeBLL
    {
        bool EndreKunde(ViewKunde k, int id);
        Kunde FinnKunde(int id);
        int FinnKundeLoggInn(LoggInnKunde innKunde);
        List<Kunde> HentKundeListe();
        ViewKunde HentViewKunde(int id);
        List<OrdreLinjeView> LagOrdreLinjeView(List<OrdreLinje> lo);
        bool RegistrerKunde(ViewKunde k);
        bool SlettKunde(int kundeId);
        List<KurvProduktView> ViewKurvFraId(int id);
    }
}
using System.Collections.Generic;
using WebShopPage.Model;

namespace WebShopPage.BLL
{
    public interface IProduktBLL
    {
        bool EndreProdukt(ViewProdukt p);
        List<Produkt> HentProduktListe();
        List<ViewProdukt> HentProduktListeView();
        ViewProdukt HentViewProdukt(int id);
        bool NyttProdukt(ViewProdukt p);
        bool SlettProdukt(int id);
    }
}
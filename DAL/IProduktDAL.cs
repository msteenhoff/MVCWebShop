using System.Collections.Generic;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public interface IProduktDAL
    {
        bool EndreProdukt(ViewProdukt p);
        Produkt HentProdukt(int id);
        List<Produkt> HentProduktListe();
        bool NyttProdukt(ViewProdukt p);
        bool SlettProdukt(int id);
    }
}
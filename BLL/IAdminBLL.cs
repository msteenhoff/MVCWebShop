using System.Collections.Generic;
using WebShopPage.Model;

namespace WebShopPage.BLL
{
    public interface IAdminBLL
    {
        bool EndreKunde(ViewKunde vk);
        bool EndreKundePassord(AdminEndreKundePassordView ap);
        OrdreListeView HentEnkeltOrdre(int id);
        List<OrdreView> HentOrdre();
        List<OrdreView> HentOrdre(int id);
        bool LoggInn(LoggInnAdmin input);
        bool SlettOrdre(int id);
    }
}
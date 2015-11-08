using System.Collections.Generic;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public interface IAdminDAL
    {
        OrdreListeView HentEnkeltOrdre(int ordreid);
        List<OrdreView> HentOrdre();
        List<Ordre> HentOrdre(int kundeid);
        bool LoggInnAdmin(AdminBruker a);
        List<OrdreView> OrdreViewFraOrdreListe(List<Ordre> ordre);
        bool SlettOrdre(int id);
    }
}
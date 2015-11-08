using System.Collections.Generic;
using WebShopPage.Model;

namespace WebShopPage.BLL
{
    public interface IKurvBLL
    {
        bool LeggiKurv(int kundeId, int produktId);
        bool PlasserOrdre(int kundeId);
        List<KurvProduktView> ViewFraKurvProdukt(List<KurvProdukt> kplist);
    }
}
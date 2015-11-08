namespace WebShopPage.BLL
{
    using WebShopPage.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class KurvBLL : IKurvBLL
    {
        public List<KurvProduktView> ViewFraKurvProdukt(List<KurvProdukt> kplist)
        {
            var kb = new DAL.Kurvbehandler();
            return kb.ViewFraKurvProdukt(kplist);
        }

        public bool LeggiKurv(int kundeId, int produktId)
        {
            var kb = new DAL.Kurvbehandler();
            return kb.LeggiKurv(kundeId, produktId);
        }

        public bool PlasserOrdre(int kundeId)
        {
            var kb = new DAL.Kurvbehandler();
            return kb.PlasserOrdre(kundeId);
        }
    }
}
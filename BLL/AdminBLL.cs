using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopPage.DAL;
using WebShopPage.Model;


namespace WebShopPage.BLL
{
    public class AdminBLL : IAdminBLL
    {
        private IAdminDAL _aDAL;
        private IKundebehandler _kDAL;
        public AdminBLL()
        {
            _aDAL = new AdminDAL();
            _kDAL = new Kundebehandler();
        }
        public AdminBLL(IAdminDAL astub, IKundebehandler kstub)
        {
            _aDAL = astub;
            _kDAL = kstub;
        }
        public bool EndreKunde(ViewKunde vk)
        {
            return _kDAL.EndreKundeAdmin(vk);
        }

        public bool EndreKundePassord(AdminEndreKundePassordView ap)
        {
            var sikker = new Sikkerhet();
            var p = sikker.LagHash(ap.passord);
            return _kDAL.EndreKundePassord(ap, p);
        }

        public List<OrdreView> HentOrdre() //henter alle ordre fra db
        {
            var ordre = _aDAL.HentOrdre();
            return ordre;
        }

        public List<OrdreView> HentOrdre(int id) //henter alle ordre for en kunde
        {
            var ordre = _aDAL.HentOrdre(id);
            return _aDAL.OrdreViewFraOrdreListe(ordre);
        }

        public OrdreListeView HentEnkeltOrdre(int id) //henter linjer og sum for én ordre
        {
            var ordre = _aDAL.HentEnkeltOrdre(id);
            return ordre;
        }

        public bool SlettOrdre(int id)
        {
            return _aDAL.SlettOrdre(id);
        }

        public bool LoggInn(LoggInnAdmin input)
        {
            var sikkerhet = new Sikkerhet();
            var admin = new AdminBruker()
            {
                mail = input.mail,
                passord = sikkerhet.LagHash(input.passord)
            };

            var ok = _aDAL.LoggInnAdmin(admin);
            return ok;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public class AdminDAL : IAdminDAL
    {
        private WebshopContext _db = new WebshopContext();

        public List<OrdreView> HentOrdre() //henter alle ordre i db
        {
            var dummy = new List<OrdreView>();

            using (_db)
            {
                try
                {
                    var ordredb = _db.OrdreDB;

                    var ovlist = new List<OrdreView>();
                    foreach (var l in ordredb)
                    {
                        /*var kunde = _db.Kunder.Find(l.kunde.kundeId);
                        var navn = "";
                        if (kunde != null)
                            navn = kunde.FulltNavn();*/
                        var kunde = "";
                        if (l.kunde != null)
                            kunde = l.kunde.FulltNavn();
                        var ov = new OrdreView();
                        ov.dato = l.dato;
                        ov.kundenavn = l.kunde.kundeId.ToString();
                        ov.ordreId = l.ordreId;
                        ov.sum = l.sum;
                        ovlist.Add(ov);
                    }
                    return ovlist;
                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "HentOrdre");
                    return null;
                }
            }
        }

        public List<OrdreView> OrdreViewFraOrdreListe(List<Ordre> list)
        {
            using (_db) { 
                var ovlist = new List<OrdreView>();
                foreach (var l in list)
                {
                    var ov = new OrdreView();
                    ov.dato = l.dato;
                    ov.kundenavn = l.kunde.fornavn + " " + l.kunde.etternavn;
                    ov.ordreId = l.ordreId;
                    ov.sum = l.sum;
                    ovlist.Add(ov);
                }
                return ovlist;
            }
        }

        /*public OrdreListeView OrdreListeViewFraOrdre(Ordre ordre)
        {
            var ovlist = new OrdreListeView();
            ovlist.linjer = new List<OrdreLinjeView>();
            ovlist.ordresum = 0;

            foreach (var o in ordre.ordrelinjer)
            {
                var ov = new OrdreLinjeView();
                ov.antall = o.antall;
                ov.navn = o.produkt.navn;
                ov.pris = o.produkt.pris;

                ovlist.ordresum += o.linjesum;
                ovlist.linjer.Add(ov);
            }
            return ovlist;
        }*/

        public List<Ordre> HentOrdre(int kundeid) //henter ordre for én kunde
        {
            try
            {
                var kunde = _db.Kunder.Find(kundeid);
                if (kunde.ordre == null)
                    kunde.ordre = new List<Ordre>();

                return kunde.ordre;
            }
            catch (Exception e)
            {
                ExceptionWriter.LoggFeil(e, "HentOrdre");
                return null;
            }
        }

        public OrdreListeView HentEnkeltOrdre(int ordreid) //henter ordre for én kunde
        {
            try
            {
                var ordre = _db.OrdreDB.Find(ordreid);
                if (ordre == null)
                    return null;

                var ovlist = new OrdreListeView();
                ovlist.linjer = new List<OrdreLinjeView>();
                ovlist.ordresum = 0;

                foreach (var o in ordre.ordrelinjer)
                {
                    var ov = new OrdreLinjeView();
                    ov.antall = o.antall;
                    ov.navn = o.produkt.navn;
                    ov.pris = o.produkt.pris;

                    ovlist.ordresum += o.linjesum;
                    ovlist.linjer.Add(ov);
                }
                return ovlist;
            }
            catch (Exception e)
            {
                ExceptionWriter.LoggFeil(e, "HentEnkeltOrdre");
                return null;
            }
        }

        public bool LoggInnAdmin(AdminBruker a)
        {
            try
            {
                AdminBruker dbAdmin = _db.AdminBrukere.FirstOrDefault
                    (k => k.passord == a.passord && k.mail == a.mail);
                if (dbAdmin == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                ExceptionWriter.LoggFeil(e, "LoggInnAdmin");
                return false;
            }
        }

        public bool SlettOrdre(int id)
        {
            try
            {
                var o = _db.OrdreDB.Find(id);
                if (o == null)
                    return false;
                _db.OrdreDB.Remove(o);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                ExceptionWriter.LoggFeil(e, "SlettOrdre");
                return false;
            }
        }

    }
}

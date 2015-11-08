namespace WebShopPage.DAL
{

    using WebShopPage.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Entity;

    public class Kundebehandler : IKundebehandler
    {
        //henter kundekurven basert på kundeId, oppretter ny hvis kurven er null
        public List<KurvProduktView> ViewKurvFraId(int id)
        {
            using (var db = new WebshopContext())
            {
                var kurv = new Handlekurv();

                try
                {
                    Kunde dbKunde = db.Kunder.Find(id);

                    if (dbKunde != null)
                    {
                        if (dbKunde.kurv == null)
                        {
                            dbKunde.kurv = new Handlekurv();
                        }
                        if (dbKunde.kurv.produkter == null)
                        {
                            dbKunde.kurv.produkter = new List<KurvProdukt>();                            
                        }
                        db.SaveChanges();

                        var kpviewlist = new List<KurvProduktView>();

                        foreach (KurvProdukt kp in dbKunde.kurv.produkter)
                        {
                            var kpview = new KurvProduktView();

                            kpview.navn = kp.produkt.navn;
                            kpview.antall = kp.antall;
                            kpview.pris = kp.produkt.pris;

                            kpviewlist.Add(kpview);
                        }

                        return kpviewlist;
                    }
                    else
                        return null;
                }
                catch
                {
                    return null;
                }

            }
        }

        /*public static Handlekurv HentKurvFraId(int id)
        {
            using (var db = new WebshopContext())
            {
                var kurv = new Handlekurv();

                try
                {
                    Kunde dbKunde = db.Kunder.Find(id);

                    if (dbKunde != null)
                    {
                        if (dbKunde.kurv == null)
                        {
                            dbKunde.kurv = new Handlekurv();
                        }
                        if (dbKunde.kurv.produkter == null)
                        {
                            dbKunde.kurv.produkter = new List<KurvProdukt>();
                        }
                        db.SaveChanges();

                        var kpviewlist = new List<KurvProduktView>();

                        foreach (KurvProdukt kp in dbKunde.kurv.produkter)
                        {
                            var kpview = new KurvProduktView();

                            kpview.navn = kp.produkt.navn;
                            kpview.antall = kp.antall;
                            kpview.pris = kp.produkt.pris;

                            kpviewlist.Add(kpview);
                        }

                        return kpviewlist;
                    }
                    else
                        return null;
                }
                catch
                {
                    return null;
                }

            }
        }*/

        // henter kunde-id fra db for innlogging
        public int FinnKundeLoggInn( string mail, byte[] passordDb)
        {
            using (var db = new WebshopContext())
            {
                Kunde dbKunde = db.Kunder.FirstOrDefault
                    (k => k.passord == passordDb && k.mail == mail);
                if (dbKunde == null)
                {
                    return 0;
                }
                else
                {
                    return dbKunde.kundeId;
                }
            }
        }

        public Kunde FinnKunde(int id)
        {
            using (var db = new WebshopContext())
            {
                Kunde dbKunde = db.Kunder.FirstOrDefault
                    (k => k.kundeId == id);

                if (dbKunde != null)
                    return dbKunde;
                else
                    return null;
            }

        }


        public List<OrdreLinjeView> LagOrdreLinjeView(List<OrdreLinje> lo)
        {
            var linjeView = new List<OrdreLinjeView>();

            foreach(OrdreLinje linje in lo)
            {
                var lv = new OrdreLinjeView();

                lv.navn = linje.produkt.navn;
                lv.antall = linje.antall;
                lv.pris = linje.produkt.pris;

                linjeView.Add(lv);
            }
            return linjeView;
        }


        /* -------- NYTT I OPPGAVE 2 ---------- */
        public List<Kunde> HentKundeListe()
       {
            var dummy = new List<Kunde>();

            using (var db = new WebshopContext())
            {
                try
                {
                    var kunder = db.Kunder.ToList();
                    return kunder;
                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "HentKundeListe");
                    return dummy;
                }
            }
       }

        public bool RegistrerKunde(Kunde k)
        {
            using (var db = new WebshopContext())
            {
                try
                {
                    db.Kunder.Add(k);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "RegistrerKunde");
                    return false;
                }
            }
        }

        public bool EndreKunde(ViewKunde k, int id)
        {
            using (var db = new WebshopContext())
            {
                try
                {
                    var dbKunde = db.Kunder.Find(id);

                    dbKunde.fornavn = k.fornavn;
                    dbKunde.etternavn = k.etternavn;
                    dbKunde.adresse = k.adresse;
                    dbKunde.tlf = k.tlf;
                    dbKunde.postnummer = k.postnummer;

                    db.Entry(dbKunde).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();

                    return true;

                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "EndreKunde");
                    return false;
                }
            }

        }

        public bool EndreKundeAdmin(ViewKunde k)
        {
            using (var db = new WebshopContext())
            {
                try
                {
                    var dbKunde = db.Kunder.Find(k.kundeId);

                    dbKunde.mail = k.mail;
                    dbKunde.fornavn = k.fornavn;
                    dbKunde.etternavn = k.etternavn;
                    dbKunde.adresse = k.adresse;
                    dbKunde.tlf = k.tlf;
                    dbKunde.postnummer = k.postnummer;

                    db.Entry(dbKunde).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;

                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "RegistrerKunde");
                    return false;
                }
            }

        }

        public bool EndreKundePassord(AdminEndreKundePassordView k, byte[] p)
        {
            using (var db = new WebshopContext())
            {
                try
                {
                    var dbKunde = db.Kunder.Find(k.kundeId);

                    dbKunde.mail = k.mail;
                    dbKunde.passord = p;

                    db.Entry(dbKunde).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();

                    return true;

                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "EndreKundePassord");
                    return false;
                }
            }

        }

        /*public bool EndreKunde(int id, ViewKunde vk)
        {
            using (var db = new WebshopContext())
            {
                try
                {
                    var dbKunde = db.Kunder.Find(id);

                    dbKunde.mail = vk.mail;
                    dbKunde.fornavn = vk.fornavn;
                    dbKunde.etternavn = vk.etternavn;
                    dbKunde.adresse = vk.adresse;
                    dbKunde.tlf = vk.tlf;
                    dbKunde.postnummer = vk.postnummer;

                    db.Entry(dbKunde).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }*/

        public bool SlettKunde(int kundeId)
        {
            using (var db = new WebshopContext())
            {
                try
                {
                    var kunde = db.Kunder.Find(kundeId);
                    db.Kunder.Remove(kunde);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "SlettKunde");
                    return false;
                }
            }
        }
    }
}
namespace WebShopPage.DAL
{
    using WebShopPage.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class Kurvbehandler
    {
        public List<KurvProduktView> ViewFraKurvProdukt(List<KurvProdukt> kplist)
        {
            var kpviewlist = new List<KurvProduktView>();

            foreach (KurvProdukt kp in kplist )
            {
                var kpview = new KurvProduktView();

                kpview.navn = kp.produkt.navn;
                kpview.antall = kp.antall;
                kpview.pris = kp.produkt.pris;
            }

            return kpviewlist;
        }

        public bool LeggiKurv(int kundeId, int produktId)
        {
            using (var db = new WebshopContext())
            {
                var kunde = db.Kunder.Find(kundeId);
                var p1 = db.Produkter.Find(produktId);

                var kurvprodukter = new List<KurvProdukt>();
                var kp = new KurvProdukt();

                //hvis kunde eller produkt ikke finnes i database
                if (p1 == null || kunde == null)
                {
                    return false;
                }

                //oppretter kurv hvis den er null
                if (kunde.kurv == null)
                {
                    kunde.kurv = new Handlekurv();
                }

                //sjekk om handlekurven er tom
                if (kunde.kurv.produkter == null)
                {
                    kp = new KurvProdukt()
                    {
                        antall = 1,
                        produkt = p1,
                        produktId = p1.produktId,
                        kurvId = kunde.kurv.kurvId
                    };

                    db.KurvProdukter.Add(kp);
                }
                else //kurven er ikke tom
                {
                    // sjekk om varen allerede ligger i kurven
                    kp = db.KurvProdukter.SingleOrDefault(
                        kurvP => kurvP.produkt.produktId == p1.produktId
                        && kurvP.kurvId == kunde.kurv.kurvId);

                    if (kp == null) //varen er ikke i kurven
                    {
                        kp = new KurvProdukt
                        {
                            antall = 1,
                            produkt = p1,
                            produktId = p1.produktId,
                            kurvId = kunde.kurv.kurvId
                        };

                        db.KurvProdukter.Add(kp);
                    }
                    else //varen er i kurven - øker antall
                    {
                        kp.antall++;
                    }
                }

                db.SaveChanges();

                return true;

            }
        }

        public bool PlasserOrdre(int kundeId)
        {
            try
            {
                using (var db = new WebshopContext())
                {
                    var dbkunde = db.Kunder.Find(kundeId);

                    if (dbkunde == null || dbkunde.kurv == null || dbkunde.kurv.produkter == null || dbkunde.kurv.produkter.Count == 0)
                    {
                        return false;
                    }
                    if (dbkunde.ordre == null)
                    {
                        dbkunde.ordre = new List<Ordre>();
                    }

                    var nyOrdre = new Ordre()
                    {
                        dato = System.DateTime.Now,
                        kunde = dbkunde,
                        ordrelinjer = new List<OrdreLinje>()
                    };

                    var kurvP = dbkunde.kurv.produkter;
                    var tempsum = 0;

                    foreach (KurvProdukt kp in kurvP)
                    {
                        var ol = new OrdreLinje()
                        {
                            antall = kp.antall,
                            produkt = kp.produkt,
                            linjesum = kp.antall * kp.produkt.pris
                        };

                        //db.Produkter.Find(kp.produktId).lager -= kp.antall;
                        tempsum += (kp.antall * kp.produkt.pris);
                        nyOrdre.ordrelinjer.Add(ol);
                    }
                    nyOrdre.sum = tempsum;
                    dbkunde.kurv = null;
                    dbkunde.ordre.Add(nyOrdre);                  
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                ExceptionWriter.LoggFeil(e, "PlasserOrdre()");
                return false;
            }

            
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public class ProduktDAL : IProduktDAL
    {
        private WebshopContext _db = new WebshopContext();

        public Produkt HentProdukt(int id)
        {
            try
            {
               var p = _db.Produkter.Find(id);
                if (p == null)
                {
                    return null;
                }
                else
                    return p;
            }
            catch(Exception e)
            {
                ExceptionWriter.LoggFeil(e, "HentProdukt()");
                return null;
            }
        }

        public List<Produkt> HentProduktListe()
        {
            var dummy = new List<Produkt>();

            using (_db)
            {
                try
                {
                    var produkter = _db.Produkter.ToList();
                    return produkter;
                }
                catch
                {
                    return dummy;
                }
            }
        }

        public bool NyttProdukt(ViewProdukt p)
        {
            using (_db)
            {
                try
                {
                    if (_db.Produkter.Find(p.produktId) != null)
                        return false; //produktet eksisterer allerede i db

                    var nyttP = new Produkt();

                    nyttP.navn = p.Navn;
                    nyttP.beskrivelse = p.Beskrivelse;
                    nyttP.pris = p.Pris;

                    _db.Produkter.Add(nyttP);
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "NyttProdukt");
                    return false;
                }
            }
        }

        public bool EndreProdukt(ViewProdukt p)
        {
            using (_db)
            {
                try
                {
                    if (_db.Produkter.Find(p.produktId) == null)
                        return false; //produktet eksisterer ikke i db

                    var produkt = HentProdukt(p.produktId);
                    produkt.navn = p.Navn;
                    produkt.pris = p.Pris;
                    produkt.beskrivelse = p.Beskrivelse;

                    _db.Entry(produkt).State = EntityState.Modified;
                    _db.SaveChanges();

                    return true;
                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "EndreProdukt");
                    return false;
                }
            }
        }

        /*public bool EndreProdukt(ViewProdukt p)
        {
            using (_db)
            {
                try
                {
                    var dbProdukt = _db.Produkter.Find(p.produktId);
                    if (dbProdukt == null)
                        return false;

                    dbProdukt.navn = p.Navn;
                    dbProdukt.pris = p.Pris;
                    dbProdukt.beskrivelse = p.Beskrivelse;

                    _db.Entry(dbProdukt).State = EntityState.Modified;
                    _db.SaveChanges();

                    return true;

                }
                catch (Exception e)
                {
                    ExceptionWriter.LoggFeil(e, "EndreProdukt");
                    return false;
                }
            }

        }*/

        public bool SlettProdukt(int id)
        {
            try
            {
                var p = _db.Produkter.Find(id);
                if (p == null)
                    return false;
                _db.Produkter.Remove(p);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                ExceptionWriter.LoggFeil(e, "SlettProdukt");
                return false;
            }
        }
    }
}

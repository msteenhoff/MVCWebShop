using System;
using System.Collections.Generic;
using System.Data.Entity;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public class ProduktDALStub : IProduktDAL
    {
        public Produkt HentProdukt(int id)
        {
            return new Produkt()
            {
                navn = "test",
                produktId = id,
                beskrivelse = "testbeskrivelse",
                pris = 25
            };
        }

        public List<Produkt> HentProduktListe()
        {
            var dummy = new List<Produkt>();

            var p = new Produkt()
            {
                navn = "test",
                produktId = 1,
                beskrivelse = "testbeskrivelse",
                pris = 25
            };
            dummy.Add(p);
            dummy.Add(p);
            dummy.Add(p);
            return dummy;
        }

        public bool NyttProdukt(ViewProdukt p)
        {
            if (p.Navn != "")
                return true;
            else
                return false;
        }

        public bool EndreProdukt(ViewProdukt p)
        {
            if (p.Navn != "")
                return true;
            else
                return false;
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
            if (id == 0)
                return false;
            else
                return true;
        }
    }
}

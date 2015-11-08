namespace WebShopPage.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Kunde
    {
        [Key]
        public int kundeId { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string adresse { get; set; }
        public int tlf { get; set; }
        public virtual List<Ordre> ordre { get; set; }
        public string postnummer { get; set; }
        public byte[] passord { get; set; }
        public string mail { get; set; }

        public virtual Handlekurv kurv { get; set; }


        public string FulltNavn()
        {
            var navn = this.fornavn + " " + this.etternavn;
            return navn;

        }

        /*public static bool FinnKundeDb(LoggInnKunde innKunde)
        {
            using (var db = new WebshopContext())
            {
                byte[] passordDb = Sikkerhet.LagHash(innKunde.passord);
                Kunde dbKunde = db.Kunder.FirstOrDefault
                    (k => k.passord == passordDb && k.mail == innKunde.mail);
                if (dbKunde == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }*/
    }

    public class Poststed
    {
        [Key]
        public int postnummer { get; set; }
        public string sted { get; set; }
        public virtual List<Kunde> Kunder { get; set; }

        public Poststed()
        {
            Kunder = new List<Kunde>();
        }

    }

}
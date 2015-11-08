using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebShopPage.Model;

namespace WebShopPage.DAL
{
    public class WebshopInit : DropCreateDatabaseIfModelChanges<WebshopContext>
    {
        protected override void Seed(WebshopContext context)
        {
            var produkter = new List<Produkt>()
            {
                new Produkt
                {
                    navn = "Banan",
                    beskrivelse = "Gul. Litt rar form.",
                    pris = 12,

                },
                new Produkt
                {
                    navn = "Eple",
                    beskrivelse = "Grønne med stilk",
                    pris = 25,
                },
                new Produkt
                {
                    navn = "Pære",
                    beskrivelse = "Ikke til opplysning.",
                    pris = 7,
                }
            };
            context.Kunder.Add(new Kunde()
            {
                    fornavn = "Magnus",
                    etternavn = "Steenhoff",
                    adresse = "Hjemmeveien 3",
                    postnummer = "1234",
                    mail = "m@mail.no",
                    passord = LagHash("p"),
                    tlf = 12341234
            });
            context.AdminBrukere.Add(new AdminBruker()
            {
                mail = "admin@mail.no",
                passord = LagHash("admin")
            });



            produkter.ForEach(p => context.Produkter.Add(p));
            base.Seed(context);
        }

        private byte[] LagHash(string innPassord)
        {
            byte[] innData, utData;
            var algoritme = System.Security.Cryptography.SHA256.Create();
            innData = System.Text.Encoding.ASCII.GetBytes(innPassord);
            utData = algoritme.ComputeHash(innData);
            return utData;
        }
    }


}
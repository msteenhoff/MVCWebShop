using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebShopPage.Models;
using WebShopPage.Model;
using WebShopPage.BLL;

namespace WebShopPage
{
    public class WebshopInit : DropCreateDatabaseIfModelChanges<WebshopContext>
    {
        protected override void Seed(WebshopContext context)
        {
            var sikkerhet = new Sikkerhet();
            var produkter = new List<Produkt>()
            {
                new Produkt
                {
                    navn = "Banan",
                    lager = 25,
                    beskrivelse = "Gul. Litt rar form.",
                    pris = 12,
                    imgUrl = ""
                },
                new Produkt
                {
                    navn = "Eple",
                    lager = 7,
                    beskrivelse = "Grønne med stilk",
                    pris = 25,
                    imgUrl = ""
                },
                new Produkt
                {
                    navn = "Pære",
                    lager = 12,
                    beskrivelse = "Ikke til opplysning.",
                    pris = 7,
                    imgUrl = ""
                }
            };
            context.Kunder.Add(new Kunde()
            {
                    fornavn = "Magnus",
                    etternavn = "Steenhoff",
                    adresse = "Hjemmeveien 3",
                    postnummer = "1234",
                    mail = "m@mail.no",
                    passord = sikkerhet.LagHash("p"),
                    tlf = 12341234
            });

            produkter.ForEach(p => context.Produkter.Add(p));
            base.Seed(context);
        }
    }
}
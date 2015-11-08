using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopPage.Model;

namespace WebShopPage.BLL
{
    public class InitBLL
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
        }

    }
}

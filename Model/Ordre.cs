namespace WebShopPage.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;


    public class Ordre
    {
        [Key]
        public int ordreId { get; set; }
        public Kunde kunde { get; set; }
        public System.DateTime dato { get; set; }
        public virtual List<OrdreLinje> ordrelinjer { get; set; }
        public decimal sum { get; set; }
        public Ordre()
        {
            ordrelinjer = new List<OrdreLinje>();
        }

    }
    public class OrdreLinje
    {
        public int ordreLinjeId { get; set; }
        public Produkt produkt { get; set; }
        public int antall { get; set; }
        public decimal linjesum { get; set; }
    }

}

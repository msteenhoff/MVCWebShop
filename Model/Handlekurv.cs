namespace WebShopPage.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;


    public class KurvProdukt
    {
        public int kurvProduktId { get; set; }
        public int kurvId { get; set; }
        public int produktId { get; set; }
        public int antall { get; set; }

        public virtual Produkt produkt { get; set; }
        public virtual Handlekurv kurv { get; set; }

    }
    public class Handlekurv
    {
        [Key]
        public int kurvId { get; set; }
        public virtual List<KurvProdukt> produkter { get; set; }
    }
}
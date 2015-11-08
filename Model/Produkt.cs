namespace WebShopPage.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Produkt
    {
        [Key]
        public int produktId { get; set; }
        [Required]
        public string navn { get; set; }
        [Required]
        public string beskrivelse { get; set; }
        [Required]
        public int pris { get; set; }
    }
}
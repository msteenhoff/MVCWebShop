namespace WebShopPage.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class LoggInnKunde
    {
        [Key]
        [Required(ErrorMessage = "Mailadresse m� oppgis")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Epostadresse: ")]
        public string mail { get; set; }
        [Required(ErrorMessage = "Passord m� oppgis")]
        [DisplayName("Passord: ")]
        [DataType(DataType.Password)]
        public string passord { get; set; }
    }

    public class ViewKunde
    {
        public int kundeId { get; set; }
        [Required(ErrorMessage = "Mailadresse m� oppgis")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Epostadresse: ")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Mailadresse m� oppgis")]
        [DataType(DataType.EmailAddress)]
        [Compare("mail", ErrorMessage = "Mailadressene m� v�re like!")]
        [DisplayName("Bekreft mailadresse: ")]
        public string mailBekreft { get; set; }

        [Required(ErrorMessage = "Passord m� oppgis")]
        [DisplayName("Passord: ")]
        [DataType(DataType.Password)]
        public string passord { get; set; }

        [Required(ErrorMessage = "Vennligst tast passord �n gang til")]
        [DisplayName("Bekreft passord: ")]
        [Compare("passord", ErrorMessage = "Passord ikke like!")]
        [DataType(DataType.Password)]
        public string passordBekreft { get; set; }

        [DisplayName("Tast nytt passord om �nskelig: ")]
        [DataType(DataType.Password)]
        public string nyttPassord { get; set; }

        [DisplayName("Bekreft nytt passord: ")]
        [Compare("nyttPassord", ErrorMessage = "Passord ikke like!")]
        [DataType(DataType.Password)]
        public string nyttPassordBekreft { get; set; }

        [Required(ErrorMessage = "Fornavn m� oppgis")]
        [StringLength(50, ErrorMessage = "Maks 50 tegn i fornavn!")]
        [DisplayName("Fornavn/mellomnavn: ")]
        public string fornavn { get; set; }

        [Required(ErrorMessage = "Etternavn m� oppgis")]
        [StringLength(50, ErrorMessage = "Maks 50 tegn i etternavn!")]
        [DisplayName("Etternavn: ")]
        public string etternavn { get; set; }

        [Required(ErrorMessage = "Adresse m� oppgis")]
        [StringLength(50, ErrorMessage = "Maks 50 tegn i adresse!")]
        [DisplayName("Adresse: ")]
        public string adresse { get; set; }
        [Required]
        [Range(10000000, 99999999, ErrorMessage = "Telefonnummer m� v�re �tte siffer!")]
        [DisplayName("Telefonnummer: ")]
        public int tlf { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnummer m� v�re mellom 1 og 9999!")]
        [DisplayName("Postnummer: ")]
        public string postnummer { get; set; }

        /*public static ViewKunde ViewKundeFraKunde(Kunde k)
        {
            if (k != null)
            {
                var vk = new ViewKunde();
                vk.kundeId = k.kundeId;
                vk.mail = k.mail;
                vk.fornavn = k.fornavn;
                vk.etternavn = k.etternavn;
                vk.adresse = k.adresse;
                vk.postnummer = k.postnummer;
                vk.tlf = k.tlf;

                return vk;
            }
            else
                return null;
        }*/
    }


    public class KurvProduktView
    {
        public string navn { get; set; }
        public int antall { get; set; }
        public decimal pris { get; set; }
    }

    public class ViewProdukt
    {
        [Key]
        public int produktId { get; set; }
        [Required]
        public string Navn { get; set; }
        [Required]
        public string Beskrivelse { get; set; }
        [Required]
        public int Pris { get; set; }

    }

    public class OrdreView // for visning av flere ordre i liste
    {
        public int ordreId { get; set; }
        public System.DateTime dato { get; set; }
        public string kundenavn { get; set; }
        public decimal sum { get; set; }

    }

    public class OrdreListeView //for visning av �n ordre med sum
    {
        public decimal ordresum { get; set; }
        public List<OrdreLinjeView> linjer { get; set; }

    }


    public class OrdreLinjeView // for visning av linjer i hver enkelt ordre
    {
        public string navn { get; set; }
        public int antall { get; set; }
        public decimal pris { get; set; }
        
    }

    public class AdminEndreKundePassordView
    {
        public int kundeId { get; set; }
        [Required(ErrorMessage = "Mailadresse m� oppgis")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Epostadresse: ")]
        public string mail { get; set; }
        [Required(ErrorMessage = "Passord m� oppgis")]
        [DisplayName("Passord: ")]
        [DataType(DataType.Password)]
        public string passord { get; set; }

        [Required(ErrorMessage = "Vennligst tast passord �n gang til")]
        [DisplayName("Bekreft passord: ")]
        [Compare("passord", ErrorMessage = "Passord ikke like!")]
        [DataType(DataType.Password)]
        public string passordBekreft { get; set; }
    }
}
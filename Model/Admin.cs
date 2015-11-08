using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopPage.Model
{
    public class AdminBruker
    {
        [Key]
        public string mail { get; set; }
        public byte[] passord  { get; set; }
    }

    public class OpprettAdminBruker
    {
        [Key]
        [Required(ErrorMessage = "Mailadresse må oppgis")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Epostadresse: ")]
        public string mail { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        [DisplayName("Passord: ")]
        [DataType(DataType.Password)]
        public string passord { get; set; }
        [Required(ErrorMessage = "Vennligst tast passord én gang til")]
        [DisplayName("Bekreft passord: ")]
        [Compare("passord", ErrorMessage = "Passord ikke like!")]
        [DataType(DataType.Password)]
        public string bekreftPassord { get; set; }
    }

    public class LoggInnAdmin
    {
        [Key]
        [Required(ErrorMessage = "Mailadresse må oppgis")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Epostadresse: ")]
        public string mail { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        [DisplayName("Passord: ")]
        [DataType(DataType.Password)]
        public string passord { get; set; }
    }
    public class erferf
    {

    }
}

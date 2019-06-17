using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Entities.Models
{
    public class Biaya
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Tingkat Id")]
        public int BiayaId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Kategori")]
        public string KatBiaya { get; set; }

        //public IEnumerable<SelectListItem> Biayas { get; set; }

        [Display(Name = "Jenis")]
        public string JenisBiaya { get; set; }

        [Display(Name = "Nominal")]
        public string NomBiaya { get; set; }

        [Display(Name = "Tingkat")]
        public int? TingkatId { get; set; }

        //public SelectList Tingkats()
        //{
        //    DatabaseContext db = new DatabaseContext();
        //    var Tingkats = db.Tingkats;

        //    return new SelectList(Tingkats.ToList(), "TingkatId", "Namatingkat", "0");
        //}
    }
}
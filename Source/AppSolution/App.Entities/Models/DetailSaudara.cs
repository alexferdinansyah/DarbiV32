using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class DetailSaudara
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Detail Saudara Id")]
        public int DetailSaudaraId { get; set; }
        //public IEnumerable<SelectListItem> DetailSaudaras { get; set; }

        [Display(Name = "Siswa Id")]
        public int SiswaId { get; set; }

        [Required]
        [Display(Name = "Nama Lengkap")]
        public string Fullname { get; set; }

        [Required]
        [Display(Name = "Jenis Kelamin")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Tanggal Lahir")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string Dob { get; set; }
    }
}
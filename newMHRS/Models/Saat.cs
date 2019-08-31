using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class Saat
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Lütfen Saat Alanını Seçiniz. Saat Seçimi yapamıyorsanız, doktora ait uygun saat bulunmamaktadır.")]
      //  [Display(Name = "Lütfen Saat Alanını Seçiniz.\n Saat Seçimi yapamıyorsanız, doktora ait uygun saat bulunmamaktadır.")]
        // [DataType(DataType.Time)]
        public string SaatKac { get; set; }

        public bool SaatDurum { get; set; } 


        public int? DoktorId { get; set; }//?
        [ForeignKey("DoktorId")]
        public virtual Doktor Doktor { get; set; }


        public virtual ICollection<Randevu> Randevus { get; set; }

    }
}
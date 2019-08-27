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


        [Required(ErrorMessage = "Lütfen Saat Seçiniz.")]
        // [DataType(DataType.Time)]
        public string SaatKac { get; set; }


        public int? DoktorId { get; set; }//?
        [ForeignKey("DoktorId")]
        public virtual Doktor Doktor { get; set; }

        public virtual ICollection<Randevu> Randevus { get; set; }

    }
}
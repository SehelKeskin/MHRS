using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class Ilce
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Lütfen İlçe Seçiniz")]
        [Display(Name = "İlçe:")]
        public string Ad { get; set; }


        //public int SehirId { get; set; }
        //[ForeignKey("SehirId")]
        //public virtual Sehir Sehir { get; set; }
        public int? SehirId { get; set; }
        public virtual Sehir Sehir { get; set; }

        public virtual ICollection<Hastahane> Hastahanes { get; set; }
        public virtual ICollection<Randevu> Randevus { get; set; }
    }
}
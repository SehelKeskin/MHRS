using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class Sehir
    {
        public int Id { get; set; }

        [StringLength(100)]

        [Required(ErrorMessage = "Lütfen Şehir Seçiniz")]
        [Display(Name = "İl:")]
        public string Ad { get; set; }


        public virtual ICollection<Hastahane> Hastahanes { get; set; }
        public virtual ICollection<Ilce> Ilces { get; set; }
        public virtual ICollection<Randevu> Randevus { get; set; }



    }
}
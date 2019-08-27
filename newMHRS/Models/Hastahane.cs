using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class Hastahane
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Lütfen Hastahane Seçiniz.")]
        [Display(Name = "Hastahane Adı:")]
        public string Ad { get; set; }

        public int SehirId { get; set; }
        [ForeignKey("SehirId")]
        public virtual Sehir Sehir { get; set; }

        public int IlceId { get; set; }
        [ForeignKey("IlceId")]
        public virtual Ilce Ilce { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Hastahane Adresi:")]
        public string Adres { get; set; }


        [StringLength(100)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Hastahane Numarası")]
        public string Tel { get; set; }

        public virtual ICollection<Bolum> Bolums { get; set; }
        public virtual ICollection<Doktor> Doktors { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class Randevu
    {

        public int Id { get; set; }

        [Display(Name = "Tarih:")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Lütfen Randevu Tarihi Seçiniz.")]
        //   [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Tarih { get; set; }

        public int? SehirId { get; set; }
        [ForeignKey("SehirId")]
        public virtual Sehir Sehir { get; set; }


        public int IlceId { get; set; }
        [ForeignKey("IlceId")]
        public virtual Ilce Ilce { get; set; }


        public int HastaId { get; set; }
        [ForeignKey("HastaId")]
        public virtual Hasta Hasta { get; set; }


        public int? HastahaneId { get; set; }
        [ForeignKey("HastahaneId")]
        public virtual Hastahane Hastahane { get; set; }

        public int? BolumId { get; set; }
        [ForeignKey("BolumId")]
        public virtual Bolum Bolum { get; set; }

        public int DoktorId { get; set; }
        [ForeignKey("DoktorId")]
        public virtual Doktor Doktor { get; set; }

        [Required(ErrorMessage = "Lütfen Saat Alanını Seçiniz.Saat Seçimi yapamıyorsanız, doktora ait uygun saat bulunmamaktadır.")]
        public int? SaatId { get; set; }
        [ForeignKey("SaatId")]
        public virtual Saat Saat { get; set; }


        public bool IptalMi { get; set; }

    }
}
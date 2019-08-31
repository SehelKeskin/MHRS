using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class CascadingClass
    {
        [Display(Name = "Tarih:")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Lütfen Randevu Tarihi Seçiniz.")]
   //     [Range(DateTime.Now(), DateTime.Now.AddDays(15), ErrorMessage ="Lütfen Randevunuzu 15 Gün içinde alınız")]
        public DateTime Tarih { get; set; }

        [Required(ErrorMessage ="Lütfen Şehir Seçiniz.")]
        [Display(Name ="Şehir:")]
        public int SehirId { get; set; }

        [Required(ErrorMessage = "Lütfen İlçe Seçiniz.")]
        [Display(Name = "İlçe:")]
        public int IlceId { get; set; }

        [Required(ErrorMessage = "Lütfen Hastahe Seçiniz.")]
        [Display(Name = "Hastahane:")]
        public int HastahaneId { get; set; }

        [Required(ErrorMessage = "Lütfen Bölüm Seçiniz.")]
        [Display(Name = "Bolüm:")]
        public int BolumId { get; set; }

        [Required(ErrorMessage = "Lütfen Doktor Seçiniz.")]
        [Display(Name = "Doktor:")]
        public int DoktorId { get; set; }

       [Required(ErrorMessage = "Lütfen Saat Alanını Seçiniz. Saat Seçimi yapamıyorsanız, doktora ait uygun saat bulunmamaktadır.")]
       [Display(Name="Saat:")]
        public int SaatId { get; set; }
    }
}
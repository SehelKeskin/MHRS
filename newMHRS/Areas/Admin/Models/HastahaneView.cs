using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newMHRS.Areas.Admin.Models
{
    public class HastahaneView
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Lütfen Hastahane Adı Giriniz.")]
        [Display(Name = "Hastahane Adı:")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen Şehir Seçiniz.")]
        [Display(Name = "Şehir:")]
        public int SehirId { get; set; }

        [Required(ErrorMessage = "Lütfen İlçe Seçiniz.")]
        [Display(Name = "İlçe:")]
        public int IlceId { get; set; }

        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Hastahane Adresi:")]
        public string Adres { get; set; }


        [StringLength(100)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Hastahane Numarası")]
        public string Tel { get; set; }
    }
}
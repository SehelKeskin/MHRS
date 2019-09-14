using newMHRS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newMHRS.Areas.Admin.Models
{
    public class HastaView
    {


        [RegularExpression(@"^\d+$", ErrorMessage = "Lütfen Tc'nizi sayı olarak giriniz")]
        [Required(ErrorMessage = "Lütfen Tc'nizi giriniz.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Lütfen Tc'nizi 11 hane olarak giriniz.")]
        [Display(Name = "Hastanın Tc'si:")]
        public string Tc { get; set; }

        [MaxLength(50)]
        [Display(Name = "Ad:")]
        [Required(ErrorMessage = "Lütfen Adınızı Giriniz ")]
        public string Ad { get; set; }

        [MaxLength(50)]
        [Display(Name = "Soyad:")]
        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz")]
        public string Soyad { get; set; }

        [Display(Name = "Cinsiyet:")]
        public Gender Cinsiyet { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi:")]
        public DateTime DogumTarihi { get; set; }

        [MaxLength(150)]
        [Display(Name = "Doğum Yeri:")]
        public string DogumYeri { get; set; }

        [Display(Name = "Anne Adı:")]
        [MaxLength(50)]
        public string AnneAdi { get; set; }

        [MaxLength(50)]
        [Display(Name = "Baba Adı:")]
        public string BabaAdi { get; set; }

        [MaxLength(20)]
        [Display(Name = "Cep Telefon:")]
        [DataType(DataType.PhoneNumber)]
        public string CepTel { get; set; }

        [Required(ErrorMessage ="Mail Alanını Boş Geçemezsiniz.")]
        [MaxLength(50)]
        [Display(Name = "E-Mail:")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen Şifre Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre:")]
        public string Sifre { get; set; }

        [MaxLength(50)]
        [Compare("Sifre", ErrorMessage = "Şifreler eşleşmiyor.Tekrar deneyiniz.")]
        [Required(ErrorMessage = "Tekrar Şifre Alanı Boş Olamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Tekrar Şifre:")]
        public string TSifre { get; set; }
    }
}
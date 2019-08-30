using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class AdminClass
    {
        public int Id { get; set; }


        [RegularExpression(@"^\d+$", ErrorMessage = "Lütfen Tc'nizi sayı olarak giriniz")]
        [Required(ErrorMessage = "Lütfen Tc'nizi giriniz.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Lütfen Tc'nizi 11 hane olarak giriniz.")]
        [Display(Name = "Tc")]
        public string Tc { get; set; }

        [MaxLength(50)]
        [Display(Name = "Adınız:")]
        [Required(ErrorMessage = "Lütfen Adınızı Giriniz ")]
        public string Ad { get; set; }

        [MaxLength(50)]
        [Display(Name = "Soyadınız:")]
        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz")]
        public string Soyad { get; set; }


        [MaxLength(20)]
        [Display(Name = "Cep Telefonunuz:")]
        [DataType(DataType.PhoneNumber)]
        public string CepTel { get; set; }

        [MaxLength(50)]
        [Display(Name = "E-Mailiniz :")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifreniz:")]
        public string Sifre { get; set; }

        [MaxLength(50)]
        [Compare("Sifre", ErrorMessage = "Şifreleriniz eşleşmiyor.Tekrar deneyiniz.")]
        [Required(ErrorMessage = "Tekrar Şifrenizi Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Tekrar Şifre:")]
        public string TSifre { get; set; }
    }
}
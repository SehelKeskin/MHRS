using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class ParolaViewModel
    {


        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifreniz:")]
        public string MSifre { get; set; }//Mevcut Şifre


        [MaxLength(50)]
        [Required(ErrorMessage = "Yeni Şifrenizi Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre:")]
        public string YSifre { get; set; }//Yeni Şifre


        [MaxLength(50)]
        [Compare("YSifre", ErrorMessage = "Şifreleriniz eşleşmiyor.Tekrar deneyiniz.")]
        [Required(ErrorMessage = "Yeni Şifrenizi Tekrar Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Tekrar Şifre:")]
        public string TSifre { get; set; }


    }
}
using newMHRS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newMHRS.Areas.Admin.Models
{
    public class DoktorView
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen Doktor Adını Giriniz.")]
        [Display(Name = "Ad:")]
        public string Ad { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen Doktor Soyadını Giriniz.")]
        [Display(Name = "Soyad:")]
        public string Soyad { get; set; }

        [Display(Name = "Cinsiyet:")]
        public Gender Cinsiyet { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(50)]
        [Display(Name = "Cep Telefon:")]
        public string CepTel { get; set; }

        [MaxLength(50)]
        [Display(Name = "E-Mail Adres:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Lütfen Hastahane Seçiniz.")]
        public int HastahaneId { get; set; }
        [Required(ErrorMessage = "Lütfen Bölüm Seçiniz.")]
        public int BolumId { get; set; }
       
    }
}
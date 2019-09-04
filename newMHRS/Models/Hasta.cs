using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class Hasta
    {
        [Display(Name = "Id:")]
  //      [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        //artık ıd tc oldu.

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

        [Display(Name = "Cinsiyetiniz:")]
        public Gender Cinsiyet { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihiniz:")]
        public DateTime DogumTarihi { get; set; }

        [MaxLength(150)]
        [Display(Name = "Doğum Yeriniz:")]
        public string DogumYeri { get; set; }

        [Display(Name = "Annenizin Adı:")]
        [MaxLength(50)]
        public string AnneAdi { get; set; }

        [MaxLength(50)]
        [Display(Name = "Babanızın Adı:")]
        public string BabaAdi { get; set; }

        [MaxLength(20)]
        [Display(Name = "Cep Telefonunuz:")]
        [DataType(DataType.PhoneNumber)]
        public string CepTel { get; set; }

        [MaxLength(50)]
        [Display(Name = "E-Mailiniz :")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage ="Lütfen Şifrenizi Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifreniz:")]
        public string Sifre { get; set; }

        [MaxLength(50)]
        [Compare("Sifre", ErrorMessage = "Şifreleriniz eşleşmiyor.Tekrar deneyiniz.")]
        [Required(ErrorMessage = "Tekrar Şifrenizi Giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Tekrar Şifre:")]
        public string TSifre { get; set; }

        // public string LoginErorMsg { get; set; }

        public virtual ICollection<Randevu> Randevus { get; set; }
    }
}
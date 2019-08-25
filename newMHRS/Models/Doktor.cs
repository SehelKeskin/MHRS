using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class Doktor
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Lütfen Doktor Adını Giriniz")]
        [Display(Name = "Ad:")]
        public string Ad { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Soyad:")]
        public string Soyad { get; set; }

        [Display(Name = "Cinsiyet:")]
        public Gender Cinsiyet { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(50)]
        [Display(Name = "Cep Telefonu:")]
        public string CepTel { get; set; }

        [MaxLength(50)]
        [Display(Name = "E-Mail Adresi:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int? HastahaneId { get; set; }//?
        [ForeignKey("HastahaneId")]
        public virtual Hastahane Hastahane { get; set; }

        public int? BolumId { get; set; }//?
        [ForeignKey("BolumId")]
        public virtual Bolum Bolum { get; set; }

        //public int SaatId { get; set; }
        //[ForeignKey("SaatId")]
        //public virtual Saat Saat { get; set; }

            public virtual ICollection<Saat> Saats { get; set; }
        public virtual ICollection<Randevu> Randevus { get; set; }



        [NotMapped]
        public string TamAd
        {
            get
            {
                return Ad + " " + Soyad;
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace newMHRS.Areas.Admin.Models
{
    public class BolumView
    {
        [StringLength(100)]
        [Required(ErrorMessage ="Lütfen Bölüm Adını Giriniz.")]
        [Display(Name ="Bölüm Adı")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen Hastahane Seçimi Yapınız.")]
        [Display(Name = "Hastahane Adı")]
        public int HastahaneId { get; set; }
       
    }
}
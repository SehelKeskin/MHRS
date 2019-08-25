using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newMHRS.Models
{
    public class Bolum
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Ad { get; set; }

        public int HastahaneId { get; set; }
        [ForeignKey("HastahaneId")]
        public virtual Hastahane Hastahane { get; set; }

        public virtual ICollection<Doktor> Doktors { get; set; }

    }
}
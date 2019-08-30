using newMHRS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace newMHRS
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Bolum> Bolums { get; set; }
        public virtual DbSet<Doktor> Doktors { get; set; }
        public virtual DbSet<Hasta> Hastas { get; set; }
        public virtual DbSet<Hastahane> Hastahanes { get; set; }
        public virtual DbSet<Ilce> Ilces { get; set; }
        public virtual DbSet<Randevu> Randevus { get; set; }
        public virtual DbSet<Sehir> Sehirs { get; set; }
        public virtual DbSet<Saat> Saats { get; set; }
        public virtual DbSet<AdminClass> AdminClasses { get; set; }


    }
}
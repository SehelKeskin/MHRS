namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SehirId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bolums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 100),
                        HastahaneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hastahanes", t => t.HastahaneId, cascadeDelete: true)
                .Index(t => t.HastahaneId);
            
            CreateTable(
                "dbo.Doktors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 50),
                        Soyad = c.String(nullable: false, maxLength: 50),
                        Cinsiyet = c.Int(nullable: false),
                        CepTel = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        HastahaneId = c.Int(),
                        BolumId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bolums", t => t.BolumId)
                .ForeignKey("dbo.Hastahanes", t => t.HastahaneId)
                .Index(t => t.HastahaneId)
                .Index(t => t.BolumId);
            
            CreateTable(
                "dbo.Hastahanes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 100),
                        SehirId = c.Int(nullable: false),
                        IlceId = c.Int(nullable: false),
                        Adres = c.String(maxLength: 4000),
                        Tel = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ilces", t => t.IlceId, cascadeDelete: true)
                .ForeignKey("dbo.Sehirs", t => t.SehirId, cascadeDelete: true)
                .Index(t => t.SehirId)
                .Index(t => t.IlceId);
            
            CreateTable(
                "dbo.Ilces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 100),
                        SehirId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sehirs", t => t.SehirId)
                .Index(t => t.SehirId);
            
            CreateTable(
                "dbo.Randevus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tarih = c.DateTime(nullable: false),
                        SehirId = c.Int(),
                        IlceId = c.Int(nullable: false),
                        HastaId = c.Int(nullable: false),
                        HastahaneId = c.Int(),
                        BolumId = c.Int(),
                        DoktorId = c.Int(nullable: false),
                        SaatId = c.Int(),
                        IptalMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bolums", t => t.BolumId)
                .ForeignKey("dbo.Doktors", t => t.DoktorId, cascadeDelete: true)
                .ForeignKey("dbo.Hastas", t => t.HastaId, cascadeDelete: true)
                .ForeignKey("dbo.Hastahanes", t => t.HastahaneId)
                .ForeignKey("dbo.Ilces", t => t.IlceId, cascadeDelete: true)
                .ForeignKey("dbo.Saats", t => t.SaatId)
                .ForeignKey("dbo.Sehirs", t => t.SehirId)
                .Index(t => t.SehirId)
                .Index(t => t.IlceId)
                .Index(t => t.HastaId)
                .Index(t => t.HastahaneId)
                .Index(t => t.BolumId)
                .Index(t => t.DoktorId)
                .Index(t => t.SaatId);
            
            CreateTable(
                "dbo.Hastas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tc = c.String(nullable: false, maxLength: 11),
                        Ad = c.String(nullable: false, maxLength: 50),
                        Soyad = c.String(nullable: false, maxLength: 50),
                        Cinsiyet = c.Int(nullable: false),
                        DogumTarihi = c.DateTime(nullable: false),
                        DogumYeri = c.String(maxLength: 150),
                        AnneAdi = c.String(maxLength: 50),
                        BabaAdi = c.String(maxLength: 50),
                        CepTel = c.String(maxLength: 20),
                        Mail = c.String(maxLength: 50),
                        Sifre = c.String(nullable: false, maxLength: 50),
                        TSifre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Saats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaatKac = c.String(nullable: false),
                        DoktorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doktors", t => t.DoktorId)
                .Index(t => t.DoktorId);
            
            CreateTable(
                "dbo.Sehirs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Randevus", "SehirId", "dbo.Sehirs");
            DropForeignKey("dbo.Ilces", "SehirId", "dbo.Sehirs");
            DropForeignKey("dbo.Hastahanes", "SehirId", "dbo.Sehirs");
            DropForeignKey("dbo.Randevus", "SaatId", "dbo.Saats");
            DropForeignKey("dbo.Saats", "DoktorId", "dbo.Doktors");
            DropForeignKey("dbo.Randevus", "IlceId", "dbo.Ilces");
            DropForeignKey("dbo.Randevus", "HastahaneId", "dbo.Hastahanes");
            DropForeignKey("dbo.Randevus", "HastaId", "dbo.Hastas");
            DropForeignKey("dbo.Randevus", "DoktorId", "dbo.Doktors");
            DropForeignKey("dbo.Randevus", "BolumId", "dbo.Bolums");
            DropForeignKey("dbo.Hastahanes", "IlceId", "dbo.Ilces");
            DropForeignKey("dbo.Doktors", "HastahaneId", "dbo.Hastahanes");
            DropForeignKey("dbo.Bolums", "HastahaneId", "dbo.Hastahanes");
            DropForeignKey("dbo.Doktors", "BolumId", "dbo.Bolums");
            DropIndex("dbo.Saats", new[] { "DoktorId" });
            DropIndex("dbo.Randevus", new[] { "SaatId" });
            DropIndex("dbo.Randevus", new[] { "DoktorId" });
            DropIndex("dbo.Randevus", new[] { "BolumId" });
            DropIndex("dbo.Randevus", new[] { "HastahaneId" });
            DropIndex("dbo.Randevus", new[] { "HastaId" });
            DropIndex("dbo.Randevus", new[] { "IlceId" });
            DropIndex("dbo.Randevus", new[] { "SehirId" });
            DropIndex("dbo.Ilces", new[] { "SehirId" });
            DropIndex("dbo.Hastahanes", new[] { "IlceId" });
            DropIndex("dbo.Hastahanes", new[] { "SehirId" });
            DropIndex("dbo.Doktors", new[] { "BolumId" });
            DropIndex("dbo.Doktors", new[] { "HastahaneId" });
            DropIndex("dbo.Bolums", new[] { "HastahaneId" });
            DropTable("dbo.Sehirs");
            DropTable("dbo.Saats");
            DropTable("dbo.Hastas");
            DropTable("dbo.Randevus");
            DropTable("dbo.Ilces");
            DropTable("dbo.Hastahanes");
            DropTable("dbo.Doktors");
            DropTable("dbo.Bolums");
        }
    }
}

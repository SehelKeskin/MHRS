namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tc = c.String(nullable: false, maxLength: 11),
                        Ad = c.String(nullable: false, maxLength: 50),
                        Soyad = c.String(nullable: false, maxLength: 50),
                        CepTel = c.String(maxLength: 20),
                        Mail = c.String(maxLength: 50),
                        Sifre = c.String(nullable: false, maxLength: 50),
                        TSifre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminClasses");
        }
    }
}

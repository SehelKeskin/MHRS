namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminClasHata : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.HastahaneViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HastahaneViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 100),
                        SehirId = c.Int(nullable: false),
                        IlceId = c.Int(nullable: false),
                        Adres = c.String(maxLength: 4000),
                        Tel = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}

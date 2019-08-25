namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alann234 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doktors", "SaatId", "dbo.Saats");
            DropIndex("dbo.Doktors", new[] { "SaatId" });
            AddColumn("dbo.Saats", "DoktorId", c => c.Int());
            CreateIndex("dbo.Saats", "DoktorId");
            AddForeignKey("dbo.Saats", "DoktorId", "dbo.Doktors", "Id");
            DropColumn("dbo.Doktors", "SaatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doktors", "SaatId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Saats", "DoktorId", "dbo.Doktors");
            DropIndex("dbo.Saats", new[] { "DoktorId" });
            DropColumn("dbo.Saats", "DoktorId");
            CreateIndex("dbo.Doktors", "SaatId");
            AddForeignKey("dbo.Doktors", "SaatId", "dbo.Saats", "Id", cascadeDelete: true);
        }
    }
}

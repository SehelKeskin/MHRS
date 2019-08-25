namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alann1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Randevus", "SaatId", c => c.Int());
            CreateIndex("dbo.Randevus", "SaatId");
            AddForeignKey("dbo.Randevus", "SaatId", "dbo.Saats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Randevus", "SaatId", "dbo.Saats");
            DropIndex("dbo.Randevus", new[] { "SaatId" });
            DropColumn("dbo.Randevus", "SaatId");
        }
    }
}

namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaatErrorMessage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Randevus", "SaatId", "dbo.Saats");
            DropIndex("dbo.Randevus", new[] { "SaatId" });
            AlterColumn("dbo.Randevus", "SaatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Randevus", "SaatId");
            AddForeignKey("dbo.Randevus", "SaatId", "dbo.Saats", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Randevus", "SaatId", "dbo.Saats");
            DropIndex("dbo.Randevus", new[] { "SaatId" });
            AlterColumn("dbo.Randevus", "SaatId", c => c.Int());
            CreateIndex("dbo.Randevus", "SaatId");
            AddForeignKey("dbo.Randevus", "SaatId", "dbo.Saats", "Id");
        }
    }
}

namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Yenilik : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Hastahanes", "SehirId", "dbo.Sehirs");
            DropIndex("dbo.Hastahanes", new[] { "SehirId" });
            RenameColumn(table: "dbo.Hastahanes", name: "SehirId", newName: "Sehir_Id");
            AlterColumn("dbo.Hastahanes", "Sehir_Id", c => c.Int());
            CreateIndex("dbo.Hastahanes", "Sehir_Id");
            AddForeignKey("dbo.Hastahanes", "Sehir_Id", "dbo.Sehirs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hastahanes", "Sehir_Id", "dbo.Sehirs");
            DropIndex("dbo.Hastahanes", new[] { "Sehir_Id" });
            AlterColumn("dbo.Hastahanes", "Sehir_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Hastahanes", name: "Sehir_Id", newName: "SehirId");
            CreateIndex("dbo.Hastahanes", "SehirId");
            AddForeignKey("dbo.Hastahanes", "SehirId", "dbo.Sehirs", "Id", cascadeDelete: true);
        }
    }
}

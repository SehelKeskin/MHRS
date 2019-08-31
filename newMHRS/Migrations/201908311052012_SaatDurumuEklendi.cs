namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaatDurumuEklendi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Saats", "SaatDurum", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Saats", "SaatDurum");
        }
    }
}

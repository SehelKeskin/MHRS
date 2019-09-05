namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RandevuDurumAlanıEklendi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Randevus", "RandevuDurum", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Randevus", "RandevuDurum");
        }
    }
}

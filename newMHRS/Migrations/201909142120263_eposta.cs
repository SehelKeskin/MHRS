namespace newMHRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eposta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hastas", "Mail", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hastas", "Mail", c => c.String(maxLength: 50));
        }
    }
}

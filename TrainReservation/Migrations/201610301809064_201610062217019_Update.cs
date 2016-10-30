namespace TrainReservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201610062217019_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Due", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Due");
        }
    }
}

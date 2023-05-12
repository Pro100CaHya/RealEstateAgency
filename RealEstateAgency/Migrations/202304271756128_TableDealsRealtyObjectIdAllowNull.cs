namespace RealEstateAgency.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableDealsRealtyObjectIdAllowNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "RealtyObjectId", "dbo.RealtyObjects");
            DropForeignKey("dbo.Deals", "RealtorId", "dbo.Realtors");
            DropIndex("dbo.Deals", new[] { "RealtyObjectId" });
            DropIndex("dbo.Deals", new[] { "RealtorId" });
            AlterColumn("dbo.Deals", "RealtyObjectId", c => c.Int());
            AlterColumn("dbo.Deals", "RealtorId", c => c.Int());
            CreateIndex("dbo.Deals", "RealtyObjectId");
            CreateIndex("dbo.Deals", "RealtorId");
            AddForeignKey("dbo.Deals", "RealtyObjectId", "dbo.RealtyObjects", "Id");
            AddForeignKey("dbo.Deals", "RealtorId", "dbo.Realtors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "RealtorId", "dbo.Realtors");
            DropForeignKey("dbo.Deals", "RealtyObjectId", "dbo.RealtyObjects");
            DropIndex("dbo.Deals", new[] { "RealtorId" });
            DropIndex("dbo.Deals", new[] { "RealtyObjectId" });
            AlterColumn("dbo.Deals", "RealtorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Deals", "RealtyObjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.Deals", "RealtorId");
            CreateIndex("dbo.Deals", "RealtyObjectId");
            AddForeignKey("dbo.Deals", "RealtorId", "dbo.Realtors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Deals", "RealtyObjectId", "dbo.RealtyObjects", "Id", cascadeDelete: true);
        }
    }
}

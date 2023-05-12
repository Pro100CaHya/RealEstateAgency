namespace RealEstateAgency.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRealtyObjectAssessment1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        DealDate = c.DateTime(nullable: false, storeType: "date"),
                        RealtyObjectId = c.Int(nullable: false),
                        RealtorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Realtors", t => t.RealtorId, cascadeDelete: true)
                .ForeignKey("dbo.RealtyObjects", t => t.RealtyObjectId, cascadeDelete: true)
                .Index(t => t.RealtyObjectId)
                .Index(t => t.RealtorId);
            
            CreateTable(
                "dbo.Realtors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        Name = c.String(),
                        Patronym = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "RealtyObjectId", "dbo.RealtyObjects");
            DropForeignKey("dbo.Deals", "RealtorId", "dbo.Realtors");
            DropIndex("dbo.Deals", new[] { "RealtorId" });
            DropIndex("dbo.Deals", new[] { "RealtyObjectId" });
            DropTable("dbo.Realtors");
            DropTable("dbo.Deals");
        }
    }
}

namespace RealEstateAgency.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRealtyObject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RealtyObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Floor = c.Int(nullable: false),
                        NumberOfRooms = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Description = c.String(),
                        Area = c.Single(nullable: false),
                        AnnouncementDate = c.DateTime(nullable: false, storeType: "date"),
                        DistrictId = c.Int(nullable: false),
                        RealtyObjectTypeId = c.Int(nullable: false),
                        BuildingMaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BuildingMaterials", t => t.BuildingMaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .ForeignKey("dbo.RealtyObjectTypes", t => t.RealtyObjectTypeId, cascadeDelete: true)
                .Index(t => t.DistrictId)
                .Index(t => t.RealtyObjectTypeId)
                .Index(t => t.BuildingMaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealtyObjects", "RealtyObjectTypeId", "dbo.RealtyObjectTypes");
            DropForeignKey("dbo.RealtyObjects", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.RealtyObjects", "BuildingMaterialId", "dbo.BuildingMaterials");
            DropIndex("dbo.RealtyObjects", new[] { "BuildingMaterialId" });
            DropIndex("dbo.RealtyObjects", new[] { "RealtyObjectTypeId" });
            DropIndex("dbo.RealtyObjects", new[] { "DistrictId" });
            DropTable("dbo.RealtyObjects");
        }
    }
}

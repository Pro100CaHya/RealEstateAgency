namespace RealEstateAgency.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRealtyObjectAssessment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RealtyObjectAssessments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        ScoreDate = c.DateTime(nullable: false, storeType: "date"),
                        RealtyObjectId = c.Int(nullable: false),
                        CriteriaTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CriteriaTypes", t => t.CriteriaTypeId, cascadeDelete: true)
                .ForeignKey("dbo.RealtyObjects", t => t.RealtyObjectId, cascadeDelete: true)
                .Index(t => t.RealtyObjectId)
                .Index(t => t.CriteriaTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RealtyObjectAssessments", "RealtyObjectId", "dbo.RealtyObjects");
            DropForeignKey("dbo.RealtyObjectAssessments", "CriteriaTypeId", "dbo.CriteriaTypes");
            DropIndex("dbo.RealtyObjectAssessments", new[] { "CriteriaTypeId" });
            DropIndex("dbo.RealtyObjectAssessments", new[] { "RealtyObjectId" });
            DropTable("dbo.RealtyObjectAssessments");
        }
    }
}

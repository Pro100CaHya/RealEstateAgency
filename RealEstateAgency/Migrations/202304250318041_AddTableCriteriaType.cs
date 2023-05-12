namespace RealEstateAgency.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableCriteriaType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CriteriaTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CriteriaTypes");
        }
    }
}

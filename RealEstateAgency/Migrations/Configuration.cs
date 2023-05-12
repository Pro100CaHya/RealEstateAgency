namespace RealEstateAgency.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RealEstateAgency.DataModel.RealEstateAgencyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RealEstateAgency.DataModel.RealEstateAgencyContext";
        }

        protected override void Seed(RealEstateAgency.DataModel.RealEstateAgencyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

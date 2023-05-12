using System;
using System.Data.Entity;
using System.Linq;

namespace RealEstateAgency.DataModel
{
    public class RealEstateAgencyContext : DbContext
    {
        public RealEstateAgencyContext()
            : base("name=RealEstateAgencyContext")
        {
        }

        public DbSet<District> Districts { get; set; }
        public DbSet<BuildingMaterial> BuildingMaterials { get; set; }
        public DbSet<RealtyObjectType> RealtyObjectTypes { get; set; }
        public DbSet<RealtyObject> RealtyObjects { get; set; }
        public DbSet<CriteriaType> CriteriaTypes { get; set; }
        public DbSet<RealtyObjectAssessment> RealtyObjectAssessments { get; set; }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Deal> Deals { get; set; }
    }
}
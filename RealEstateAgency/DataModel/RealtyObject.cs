using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.DataModel
{
    public class RealtyObject
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Floor { get; set; }
        public int NumberOfRooms { get; set; }
        public int Status { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public float Area { get; set; }

        [Column(TypeName = "date")]
        public DateTime AnnouncementDate { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public int RealtyObjectTypeId { get; set; }
        public RealtyObjectType RealtyObjectType { get; set; }

        public int BuildingMaterialId { get; set; }
        public BuildingMaterial BuildingMaterial { get; set; }

        public ICollection<RealtyObjectAssessment> RealtyObjectAssessment { get; set; }
        public ICollection<Deal> Deal { get; set; }
    }
}

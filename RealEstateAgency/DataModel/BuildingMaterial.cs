using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.DataModel
{
    public class BuildingMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RealtyObject> RealtyObject { get; set; }
    }
}

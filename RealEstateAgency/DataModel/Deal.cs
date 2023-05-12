using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.DataModel
{
    public class Deal
    {
        public int Id { get; set; }
        public float Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime DealDate { get; set; }

        public int? RealtyObjectId { get; set; }
        public RealtyObject RealtyObject { get; set; }

        public int? RealtorId { get; set; }
        public Realtor Realtor { get; set; }
    }
}

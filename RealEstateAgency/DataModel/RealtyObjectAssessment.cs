using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.DataModel
{
    public class RealtyObjectAssessment
    {
        public int Id { get; set; }
        public int Score { get; set; }

        [Column(TypeName = "date")]
        public DateTime ScoreDate { get; set; }

        public int RealtyObjectId { get; set; }
        public RealtyObject RealtyObject { get; set; }

        public int CriteriaTypeId { get; set; }
        public CriteriaType CriteriaType { get; set; }
    }
}

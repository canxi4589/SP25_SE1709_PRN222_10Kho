using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class RefundPolicySetting : BaseEntity
    {
        public int NoRefundHours { get; set; } = 6;
        public int PartialRefundHours { get; set; } = 12;
        public int FullRefundHours { get; set; } = 24;
        public decimal PartialRefundPercentage { get; set; } = 50; // 50%
    }
}

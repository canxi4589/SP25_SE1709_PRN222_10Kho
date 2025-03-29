using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Entities
{
    public class Refund : BaseEntity
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; } = null!;
        public DateTime RefundedAt { get; set; } = DateTime.Now;

        public Appointment Appointment { get; set; } = null!;
    }

}

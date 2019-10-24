using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gene
{
    public class PaymentInformationModel
    { 
        public int SummaryDetailId { get; set; }
        public string TransactionId { get; set; }

        public string PaymentId { get; set; }
        public string TransactionAmount { get; set; }
        public string TerminalId { get; set; }
        public string CardNumber { get; set; }
        public int VehicleDetailId { get; set; }

        public string IceCashPolicyNumber { get; set; }
    }
}

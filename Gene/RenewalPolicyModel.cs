using GensureAPIv2.Models;
using InsuranceClaim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gene
{
   public class RenewalPolicyModel
    {
        public CustomerModel Cutomer { get; set; }
        public RiskDetailModel riskdetail { get; set; }

        public SummaryDetailModel SummaryDetails { get; set; }

        public string ErrorMessage { get; set; }
    }
}

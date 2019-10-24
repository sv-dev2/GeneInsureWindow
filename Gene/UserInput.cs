using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gene
{
    class UserInput
    {
        public string VRN { get; set; }

        //RiskDetails
        public string SumInsured { get; set; }
        public string VehicalUsage { get; set; }
        public string PaymentTerm { get; set; }
        public int CoverType { get; set; }
        public int Currency { get; set; }


        //Vehicle Type
        public string Make { get; set; }
        public string MakeID { get; set; }
        public string Model { get; set; }
        public string ModelID { get; set; }
        public string Year { get; set; }
        public string ChasisNumber { get; set; }
        public string EngineNumber { get; set; }


        //PersonalDetails1
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }



        //PersonalDetails2
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string IDNumber { get; set; }

        
        //optionalCover
        public Int32 ExcessBuyback { get; set; }
        public Int32 RoadsideAssistance { get; set; }
        public Int32 MedicalExpenses { get; set; }
        public Int32 PassengerAccidentalCover { get; set; }
        public Int32 NumberOfPerson { get; set; }



    }
}

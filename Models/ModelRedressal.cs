using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintTracker.Models
{
    public class ModelRedressal
    {
        public string OFFICE_NAME { get; set; }
        public string COMPLAINTS_PENDING_TILL_FROM_DATE { get; set; }
        public string COMPLAINS_RECEIEVED_UPTO_MONTH_NO_CURRENT_COMPLAINT { get; set; }
        public string COMPLAINS_RECEIEVED_UPTO_MONTH_TRANSFORMER_FAILURE { get; set; }
        public string COMPLAINS_RECEIEVED_UPTO_MONTH_ENERGY_THEFT { get; set; }
        public string COMPLAINS_RECEIEVED_UPTO_MONTH_SAFETY_RELATED { get; set; }

        public string COMPLAINS_RECEIEVED_UPTO_MONTH_HARASSMENT_BY_OFFICIALS { get; set; }

        public string COMPLAINS_RECEIEVED_UPTO_MONTH_BILL_RELATED { get; set; }
        public string COMPLAINS_RECEIEVED_UPTO_MONTH_METER_RELATED { get; set; }
        public string COMPLAINS_RECEIEVED_UPTO_MONTH_OTHER_TRCHNICAL_COMPLAINTS { get; set; }


        public string COMPLAINS_RECEIEVED_UPTO_MONTH_TOTAL { get; set; }
        public string COMPLAINS_REDRESSED_UPTO_MONTH_NO_CURRENT_COMPLAINT { get; set; }
        public string COMPLAINS_REDRESSED_UPTO_MONTH_TRANSFORMER_FAILURE { get; set; }
        public string COMPLAINS_REDRESSED_UPTO_MONTH_ENERGY_THEFT { get; set; }

        public string COMPLAINS_REDRESSED_UPTO_MONTH_SAFETY_RELATED { get; set; }
        public string COMPLAINS_REDRESSED_UPTO_MONTH_HARASSMENT_BY_OFFICIALS { get; set; }
        public string COMPLAINS_REDRESSED_UPTO_MONTH_BILL_RELATED { get; set; }
        public string COMPLAINS_REDRESSED_UPTO_MONTH_METER_RELATED { get; set; }
        public string COMPLAINS_REDRESSED_UPTO_MONTH_OTHER_TRCHNICAL_COMPLAINTS { get; set; }
        public string COMPLAINS_REDRESSED_UPTO_MONTH_TOTAL { get; set; }
        public string BALANCE_NO_CURRENT_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string BALANCE_TRANSFORMER_FAILURE_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string BALANCE_ENERGY_THEFT_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string BALANCE_SAFETY_RELATED_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string BALANCE_HARASSMENT_BY_OFFICIALS_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string BALANCE_BILL_RELATED_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string BALANCE_METER_RELATED_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string BALANCE_OTHER_TECHNICAL_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string TOTAL_BALANCE_COMPLAINTS_TILL_TO_DATE { get; set; }
        public string NO_CURRENT_AVEG { get; set; }

        public string TRANSFORMER_FAILURE_AVEG { get; set; }
        public string ENERGY_THEFT { get; set; }
        public string SAFETY_RELATED_AVEG { get; set; }

        public string HARASSMENT_BY_OFFICIALS_AVEG { get; set; }

        public string BILL_RELATED_AVEG { get; set; }
        public string METER_RELATED_AVEG { get; set; }
        public string OTHER_TECHNICAL_COMPLAINTS_AVEG { get; set; }
    }
}
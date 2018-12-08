using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLogic
{
    public class CustomersCriteria
    {
        public int? age_from {get;set;}
        public int? age_to { get; set; }
        public decimal? income_from { get; set; }
        public decimal? income_to { get; set; }
        public string city { get; set; }
        public DateTime? insert_date { get; set; }
        public bool isLastMounth { get; set; }
        public bool isCitySelect { get; set; }
        public bool isAgeSelect { get; set; }
    }
}

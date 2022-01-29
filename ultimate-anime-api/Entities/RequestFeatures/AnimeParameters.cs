using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class AnimeParameters : RequestParameters
    {
        public AnimeParameters()
        {
            OrderBy = "name";
        }

        public DateTime MinDate { get; set; } = new DateTime();
        public DateTime MaxDate { get; set; } = DateTime.Now;
        public bool ValidDateRange => MaxDate > MinDate;
        public string SearchTerm { get; set; }
    }
}

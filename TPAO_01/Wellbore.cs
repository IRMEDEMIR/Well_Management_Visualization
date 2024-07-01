using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAO_01
{
    internal class Wellbore
    {
        // Private field
        private string _wellboreAdi;

        // Parameter Constructor
        public Wellbore(string wellboreAdi)
        {
            _wellboreAdi = wellboreAdi;
        }

        // Public property for WellboreAdi
        public string WellboreAdi
        {
            get { return _wellboreAdi; }
            set { _wellboreAdi = value; }
        }

        // Public property for Kuyular
        public Kuyu Kuyu { get; set; }
    }
}


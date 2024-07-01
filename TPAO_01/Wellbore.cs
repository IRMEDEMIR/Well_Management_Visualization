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
        private double? _derinlik;
        private string? _id;

        // Parameter Constructor
        public Wellbore(string wellboreAdi)
        {
            _wellboreAdi = wellboreAdi;
            _id = null;
            _derinlik = null;
        }

        // Public property for WellboreAdi
        public string WellboreAdi
        {
            get { return _wellboreAdi; }
            set { _wellboreAdi = value; }
        }

        public string? Id { get; set; }

        public double? Derinlik { get; set; }

        // Public property for Kuyular
        public Kuyu Kuyu { get; set; }
    }
}
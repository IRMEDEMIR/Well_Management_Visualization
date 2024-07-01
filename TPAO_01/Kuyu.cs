using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAO_01
{
    internal class Kuyu
    {
        //Variables
        private string _kuyuAdi;
        private double? _enlem;
        private double? _boylam;
        private string? _id;

        // Constructor
        public Kuyu(string kuyuAdi)
        {
            _kuyuAdi = kuyuAdi;
            _enlem = null;
            _boylam = null;
            _id = null;
        }
        //Properties
        public string KuyuAdi
        {
            get { return _kuyuAdi; }
            set { _kuyuAdi = value; }
        }

        public double? Enlem { get; set; }
        public double? Boylam { get; set; }

        public string? Id { get; set; }
        public Kuyu_Grubu Kuyu_Grubu { get; set; }
        public Saha Saha { get; set; }

    }
}
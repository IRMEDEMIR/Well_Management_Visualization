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

        // Constructor
        public Kuyu(string kuyuAdi)
        {
            _kuyuAdi = kuyuAdi;
        }
        //Properties
        public string KuyuAdi
        {
            get { return _kuyuAdi; }
            set { _kuyuAdi = value; }
        }
        public Kuyu_Grubu Kuyu_Grubu {get; set; }
        public Saha Saha { get; set; }

    }
}

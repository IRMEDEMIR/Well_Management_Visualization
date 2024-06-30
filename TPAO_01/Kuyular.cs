using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAO_01
{
    internal class Kuyular
    {
        //Variables
        private string _kuyuAdi;

        // Constructor
        public Kuyular(string kuyuAdi)
        {
            _kuyuAdi = kuyuAdi;
        }
        //Properties
        public string KuyuAdi
        {
            get { return _kuyuAdi; }
            set { _kuyuAdi = value; }
        }
        public Kuyu_Gruplari Kuyu_Gruplari {get; set; }
        public Sahalar Sahalar { get; set; }

    }
}

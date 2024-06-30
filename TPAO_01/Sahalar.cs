using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAO_01
{
    internal class Sahalar
    {
        //Variables
        private string _sahaAdi;

        //Constructor
        public Sahalar(string sahaAdi)
        {
            _sahaAdi = sahaAdi;
        }
        public Sahalar()
        {
            _sahaAdi = null;
        }

        //Properties
        public string SahaAdi
        {
            get { return _sahaAdi; }
            set { _sahaAdi = value; }
        }
    }
}

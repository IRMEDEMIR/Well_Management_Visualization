using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAO_01
{
    internal class Kuyu_Grubu
    {
        //Variables
        private string _kuyuGrubuAdi;

        //Constructors

        //Parameter Constructor
        public Kuyu_Grubu(string kuyuGrubuAdi)
        {
            _kuyuGrubuAdi = kuyuGrubuAdi;
        }

        //Properties
        public Saha Saha { get; set; }
        public string KuyuGrubuAdi
        {
            get { return _kuyuGrubuAdi; }
            set { _kuyuGrubuAdi = value; }
        }
    }
}

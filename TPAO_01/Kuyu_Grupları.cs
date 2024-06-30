using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAO_01
{
    internal class Kuyu_Gruplari
    {
        //Variables
        private string _kuyuGrubuAdi;

        //Constructors

        //Parameter Constructor
        public Kuyu_Gruplari(string kuyuGrubuAdi)
        {
            _kuyuGrubuAdi = kuyuGrubuAdi;
        }

        //Properties
        public Sahalar Sahalar { get; set; }
        public string KuyuGrubuAdi
        {
            get { return _kuyuGrubuAdi; }
            set { _kuyuGrubuAdi = value; }
        }
    }
}

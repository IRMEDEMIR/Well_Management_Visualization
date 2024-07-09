namespace ParsingProjectMVC.Models
{
    public class KuyuModel
    {/*
        //Variables
        private string _kuyuAdi;
        private double? _enlem;
        private double? _boylam;
        private static int _idCounter = 1;  //ID sayacı

        // Constructor
        public KuyuModel(string kuyuAdi)
        {
            _kuyuAdi = kuyuAdi;
            _enlem = null;
            _boylam = null;
            Id = _idCounter++;
        }

        public KuyuModel()
        {
            _kuyuAdi = null;
        }*/
        //Properties
        public int Id { get; set; } 
        public string KuyuAdi { get; set; }

        public double? Enlem { get; set; }
        public double? Boylam { get; set; }

        public KuyuGrubuModel KuyuGrubuAdi { get; set; }
        public SahaModel SahaAdi { get; set; }

    }
}

namespace ParsingProjectMVC.Models
{
    public class KuyuModel
    {
        //Variables
        private string _kuyuAdi;
        private double? _enlem;
        private double? _boylam;
        private string? _id;

        // Constructor
        public KuyuModel(string kuyuAdi)
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
        public KuyuGrubuModel Kuyu_Grubu { get; set; }
        public SahaModel Saha { get; set; }

    }
}

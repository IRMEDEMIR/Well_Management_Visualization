namespace ParsingProjectMVC.Models
{
    public class WellboreModel
    {
        // Private field
        private string _wellboreAdi;
        private double? _derinlik;
        private string? _id;

        // Parameter Constructor
        public WellboreModel(string wellboreAdi)
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
        public KuyuModel Kuyu { get; set; }
    }
}

namespace ParsingProjectMVC.Models
{
    public class SahaModel
    {
        // Variables
        private string _sahaAdi;
        private static int _idCounter = 1; // ID sayacı

        // Constructor
        public SahaModel(string sahaAdi)
        {
            _sahaAdi = sahaAdi;
            Id = _idCounter++;
        }

        public SahaModel()
        {
            _sahaAdi = null;
            Id = _idCounter++;
        }

        // Properties
        public int Id { get; private set; } // Private setter
        public string SahaAdi
        {
            get { return _sahaAdi; }
            set { _sahaAdi = value; }
        }
    }
}

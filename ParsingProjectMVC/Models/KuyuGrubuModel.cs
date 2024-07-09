namespace ParsingProjectMVC.Models
{
    public class KuyuGrubuModel
    {
        //Variables
        private string _kuyuGrubuAdi;
        private static int _idCounter = 1; // ID sayacı

        //Constructors
        public KuyuGrubuModel(string kuyuGrubuAdi)
        {
            _kuyuGrubuAdi = kuyuGrubuAdi;
            Id = _idCounter++;
        }

        public KuyuGrubuModel()
        {
            _kuyuGrubuAdi = null;
            Id = _idCounter++;
        }
        //Properties
        public int Id { get; private set; } // Private setter
        public string KuyuGrubuAdi
        {
            get { return _kuyuGrubuAdi; }
            set { _kuyuGrubuAdi = value; }
        }
    }
}

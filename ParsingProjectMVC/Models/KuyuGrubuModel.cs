namespace ParsingProjectMVC.Models
{
    public class KuyuGrubuModel
    {
        //Variables
        private string _kuyuGrubuAdi;

        //Constructors

        //Parameter Constructor
        public KuyuGrubuModel(string kuyuGrubuAdi)
        {
            _kuyuGrubuAdi = kuyuGrubuAdi;
        }

        //Properties
        public SahaModel Saha { get; set; }
        public string KuyuGrubuAdi
        {
            get { return _kuyuGrubuAdi; }
            set { _kuyuGrubuAdi = value; }
        }
    }
}

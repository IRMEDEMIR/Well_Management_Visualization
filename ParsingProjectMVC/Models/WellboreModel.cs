namespace ParsingProjectMVC.Models
{
    public class WellboreModel
    {
        public string WellboreAdi { get; set; }
        public int Id { get; set; }
        public string Derinlik { get; set; } 
        public SahaModel SahaAdi { get; set; }
        public KuyuModel KuyuAdi { get; set; }
        public KuyuGrubuModel KuyuGrubuAdi { get; set; }
    }
}
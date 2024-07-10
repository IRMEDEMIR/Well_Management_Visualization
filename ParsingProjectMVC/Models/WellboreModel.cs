namespace ParsingProjectMVC.Models
{
    public class WellboreModel
    {
        public string WellboreAdi { get; set; }
        public int Id { get; set; }
        public int? Derinlik { get; set; } // Derinlik now int
        public SahaModel SahaAdi { get; set; }
        public KuyuModel KuyuAdi { get; set; }
        public KuyuGrubuModel KuyuGrubuAdi { get; set; }
    }
}

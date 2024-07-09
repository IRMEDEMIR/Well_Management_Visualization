namespace ParsingProjectMVC.Models
{
    public class KuyuModel
    {
        public int Id { get; set; } 
        public string KuyuAdi { get; set; }

        public double? Enlem { get; set; }
        public double? Boylam { get; set; }

        public KuyuGrubuModel KuyuGrubuAdi { get; set; }
        public SahaModel SahaAdi { get; set; }

    }
}

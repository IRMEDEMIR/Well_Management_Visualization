using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsingProjectMVC.Models
{
    public class KuyuModel
    {
        [Key]
        public int KuyuId { get; set; } 
        public string KuyuAdi { get; set; }

        public string Enlem { get; set; }
        public string Boylam { get; set; }

        [ForeignKey("KuyuGrubuId")]
        public KuyuGrubuModel KuyuGrubu { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsingProjectMVC.Models
{
    public class KuyuGrubuModel
    {
        [Key]
        public int KuyuGrubuId { get; set; } 
        public string KuyuGrubuAdi { get; set; }
        [ForeignKey("SahaId")]
        public SahaModel Saha { get; set; }
    }
}

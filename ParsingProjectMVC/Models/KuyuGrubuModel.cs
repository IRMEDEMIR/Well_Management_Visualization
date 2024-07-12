using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsingProjectMVC.Models
{
    public class KuyuGrubuModel
    {
       
        public int Id { get; set; } 
        public string KuyuGrubuAdi { get; set; }
        //[ForeignKey("SahaId")]
        public int SahaId { get; set; }
        public SahaModel Saha { get; set; }
    }
}

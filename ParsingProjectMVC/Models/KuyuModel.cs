using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsingProjectMVC.Models
{
    public class KuyuModel
    {
        
        public int Id { get; set; } 
        public string KuyuAdi { get; set; }

        public string Enlem { get; set; }
        public string Boylam { get; set; }

        //[ForeignKey("Id")]
        public int KuyuGrubuId { get; set; }    
        public KuyuGrubuModel KuyuGrubu { get; set; }
    }
}

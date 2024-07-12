using System.ComponentModel.DataAnnotations;

namespace ParsingProjectMVC.Models
{
    public class SahaModel
    {
        [Key]
        public int SahaId { get; set; } 
        public string SahaAdi { get; set; }
    }
}

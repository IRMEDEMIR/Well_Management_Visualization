using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParsingProjectMVC.Models
{
    public class WellboreModel
    {
        [Key]
        public int WellboreId { get; set; }
        public string WellboreAdi { get; set; }
        
        public string Derinlik { get; set; }
        [ForeignKey("KuyuId")]
        public KuyuModel Kuyu { get; set; }
    }
}
namespace ParsingProjectMVC.Models
{
    public class KuyuJsonModel
    {
        public string WellName { get; set; }
        public string DepthUnit { get; set; }
        public double[] WellHeadPos { get; set; }
        public string[] PathHeader { get; set; }
        public double[][] Path { get; set; }
    }
}

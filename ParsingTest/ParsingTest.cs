using Xunit;
using tpao_project_01;

namespace tpao_project_01
{
    public class ParsingTest
    {
        [Fact]
        public void ParseSahaAdi_ShouldReturnCorrectResult_FoComlpexInput()
        {
            // Arrange
            string line = "AD/?A14N~~A-36>";
            string line1 = "ADANA-36/K8/S12/R25";
            string line2 = "ADANA-UVI/K2/S3";
            string line3 = "36";
            string line4 = "ADANA-36/T/M";
            string line5 = "ADANA-36/S1/R1/K1";
            string line6 = "ADANA-36/K1/11S";
            string line7 = "TUTI KUMUKEM-22/K";
            string line8 = "tuTI kumukEM-22/K";

            // Act
            string result = Program.SahaOlustur(line);
            string result1 = Program.SahaOlustur(line1);
            string result2 = Program.SahaOlustur(line2);
            string result3 = Program.SahaOlustur(line3);
            string result4 = Program.SahaOlustur(line4);
            string result5 = Program.SahaOlustur(line5);
            string result6 = Program.SahaOlustur(line6);
            string result7 = Program.SahaOlustur(line7);
            string result8 = Program.SahaOlustur(line8);

            // Assert
            Assert.Equal("Error", result);
            Assert.Equal("ADANA", result1);
            Assert.Equal("Error", result2);
            Assert.Equal("Error", result3);
            Assert.Equal("Error", result4);
            Assert.Equal("Error", result5);
            Assert.Equal("Error", result6);
            Assert.Equal("TUTI KUMUKEM", result7);
            Assert.Equal("Error", result8);
        }

        [Fact]
        public void ParseKuyuGrubuAdi_ShouldReturnCorrectResult_ForComplexInput()
        {
            // Arrange
            string line = "ADANA-3ab6c";
            string line1 = "ADANA-36";
            string line2 = "ADANA-36/T/M";
            string line3 = "AD/?A14N~~A-36>";
            string line4 = "ADANA-36/S1/R1/K1";
            string line5 = "ADANA-36/K1/11S";

            // Act
            string result = Program.KuyuGrubuOlustur(line);
            string result1 = Program.KuyuGrubuOlustur(line1);
            string result2 = Program.KuyuGrubuOlustur(line2);
            string result3 = Program.KuyuGrubuOlustur(line3);
            string result4 = Program.KuyuGrubuOlustur(line4);
            string result5 = Program.KuyuGrubuOlustur(line5);

            // Assert
            Assert.Equal("Error", result);
            Assert.Equal("ADANA-36", result1);
            Assert.Equal("Error", result2);
            Assert.Equal("Error", result3);
            Assert.Equal("Error", result4);
            Assert.Equal("Error", result5);
        }

        [Fact]
        public void ParseKuyuAdi_ShouldReturnCorrectResult_ForComplexInput()
        {
            // arrange

            string line = "ADANA-36/S1/R1/K1";
            string line1 = "ADANA-36/K1/M4S";
            string line2 = "ADANA-36/K11";
            string line3 = "AD/?A14N~~A-36>";
            string line4 = "ADANA-36/S/M";
            string line5 = "ADANA-UVI/K2/S3";
            string line6 = "ADANA-36/T/S";

            // act
            List<string> result = Program.KuyuOlustur(line);
            List<string> result1 = Program.KuyuOlustur(line1);
            List<string> result2 = Program.KuyuOlustur(line2);
            List<string> result3 = Program.KuyuOlustur(line3);
            List<string> result4 = Program.KuyuOlustur(line4);
            List<string> result5 = Program.KuyuOlustur(line5);
            List<string> result6 = Program.KuyuOlustur(line5);

            // assert
            var expected = new List<string> { "Error" };
            var expected1 = new List<string> { "Error" };
            var expected2 = new List<string> { "ADANA-36", "ADANA-36/K11", "ADANA-36/K10", "ADANA-36/K9", "ADANA-36/K8", "ADANA-36/K7", "ADANA-36/K6", "ADANA-36/K5", "ADANA-36/K4", "ADANA-36/K3", "ADANA-36/K2", "ADANA-36/K1", "ADANA-36/K" };
            var expected3 = new List<string> { "Error" };
            var expected4 = new List<string> { "ADANA-36" };
            var expected5 = new List<string> { "Error" };
            var expected6 = new List<string> { "Error" };

            Assert.Equal(13, result2.Count);

            Assert.Equal(expected, result);
            Assert.Equal(expected1, result1);
            Assert.Equal(expected2, result2);
            Assert.Equal(expected3, result3);
            Assert.Equal(expected4, result4);
            Assert.Equal(expected5, result5);
            Assert.Equal(expected6, result6);

        }

        [Fact]
        public void ParseWellboreComponents_ShouldReturnCorrectResult_ForComplexInput()
        {
            // Arrange
            string line = "ADANA-36/K1/S11";
            string line1 = "ADANA-36/KA/S11";
            string line2 = "ADANA-36/T/S11";
            string line3 = "ADANA-36/K1/S11M";
            string line4 = "ADANA-36/S1/R1/K1";
            string line5 = "ADANA-36/K1/11S";

            // Act
            List<string> result = Program.WellboreOlustur(line);
            List<string> result1 = Program.WellboreOlustur(line1);
            List<string> result2 = Program.WellboreOlustur(line2);
            List<string> result3 = Program.WellboreOlustur(line3);
            List<string> result4 = Program.WellboreOlustur(line4);
            List<string> result5 = Program.WellboreOlustur(line5);

            // Assert
            Assert.Equal(15, result.Count);

            Assert.Equal("Error", result1[0]);
            Assert.Equal("Error", result2[0]);
            Assert.Equal("Error", result3[0]);
            Assert.Equal("Error", result4[0]);
            Assert.Equal("Error", result5[0]);

            Assert.Equal("ADANA-36/K1/S11", result[0]);
            Assert.Equal("ADANA-36/K1/S10", result[1]);
            Assert.Equal("ADANA-36/K1/S9", result[2]);
            Assert.Equal("ADANA-36/K1/S8", result[3]);
            Assert.Equal("ADANA-36/K1/S7", result[4]);
            Assert.Equal("ADANA-36/K1/S6", result[5]);
            Assert.Equal("ADANA-36/K1/S5", result[6]);
            Assert.Equal("ADANA-36/K1/S4", result[7]);
            Assert.Equal("ADANA-36/K1/S3", result[8]);
            Assert.Equal("ADANA-36/K1/S2", result[9]);
            Assert.Equal("ADANA-36/K1/S1", result[10]);
            Assert.Equal("ADANA-36/K1/S", result[11]);
            Assert.Equal("ADANA-36/K1", result[12]);
            Assert.Equal("ADANA-36/K", result[13]);
            Assert.Equal("ADANA-36", result[14]);

  
        }
    }
}
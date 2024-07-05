using Xunit;
using tpao_project_01; 

namespace tpao_project_01 // Test sýnýfýnýn namespace'i
{
    public class ParsingTest
    {
        [Fact]
        public void ParseSahaAdi_ShouldReturnCorrectResult_FoComlpexInput()
        {
            // Arrange
            string line1 = "AD/?A14N~~A-36>";
            string line2 = "ADANA-36/K8/S12/R25";
            string line3 = "ADANA-UVI/K2/S3";

            // Act
            string result1 = Program.SahaOlustur(line1);
            string result2 = Program.SahaOlustur(line2);
            string result3 = Program.SahaOlustur(line3);

            // Assert
            Assert.Equal("Error", result1); // Saha adi kisminda hata var mý?
            Assert.Equal("ADANA", result2); // Saha adi kisminda hata var mý?
            Assert.Equal("Error", result3); // Saha adi kisminda hata var mý?
        }

        [Fact]
        public void ParseKuyuGrubuAdi_ShouldReturnCorrectResult_ForComplexInput()
        {
            // Arrange
            string line = "ADANA-3ab6c";
            string line1 = "ADANA-36";

            // Act
            string result = Program.KuyuGrubuOlustur(line);
            string result1 = Program.KuyuGrubuOlustur(line1);

            // Assert
            Assert.Equal("Error", result);// Kuyu_Grubu kisminda hata var mý?
            Assert.Equal("ADANA-36", result1);// Kuyu_Grubu kisminda hata var mý?

        }

        [Fact]
        public void ParseKuyuAdi_ShouldReturnCorrectResult_ForComplexInput()
        {
            // arrange

            string line = "ADANA-36/S/M";
            string line1 = "ADANA-36/K1S";
            string line2 = "ADANA-36/K1";
           // string line3 = "AD/?A14N~~A-36>";

            // act
            List<string> result = Program.KuyuOlustur(line);
            List<string> result1 = Program.KuyuOlustur(line1);
            List<string> result2 = Program.KuyuOlustur(line2);
           // List<string> result3 = Program.KuyuOlustur(line3);

            // assert
            var expected = new List<string> { "ADANA-36" };
            var expected1 = new List<string> { "Error" };
            var expected2 = new List<string> { "ADANA-36", "ADANA-36/K1", "ADANA-36/K" };
            //var expected3 = new List<string> { "Error" };


            Assert.Equal(expected, result);
            Assert.Equal(expected1, result1);
            Assert.Equal(expected2, result2);
            //Assert.Equal(expected3, result3);

        }

        [Fact]
        public void ParseWellboreComponents_ShouldReturnCorrectResult_ForComplexInput()
        {
            // Arrange
            string line = "ADANA-36/K1/S11";
            string line1 = "ADANA-36/KA/S11";
            string line2 = "ADANA-36/T/S11";


            // Act
            List<string> result = Program.WellboreOlustur(line);
            List<string> result1 = Program.WellboreOlustur(line1);
            List<string> result2 = Program.WellboreOlustur(line2);

            // Assert
            Assert.Equal(15, result.Count);

            Assert.Equal("Error", result1[0]);

            Assert.Equal("Error", result2[0]);

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


        //[Fact]
        ////public void ParseWellboreComponents_ShouldReturnCorrectResult_ForSimpleInput()
        //{
        //    // Arrange
        //    string line = "ADANA-36";

        //    // Act
        //    string[] result = Program.KuyuOlustur(line);

        //    // Assert
        //    Assert.Single(result);
        //    Assert.Equal("36", result[0]);
        //}



        //[Fact]
        //public void ParseKuyuGrubuAdi_ShouldReturnCorrectResult_ForComplexInput()
        //{
        //    // Arrange
        //    string line = "ADANA-36/K1/S5";

        //    // Act
        //    string result = Program.ParseKuyuGrubuAdi(line);

        //    // Assert
        //    Assert.Equal("36", result);
        //}



        //[Fact]
        //public void ParseKuyuGrubuAdi_ShouldReturnCorrectResult_ForEmptyWellboreComponents()
        //{
        //    // Arrange
        //    string line = "ADANA-";

        //    // Act
        //    string result = Program.ParseKuyuGrubuAdi(line);

        //    // Assert
        //    Assert.Equal("", result);
        //}

        //[Fact]
        //public void ParseWellboreComponents_ShouldReturnCorrectResult_ForEmptyWellboreComponents()
        //{
        //    // Arrange
        //    string line = "ADANA-";

        //    // Act
        //    string[] result = Program.ParseWellboreComponents(line);

        //    // Assert
        //    Assert.Empty(result);
        //}
    }
}
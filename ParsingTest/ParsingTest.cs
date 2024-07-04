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

            // Act
            string result1 = Program.SahaOlustur(line1);
            string result2 = Program.SahaOlustur(line2);

            // Assert
            Assert.Equal("Error", result1); // Saha adi kisminda hata var mý?
            Assert.Equal("ADANA", result2); // Saha adi kisminda hata var mý?

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
        //public void ParseWellboreComponents_ShouldReturnCorrectResult_ForComplexInput()
        //{
        //    // Arrange
        //    string line = "ADANA-36/K1/S5";

        //    // Act
        //    string[] result = Program.ParseWellboreComponents(line);

        //    // Assert
        //    Assert.Equal(3, result.Length);
        //    Assert.Equal("36", result[0]);
        //    Assert.Equal("K1", result[1]);
        //    Assert.Equal("S5", result[2]);
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
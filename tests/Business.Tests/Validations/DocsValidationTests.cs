using WebApiProductsProviders.Business.Models.Validations.Documents;
using Xunit;

namespace Business.Tests.Validations
{
    public class DocsValidationTests
    {
        [Theory]
        [InlineData("097.875.410-77", "12345")]
        [InlineData("05144039006", "abcdefghijl")]
        [InlineData("879963250-06", "12345678901")]
        public void CpfValidation_Validate_ShouldValidateIfCpfIsValid(string validCpf, string invalidCpf)
        {
            // Act
            var validResult = CpfValidation.Validate(validCpf);
            var invalidResult = CpfValidation.Validate(invalidCpf);

            // Assert
            Assert.True(validResult);
            Assert.False(invalidResult);
        }

        [Fact]
        public void CnpjValidation_Validate_ShouldValidateIfCnpjIsValid()
        {
            // Arrange
            var validCnpj = "72.279.733/0001-18";
            var invalidCnpj = "12345";

            // Act
            var validResult = CnpjValidation.Validate(validCnpj);
            var invalidResult = CnpjValidation.Validate(invalidCnpj);

            // Assert
            Assert.True(validResult);
            Assert.False(invalidResult);
        }
    }
}

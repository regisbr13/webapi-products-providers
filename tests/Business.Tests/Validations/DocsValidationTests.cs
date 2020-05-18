using WebApiProductsProviders.Business.Models.Validations.Documents;
using Xunit;

namespace Business.Tests.Validations
{
    public class DocsValidationTests
    {
        [Trait("Validations", "Documents")]
        [Theory(DisplayName = "Verifica se o Cpf é válido")]
        [InlineData("097.875.410-77", "12345")]
        [InlineData("05144039006", "abcdefghijl")]
        [InlineData("879963250-06", "12345678901")]
        public void CpfValidation_Validate_ShouldCheckIfCpfIsValid(string validCpf, string invalidCpf)
        {
            // Act
            var validResult = CpfValidation.Validate(validCpf);
            var invalidResult = CpfValidation.Validate(invalidCpf);

            // Assert
            Assert.True(validResult);
            Assert.False(invalidResult);
        }

        [Trait("Validations", "Documents")]
        [Fact(DisplayName = "Verifica se o Cnpj é válido")]
        public void CnpjValidation_Validate_ShouldCheckIfCnpjIsValid()
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

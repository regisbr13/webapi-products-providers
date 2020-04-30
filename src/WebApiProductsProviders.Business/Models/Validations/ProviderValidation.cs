using FluentValidation;
using WebApiProductsProviders.Business.Models.Enums;
using WebApiProductsProviders.Business.Models.Validations.Documents;

namespace WebApiProductsProviders.Business.Models.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O campo Nome não pode ser vazio")
                .Length(3, 100).WithMessage("O campo Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(p => p.ProviderType == ProviderType.LegalPerson, () => {
                RuleFor(p => p.DocumentNumber.Length).Equal(CnpjValidation.TamanhoCnpj).WithMessage("O campo Cnpj precisa ter {ComparisonValue} caracteres");
                RuleFor(p => CnpjValidation.Validar(p.DocumentNumber)).Equal(true).WithMessage("O Cnpj fornecido é inválido");
            });
            When(p => p.ProviderType == ProviderType.PhysicalPerson, () => {
                RuleFor(p => p.DocumentNumber.Length).Equal(CpfValidation.TamanhoCpf).WithMessage("O campo Cpf precisa ter {ComparisonValue} caracteres");
                RuleFor(p => CpfValidation.Validar(p.DocumentNumber)).Equal(true).WithMessage("O Cpf fornecido é inválido");
            });
        }
    }
}

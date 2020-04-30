using FluentValidation;

namespace WebApiProductsProviders.Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("O campo Rua precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo Rua precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.District)
                .NotEmpty().WithMessage("O campo Bairro precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo Bairro precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage("O campo Cep precisa ser fornecido")
                .Length(8).WithMessage("O campo Cep precisa ter {MaxLength} caracteres");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("A campo Cidade precisa ser fornecida")
                .Length(2, 100).WithMessage("O campo Cidade precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("O campo Estado precisa ser fornecido")
                .Length(2).WithMessage("O campo Estado no máximo {MaxLength} caracteres");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("O campo Número precisa ser fornecido");
        }
    }
}

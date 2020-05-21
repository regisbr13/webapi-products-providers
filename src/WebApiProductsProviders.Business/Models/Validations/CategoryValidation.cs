using FluentValidation;

namespace WebApiProductsProviders.Business.Models.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("O campo Nome precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo Nome precisa ter de {MinLength} a {MaxLength} caracteres");
        }
    }
}

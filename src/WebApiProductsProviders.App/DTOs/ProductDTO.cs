using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiProductsProviders.App.DTOs
{
    public class ProductDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "o campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "o campo {0} é obrigatório")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "o campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [Range(1.0, 1000000.0, ErrorMessage = "o campo {0} não pode ser nulo")]
        public decimal Value { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime Register { get; set; }

        [Display(Name = "Ativo?")]
        public bool Active { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "forneça o Id do {0}")]
        public string ProviderId { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "forneça o Id da {0}")]
        public string CategoryId { get; set; }

        public string Image { get; set; }
        public string ImageUpload { get; set; }
        public string Provider { get; set; }
        public string Category { get; set; }
    }
}

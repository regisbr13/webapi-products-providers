using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiProductsProviders.App.DTOs
{
    public class CategoryDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "o campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "o campo precisa ter entre {2} e {1} caracteres")]
        public string Name { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}

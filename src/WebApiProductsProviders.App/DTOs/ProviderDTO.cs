﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiProductsProviders.App.DTOs
{
    public class ProviderDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "o campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "CPF/CNPJ")]
        [Required(ErrorMessage = "o campo {0} é obrigatório")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Tipo")]
        [Range(1, 2, ErrorMessage = "Entre com {1} para Pessoa Física e {2} para Pessoa Jurídica")]
        public int ProviderType { get; set; }

        [Display(Name = "Ativo?")]
        public bool Active { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public AddressDTO Address { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}

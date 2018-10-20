using System;
using System.ComponentModel.DataAnnotations;

namespace AcessoPagador.Contracts
{
    public class Customer
    {
        [Required(ErrorMessage = "O Nome do comprador é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(255)]
        [Display(Name = "Nome do comprador")]
        public string Name { get; set; }

    }
}

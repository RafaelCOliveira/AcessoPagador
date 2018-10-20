using System;
using System.ComponentModel.DataAnnotations;

namespace AcessoPagador.Contracts
{
    public class Venda
    {
        [Required(ErrorMessage = "O Numero do Pedido é obrigatório")]
        [Display(Name = "Numero do Pedido")]
        public string MerchantOrderId { get; set; }
        public Customer customer { get; set; }
        public Payment payment { get; set; }
    }
}
﻿using System;
using System.ComponentModel.DataAnnotations;
using AcessoPagador.Contracts.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AcessoPagador.Contracts
{
    public class CreditCard
    {
        [Required(ErrorMessage = "O Numero do cartão é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(16)]
        [Display(Name = "Numero do cartão")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "O Nome impresso no cartão é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(25)]
        [Display(Name = "Nome impresso no cartão")]
        public string Holder { get; set; }

        [Required(ErrorMessage = "A Validade do cartão é obrigatória", AllowEmptyStrings = false)]
        [MaxLength(7)]
        [Display(Name = "Validade do cartão")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "O Codigo de segurança do cartão é obrigatório", AllowEmptyStrings = false)]
        [MaxLength(4)]
        [Display(Name = "Codigo de segurança")]
        public string SecurityCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [Required(ErrorMessage = "A Bandeira do cartão é obrigatória")]
        [Display(Name = "Bandeira do Cartão")]
        public BrandEnum Brand { get; set; }

    }
}
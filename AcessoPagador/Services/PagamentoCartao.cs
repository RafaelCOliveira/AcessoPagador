using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AcessoPagador.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AcessoPagador.Services
{
    public class PagamentoCartao : IPagamentoService
    {
        private readonly HttpClient Client;
        private IConfiguration _configuration;

        public PagamentoCartao(IConfiguration Configuration)
        {
            _configuration = Configuration;
            Client = new HttpClient();
            AddHeaders();
        }

        public async Task<Payment> Listar()
        {
            List<Venda> ListVenda = new List<Venda>();
            Client.BaseAddress = new Uri(_configuration["EndPoint:Consulta"]);
            var response = await Client.GetAsync("v2/sales?merchantOrderId=" + _configuration["merchantOrderId"]);
            if (response.IsSuccessStatusCode)
            {
                var retorno = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Payment>(retorno);
            }
            return new Payment();
        }

        public async Task<Venda> Consultar(String PaymentId)
        {
            if (Client.BaseAddress == null)
                Client.BaseAddress = new Uri(_configuration["EndPoint:Consulta"]);

            var response = await Client.GetAsync("v2/sales/" + PaymentId);
            if (response.IsSuccessStatusCode)
            {
                var retorno = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Venda>(retorno);
            }
            return new Venda();
        }

        public async Task<Venda> Pagar(Venda venda)
        {
            Client.BaseAddress = new Uri(_configuration["EndPoint:Transacional"]);

            var response = await Client.PostAsync("v2/sales/", ConverObject(venda));
            if (response.IsSuccessStatusCode)
            {
                var retorno = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Venda>(retorno);
            }
            return new Venda();
        }

        public async Task Capturar(String PaymentId)
        {
            Client.BaseAddress = new Uri(_configuration["EndPoint:Transacional"]);
            var response = await Client.PutAsync("v2/sales/" + PaymentId.ToString() + "/capture", null);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
                //return JsonConvert.DeserializeObject<Venda>(retorno);
            }
            //return new Venda();
        }

        public async Task Cancelar(string PaymentId)
        {
            Client.BaseAddress = new Uri(_configuration["EndPoint:Transacional"]);
            var response = await Client.PutAsync("v2/sales/" + PaymentId.ToString() + "/void", null);
            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsStringAsync();
                //return JsonConvert.DeserializeObject<Venda>(retorno);
            }
            //return new Venda();
        }

        private void AddHeaders()
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("MerchantId", _configuration["Merchant:MerchantId"]);
            Client.DefaultRequestHeaders.Add("MerchantKey", _configuration["Merchant:MerchantKey"]);
        }

        private static StringContent ConverObject(Venda venda)
        {
            var json = JsonConvert.SerializeObject(venda);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            return stringContent;
        }
    }
}
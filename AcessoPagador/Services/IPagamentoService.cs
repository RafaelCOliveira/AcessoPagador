using System.Collections.Generic;
using System.Threading.Tasks;
using AcessoPagador.Contracts;

namespace AcessoPagador.Services
{
    public interface IPagamentoService
    {
        Task<Payment> Listar();
        Task<Venda> Pagar(Venda venda);
        Task<Venda> Consultar(string PaymentId);
        Task Capturar(string PaymentId);
        Task Cancelar(string PaymentId);

    }
}
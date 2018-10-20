using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcessoPagador.Models;
using AcessoPagador.Contracts;
using AcessoPagador.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using AcessoPagador.Contracts.Enum;

namespace AcessoPagador.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly IPagamentoService _pagamento;

        private IConfiguration _configuration;

        public PagamentoController(IPagamentoService pagamento,IConfiguration Configuration)
        {
            _pagamento = pagamento;
            _configuration = Configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Venda> ListVenda = new List<Venda>();
            var Payments = await _pagamento.Listar();
            foreach (Payment Pay in Payments.Payments)
                {
                    Venda Ret = await _pagamento.Consultar(Pay.PaymentId.ToString());
                    ListVenda.Add(Ret);
                }
            return View(ListVenda);
        }

        [HttpGet]
        public IActionResult Pagar()
        {
            ViewBag.MerchantOrderId = _configuration["merchantOrderId"];
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Pagar(Venda venda)
        {            
            if (ModelState.IsValid)
            {
                var _venda = await _pagamento.Pagar(venda);   
                if(_venda == null)   
                    return View(venda);

               return RedirectToAction("Visualizar","Pagamento", new {id = _venda.payment.PaymentId.ToString()});          
            }
            ViewBag.MerchantOrderId = venda.MerchantOrderId;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Visualizar(String id)
        {
            var venda = await _pagamento.Consultar(id);
            return View(venda);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Capturar(Venda venda)
        {            
            var _venda = await _pagamento.Capturar(venda.payment.PaymentId.ToString());
            return RedirectToAction("Visualizar","Pagamento", new {id = venda.payment.PaymentId.ToString()});  
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Cancelar(Venda venda)
        {            
            var _venda = await _pagamento.Cancelar(venda.payment.PaymentId.ToString());
            return RedirectToAction("Visualizar","Pagamento", new {id = venda.payment.PaymentId.ToString()});  
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

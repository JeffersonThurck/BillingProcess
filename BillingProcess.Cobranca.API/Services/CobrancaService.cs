using BillingProcess.Cobrancas.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcess.Cobrancas.API.Services
{
    public class CobrancaService : ICobrancaService
    {
        private readonly HttpClient _httpClient;

        public CobrancaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> CalculaConsumo()
        {
            try
            {
                var result = await _httpClient.GetStringAsync("https://localhost:44394/api/cliente");

                var clientes = JsonConvert.DeserializeObject<List<Cliente>>(result);

                foreach (Cliente cliente in clientes)
                {
                    var valor = CalculaConsumo(cliente.CPF);

                    await SalvaCobranca(cliente, valor.Result);
                }

            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }

            return "Cálculo de consumo gerado com sucesso!";
        }

        private Task<double> CalculaConsumo(string cpf)
        {
            double valorFinal = 0;
            try
            {
                var valorString = cpf.Substring(0, 2) + cpf.Substring(Math.Max(0, cpf.Length - 2));

                valorFinal = Convert.ToDouble(valorString);

            }
            catch (Exception e)
            {
                throw e;
            }
            return Task.FromResult(valorFinal);
        }

        private async Task SalvaCobranca(Cliente cliente, double valor)
        {
            try
            {
                var cobranca = new Cobranca { Cliente = cliente, DataVencimento = DateTime.UtcNow.AddDays(30), Valor = valor };

                var myContent = JsonConvert.SerializeObject(cobranca);

                var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:44301/api/cobranca", stringContent);

            }
            catch (Exception exception)
            {
                throw exception;
            }

        }
    }
}

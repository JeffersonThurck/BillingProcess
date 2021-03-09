using BillingProcess.Cobrancas.API;
using BillingProcess.Cobrancas.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BillingProcess.Test
{
    public class CobrancaControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CobrancaControllerTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetAll_Cobrancas_Sucesso()
        {
            var response = await _client.GetAsync("https://localhost:44301/api/cobranca");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_CobrancaByCPF_Sucesso()
        {
            var response = await _client.GetAsync("https://localhost:44301/api/cobranca/38407848451");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_CobrancaByCPF_Erro()
        {
            var response = await _client.GetAsync("https://localhost:44301/api/cobranca/384026845");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Cobranca_Sucesso()
        {
            Cobranca cobranca = new Cobranca();
            cobranca.Cliente = new Cliente { CPF = "38402587840", Estado = "RS", Nome = "Jefferson" };
            cobranca.Valor = 788.00;
            cobranca.DataVencimento = DateTime.UtcNow;

            var myContent = JsonConvert.SerializeObject(cobranca);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:44301/api/cobranca", stringContent);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Cobranca_Erro()
        {
            Cobranca cobranca = new Cobranca();
            cobranca.Cliente = new Cliente { CPF = "38402587840", Estado = "RS", Nome = "Jefferson" };

            cobranca.DataVencimento = DateTime.UtcNow;

            var myContent = JsonConvert.SerializeObject(cobranca);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:44301/api/cobranca", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

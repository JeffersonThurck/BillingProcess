using BillingProcess.Cobrancas.API;
using BillingProcess.Cobrancas.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

using System.Threading.Tasks;
using Xunit;

namespace BillingProcess.Test.Services
{
    public class CobrancaServiceTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CobrancaServiceTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _server = new TestServer(new WebHostBuilder().UseStartup<Client.API.Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task CalculaConsumo_Sucesso()
        {
            CobrancaService cobrancaService = new CobrancaService(_client);
            var result = await cobrancaService.CalculaConsumo();

            Assert.Equal("Cálculo de consumo gerado com sucesso!", result);
        }
       
    }
}
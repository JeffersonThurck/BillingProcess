using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BillingProcess.Client.API.Models;
using BillingProcess.Client.API;
using Newtonsoft.Json;

namespace BillingProcess.Test
{
    public class ClienteControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ClienteControllerTest()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetAll_Clientes_Sucesso()
        {
            var response = await _client.GetAsync("https://localhost:44301/api/cliente");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_ClienteByCPF_Sucesso()
        {
            var response = await _client.GetAsync("https://localhost:44301/api/cliente/38407848451");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_ClienteByCPF_Erro()
        {
            var response = await _client.GetAsync("https://localhost:44301/api/cliente/3840");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Cliente_Sucesso()
        {
            Cliente cliente = new Cliente();
            cliente.Nome = "Cliente Teste";
            cliente.CPF = "37841297023";
            cliente.Estado = "SP";

            var myContent = JsonConvert.SerializeObject(cliente);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:44301/api/cliente", stringContent);
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Cliente_Erro()
        {
            Cliente cliente = new Cliente();
            cliente.Nome = "";
            cliente.CPF = "37841297845";
            cliente.Estado = "SP";

            var myContent = JsonConvert.SerializeObject(cliente);
            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://localhost:44301/api/cliente", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

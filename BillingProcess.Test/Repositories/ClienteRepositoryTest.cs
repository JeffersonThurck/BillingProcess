using BillingProcess.Client.API.Models;
using BillingProcess.Client.API.Dados.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BillingProcess.Test
{
    public class ClienteRepositoryTest
    {
        [Fact]
        public async Task Get_ClienteByCPF_Sucesso()
        {
            // Act
            ClienteRepository clienteRepository = new ClienteRepository();
            var cliente = await clienteRepository.GetByCpf("38402684515");

            // Assert
            Assert.Equal("Pedro", cliente.Nome);
            Assert.Equal("MG", cliente.Estado);
            Assert.Equal("38402684515", cliente.CPF);

            Assert.IsType<Cliente>(cliente);
        }

        [Fact]
        public async Task Add_Cliente_Sucesso()
        {
            Cliente cliente = new Cliente();
            cliente.Nome = "Jefferson";
            cliente.CPF = "45112018740";
            cliente.Estado = "SP";

            ClienteRepository clienteRepository = new ClienteRepository();
            var add = await clienteRepository.Add(cliente);

            Assert.IsType<Cliente>(cliente);

            Assert.Equal("Jefferson", cliente.Nome);
            Assert.Equal("SP", cliente.Estado);
            Assert.Equal("45112018740", cliente.CPF);  
        }

    }
}

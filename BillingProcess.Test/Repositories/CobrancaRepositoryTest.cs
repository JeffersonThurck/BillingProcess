using BillingProcess.Cobrancas.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BillingProcess.Cobrancas.API.Data.Repository;

namespace BillingProcess.Test
{
    public class CobrancaRepositoryTest
    {
        [Fact]
        public async Task Get_CobrancaByCPF_Sucesso()
        {
            // Act
            CobrancaRepository cobrancaRepository = new CobrancaRepository();
            var cobranca = await cobrancaRepository.GetByCpf("38402684515");

            Assert.IsType<double>(cobranca.Valor);
            Assert.IsType<DateTime>(cobranca.DataVencimento);
            Assert.IsType<Cliente>(cobranca.Cliente);

        }

        [Fact]
        public async Task Add_Cobranca_Sucesso()
        {
            Cobranca cobranca = new Cobranca();
            Cliente cliente = cobranca.Cliente = new Cliente { CPF = "38402587840", Estado = "RS", Nome = "Jefferson" };
            cobranca.Valor = 788.00;
            cobranca.DataVencimento = DateTime.UtcNow;

            CobrancaRepository cobrancaRepository = new CobrancaRepository();
            var add = await cobrancaRepository.Add(cobranca);

            Assert.IsType<Cobranca>(add);
        }
    }
}

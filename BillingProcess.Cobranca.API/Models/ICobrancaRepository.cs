using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingProcess.Cobrancas.API.Models;

namespace BillingProcess.Cobrancas.API.Models
{
    public interface ICobrancaRepository
    {
        Task<Cobranca> Add(Cobranca cobranca);
        Task<Cobranca> GetByCpf(string cpf);
        Task<List<Cobranca>> GetAll();

    }
}

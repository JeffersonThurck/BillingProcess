using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingProcess.Cobranca.API.Models;

namespace BillingProcess.Cobranca.API.Models
{
    public interface ICobrancaRepository
    {
        Task<Cobranca> Add(Cobranca cobranca);
        Task<Cobranca> GetByCpf(string cpf);
        Task<List<Cobranca>> GetAll();

    }
}

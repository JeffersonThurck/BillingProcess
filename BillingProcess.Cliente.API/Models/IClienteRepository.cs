using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Client.API.Models
{
    public interface IClienteRepository
    {
        Task<Cliente> Add(Cliente cliente);

        Task<Cliente> GetByCpf(string cpf);

        Task<List<Cliente>> GetAll();
    }
}

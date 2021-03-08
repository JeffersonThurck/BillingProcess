using BillingProcess.Client.API.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Client.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<Cliente> _validator;

        public ClienteController(IClienteRepository clienteRepository, IValidator<Cliente> validator)
        {
            _clienteRepository = clienteRepository;
            _validator = validator;
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> Get(string cpf)
        {
            var result = await _clienteRepository.GetByCpf(cpf);

            if (result.CPF == null)
            {
                return NotFound("Nenhum cliente foi encontrado");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            var result = await _clienteRepository.GetAll();

            if (result.Count == 0)
            {
                return NotFound("Não foi possível listar os clientes");
            }
            else
            {
                return Ok(result);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente cliente)
        {
            var validacao = _validator.Validate(cliente);

            var verificaCPF = _clienteRepository.GetByCpf(cliente.CPF);

            if (verificaCPF.Result.CPF == null && validacao.IsValid)
            {
                var result = await _clienteRepository.Add(cliente);
                return Ok(result);
            }
            else
            {
                return BadRequest("CPF já cadastrado!");
            }
        }
    }
}

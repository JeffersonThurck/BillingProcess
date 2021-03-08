using BillingProcess.Cobranca.API.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Cobranca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaRepository _cobrancaRepository;
        private readonly IValidator<Models.Cobranca> _validator;
        //private readonly ICobrancaService _cobrancaService;

        public CobrancaController(ICobrancaRepository cobrancaRepository, IValidator<Models.Cobranca> validator)
        {
            _cobrancaRepository = cobrancaRepository;
            _validator = validator;
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> Get(string cpf)
        {
            var result = await _cobrancaRepository.GetByCpf(cpf);

            if (result.Cliente == null)
            {
                return BadRequest("Nenhuma cobrança foi encontrada");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCobrancas()
        {
            var result = await _cobrancaRepository.GetAll();

            if (result.Count == 0)
            {
                return NotFound("Não foi possível listar as cobranças");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Cobranca cobranca)
        {
             var validaCobranca = _validator.Validate(cobranca);       

            if (validaCobranca.IsValid)
            {
                var result = await _cobrancaRepository.Add(cobranca);
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao cadastrar cobrança");
            }
        }
    }
}

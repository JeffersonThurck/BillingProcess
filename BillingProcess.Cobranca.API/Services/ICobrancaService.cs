﻿using BillingProcess.Cobrancas.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingProcess.Cobrancas.API.Services
{
    public interface ICobrancaService
    {
        Task<string> CalculaConsumo();
    }
}

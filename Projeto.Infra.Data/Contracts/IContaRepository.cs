﻿using Projeto.Infra.Data.DTOs;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contracts
{
    public interface IContaRepository : IBaseRepository<Conta>
    {
        List<ResumoContaDTO> GetResumoConta(); 
    }
}

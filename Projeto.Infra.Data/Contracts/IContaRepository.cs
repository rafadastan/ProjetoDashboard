using Projeto.Infra.Data.DTOs;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contracts
{
    public interface IContaRepository : IBaseRepository<Conta>
    {
        List<ResumoContaDTO> GetResumoConta();
        List<ResumoContaDTO> GetResumoConta(DateTime dataMin, DateTime dataMax);
        List<Conta> GetByDatas(DateTime dataMin, DateTime dataMax);
    }
}

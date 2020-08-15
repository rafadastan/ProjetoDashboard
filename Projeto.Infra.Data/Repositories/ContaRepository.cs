using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.DTOs;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class ContaRepository : BaseRepository<Conta>, IContaRepository
    {
        //atributo
        private readonly DataContext dataContext;

        //construtor para injeção de dependência
        public ContaRepository(DataContext dataContext)
            : base(dataContext) //construtor da superclasse
        {
            this.dataContext = dataContext;
        }

        public List<ResumoContaDTO> GetResumoConta()
        {
            return dataContext
                    .Conta
                    .Include(c => c.Categoria)
                    .GroupBy(c => c.Categoria.Nome)
                    .Select(
                        c => new ResumoContaDTO
                        {
                            NomeCategoria = c.Key,
                            Total = c.Sum(c => c.ValorConta)
                        }
                ).ToList();
        }

        public override List<Conta> GetAll()
        {
            return dataContext.Conta
                .Include(c => c.Categoria)
                .ToList();
        }

        public List<Conta> GetByDatas(DateTime dataMin, DateTime dataMax)
        {
            return dataContext.Conta
                .Include(c => c.Categoria)
                .Where(c => c.DataConta >= dataMin && c.DataConta <= dataMax)
                .ToList();
        }

        public List<ResumoContaDTO> GetResumoConta(DateTime dataMin, DateTime dataMax)
        {
            return dataContext
                    .Conta
                    .Include(c => c.Categoria)
                    .Where(c => c.DataConta >= dataMin && c.DataConta <= dataMax)
                    .GroupBy(c => c.Categoria.Nome)
                    .Select(
                        c => new ResumoContaDTO
                        {
                            NomeCategoria = c.Key,
                            Total = c.Sum(c => c.ValorConta)
                        }
                ).ToList();
        }
    }
}

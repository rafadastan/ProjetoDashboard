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
    }
}

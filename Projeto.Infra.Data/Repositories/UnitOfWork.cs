using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IContaRepository ContaRepository
        {
            get {
                return new ContaRepository(dataContext);
            } 
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return new CategoriaRepository(dataContext);
            }
        }

        public void SaveChanges()
        {
            dataContext.SaveChanges();
        }
    }
}

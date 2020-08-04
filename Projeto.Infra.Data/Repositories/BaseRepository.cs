using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data.Contexts;
using Projeto.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> 
        where TEntity : class
    {
        private readonly DataContext dataContext;

        public BaseRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Create(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Added;
        }

        public void Delete(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Deleted;
        }

        public List<TEntity> GetAll()
        {
            return dataContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return dataContext.Set<TEntity>().Find(id); 
        }

        public void Update(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contracts
{
    public interface IUnitOfWork
    {
        IContaRepository ContaRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }

        void SaveChanges();
    }
}

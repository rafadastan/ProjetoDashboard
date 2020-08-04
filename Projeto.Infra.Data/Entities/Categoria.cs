using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Entities
{
    public class Categoria
    {
        #region Propriedades

        public Guid IdCategoria { get; set; }
        public string Nome { get; set; }

        #endregion

        #region Relacionamentos

        public List<Conta> Conta { get; set; }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Entities
{
    public class Conta
    {
        #region Propriedades

        public Guid IdConta { get; set; }
        public string NomeConta { get; set; }
        public DateTime DataConta { get; set; }
        public decimal ValorConta { get; set; }
        public Guid IdCategoria { get; set; }

        #endregion

        #region Relacionamentos

        public Categoria Categoria { get; set; }

        #endregion
    }
}

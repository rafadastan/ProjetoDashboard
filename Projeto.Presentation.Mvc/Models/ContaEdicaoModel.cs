using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Models
{
    public class ContaEdicaoModel
    {
        public string IdConta { get; set; } //Campo Oculto

        [MinLength(6, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da conta.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da conta.")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da conta.")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Por favor, selecione a categoria da conta.")]
        public string IdCategoria { get; set; }

        #region Listagem de opções

        public List<SelectListItem> ItensCategoria { get; set; }

        #endregion
    }
}

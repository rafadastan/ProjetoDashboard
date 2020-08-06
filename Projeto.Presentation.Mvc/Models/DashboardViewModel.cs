using Projeto.Infra.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Models
{
    public class DashboardViewModel
    {
        public List<ResumoContaDTO> ResumoContas { get; set; }
    }
}

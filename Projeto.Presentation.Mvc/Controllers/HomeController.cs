using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Infra.Data.Contracts;
using Projeto.Presentation.Mvc.Models;

namespace Projeto.Presentation.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();

            try
            {
                model.ResumoContas = unitOfWork.ContaRepository.GetResumoConta();
            }
            catch (Exception e )
            {
                TempData["Mensagem"] = e.Message;
            }

            return View(model);
        }
    }
}
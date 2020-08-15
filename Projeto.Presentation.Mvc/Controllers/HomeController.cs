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
            return View();
        }

        //Metodo chamado no Javascript.
        public JsonResult ObterGraficoResumoContas()
        {
            try
            {
                //consultar o total de receitas e despesas
                var resumoContas = unitOfWork.ContaRepository.GetResumoConta();

                //transferir estes dados para a model do highcharts
                var model = new List<HighChartsViewModel>();
                foreach (var item in resumoContas)
                {
                    model.Add(new HighChartsViewModel
                    {
                        Name = item.NomeCategoria,
                        Data = new List<int>() { Convert.ToInt32(item.Total) }
                    });
                }

                return Json(model);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message);
            }
        }

        //método para executar uma chamada Javascript
        public JsonResult ObterGraficoResumoContasPorData(DateTime DataMin, DateTime DataMax)
        {
            try
            {
                //consultar o total de receitas e despesas
                var resumoContas = unitOfWork.ContaRepository
                    .GetResumoConta(DataMin, DataMax);

                //transferir estes dados para a model do highcharts
                var model = new List<HighChartsViewModel>();
                foreach (var item in resumoContas)
                {
                    model.Add(new HighChartsViewModel
                    {
                        Name = item.NomeCategoria,
                        Data = new List<int>() { Convert.ToInt32(item.Total) }
                    });
                }

                return Json(model);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Entities;
using Projeto.Presentation.Mvc.Models;

namespace Projeto.Presentation.Mvc.Controllers
{
    public class ContaController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ContaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Cadastro()
        {
            return View(GetContaCadastroModel());
        }

        [HttpPost]
        public IActionResult Cadastro(ContaCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta
                    {
                        IdConta = Guid.NewGuid(),
                        NomeConta = model.Nome,
                        ValorConta = decimal.Parse(model.Valor),
                        DataConta = DateTime.Parse(model.Data),
                        IdCategoria = Guid.Parse(model.IdCategoria)
                    };
                    unitOfWork.ContaRepository.Create(conta);
                    unitOfWork.SaveChanges();


                    TempData["Mensagem"] = $"Conta {conta.NomeConta}, cadastrado com sucesso.";
                    ModelState.Clear(); //limpar os campos do formulário
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }
            return View(GetContaCadastroModel());
        }

        public IActionResult Consulta()
        {
            return View();
        }

        //método para retornar uma instancia da classe model
        //de cadastro de conta com os itens de categoria
        private ContaCadastroModel GetContaCadastroModel()
        {
            var model = new ContaCadastroModel();
            model.ItensCategoria = new List<SelectListItem>();

            //percorrer as categorias obtidas no banco de dados
            foreach (var categoria in unitOfWork.CategoriaRepository.GetAll())
            {
                var item = new SelectListItem
                {
                    Value = categoria.IdCategoria.ToString(),
                    Text = categoria.Nome.ToUpper()
                };

                model.ItensCategoria.Add(item);
            }

            return model;
        }
    }
}
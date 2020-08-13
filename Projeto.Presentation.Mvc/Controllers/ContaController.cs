using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Entities;
using Projeto.Presentation.Mvc.Models;
using Projeto.Presentation.Mvc.Reports;
using Projeto.Presentation.Mvc.Reports.Contas;

namespace Projeto.Presentation.Mvc.Controllers
{
    public class ContaController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public object ContasHtmlReport { get; private set; }

        public ContaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Cadastro()
        {
            return View(GetContaCadastroModel());
        }

        public IActionResult Consulta()
        {
            var result = new List<Conta>();

            try
            {
                result = unitOfWork.ContaRepository.GetAll()
                    .OrderByDescending(c => c.DataConta)
                    .ToList();
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View(result);
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

        public IActionResult Exclusao(string id)
        {
            try
            {
                //bucar o registro pelo id
                var conta = unitOfWork.ContaRepository.GetById(Guid.Parse(id));
                //excluindo o registro
                unitOfWork.ContaRepository.Delete(conta);
                unitOfWork.SaveChanges();

                TempData["Mensagem"] = "Conta excluída com sucesso";
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(string id)
        {
            //criando um objeto da classe model
            var model = GetContaEdicaoModel();

            try
            {
                //buscar no banco de dados a conta pelo id
                var conta = unitOfWork.ContaRepository.GetById(Guid.Parse(id));

                //preenchendo a model com os dados da conta
                model.IdConta = conta.IdConta.ToString();
                model.Nome = conta.NomeConta;
                model.Valor = conta.ValorConta.ToString();
                model.Data = conta.DataConta.ToString();
                model.IdCategoria = conta.IdCategoria.ToString();
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            //enviando a model para a página
            return View(model);
        }

        [HttpPost] //recebe o SUBMIT do formulário
        public IActionResult Edicao(ContaEdicaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta
                    {
                        IdConta = Guid.Parse(model.IdConta),
                        NomeConta = model.Nome,
                        ValorConta = decimal.Parse(model.Valor),
                        DataConta = DateTime.Parse(model.Data),
                        IdCategoria = Guid.Parse(model.IdCategoria)
                    };

                    unitOfWork.ContaRepository.Update(conta);
                    unitOfWork.SaveChanges();

                    TempData["Mensagem"] = $"Conta {conta.NomeConta}, atualizada com sucesso.";
                    return RedirectToAction("Consulta");
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View(GetContaEdicaoModel());
        }

        public void ExportarPdf()
        {
            try
            {
                //consultar as contas no banco de dados..
                var contas = unitOfWork.ContaRepository.GetAll()
                                .OrderByDescending(c => c.DataConta)
                                .ToList();

                var totalReceitas = contas
                    .Where(c => c.Categoria.Nome.ToUpper()
                    .Contains("RECEITA"))
                    .Sum(c => c.ValorConta);

                var html = ContaHtmlReport.GetReport(contas);

                var pdf = PdfReport.Convert(html);

                //DOWNLOAD DO PDF..
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.Headers.Add("content-disposition", "attachment; filename=contas.pdf");
                Response.Body.WriteAsync(pdf, 0, pdf.Length);
                Response.Body.Flush();
                Response.StatusCode = StatusCodes.Status200OK;

            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }
        }

        //método para retornar uma instancia da classe model
        //de cadastro de conta com os itens de categoria
        private ContaCadastroModel GetContaCadastroModel()
        {
            var model = new ContaCadastroModel();
            model.ItensCategoria = GetItensCategoria();

            return model;
        }

        private ContaEdicaoModel GetContaEdicaoModel()
        {
            var model = new ContaEdicaoModel();
            model.ItensCategoria = GetItensCategoria();

            return model;
        }

        //método para retornar a lista de itens de opção do campo DropDownList
        private List<SelectListItem> GetItensCategoria()
        {
            var itensCategoria = new List<SelectListItem>();

            //percorrer as categorias obtidas no banco de dados
            foreach (var categoria in unitOfWork.CategoriaRepository.GetAll())
            {
                var item = new SelectListItem
                {
                    Value = categoria.IdCategoria.ToString(),
                    Text = categoria.Nome.ToUpper()
                };

                itensCategoria.Add(item);
            }

            return itensCategoria;
        }

    }
}
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Reports.Contas
{
    public class ContaHtmlReport
    {
        public static string GetReport(List<Conta> contas)
        {
            var totalReceitas = contas
                               .Where(c => c.Categoria.Nome.ToUpper().Contains("RECEITA"))
                               .Sum(c => c.ValorConta);

            var totalDespesas = contas
                               .Where(c => c.Categoria.Nome.ToUpper().Contains("DESPESA"))
                               .Sum(c => c.ValorConta);

            var saldoFinal = totalReceitas - totalDespesas;

            //desenhando o relatório em HTML
            var builder = new StringBuilder();

            builder.Append("<html>");
            builder.Append("<body>");

            builder.Append("<h2>Relatório de Contas</h2>");
            builder.Append($"<p>Relatório gerado em: {DateTime.Now.ToString("dddd, dd/MM/yyyy")}</p>");

            builder.Append("<h5>Listagem de contas:</h5>");
            builder.Append("<br/>");

            builder.Append($"<p>Total de Receitas: {totalReceitas.ToString("c")}</p>");
            builder.Append($"<p>Total de Despesas: {totalDespesas.ToString("c")}</p>");
            builder.Append($"<p>Saldo: {saldoFinal.ToString("c")}</p>");

            if (saldoFinal > 0)
            {
                builder.Append($"<p>Saldo Positivo</p>");
            }
            else if (saldoFinal == 0)
            {
                builder.Append($"<p>Saldo Nulo</p>");
            }
            else
            {
                builder.Append($"<p>Saldo Devedor</p>");
            }

            builder.Append("<br/>");

            builder.Append("<table border='1' width='100%'>");
            builder.Append("<thead>");
            builder.Append("<tr>");
            builder.Append("<th>Data da Conta</th>");
            builder.Append("<th>Valor da Conta</th>");
            builder.Append("<th>Categoria</th>");
            builder.Append("<th>Nome da Conta</th>");
            builder.Append("</tr>");
            builder.Append("</thead>");
            builder.Append("<tbody>");

            foreach (var item in contas)
            {
                builder.Append("<tr>");
                builder.Append($"<td>{item.DataConta.ToString("dd/MM/yyyy")}</td>");
                builder.Append($"<td>{item.ValorConta.ToString("c")}</td>");
                builder.Append($"<td>{item.Categoria.Nome.ToUpper()}</td>");
                builder.Append($"<td>{item.NomeConta}</td>");
                builder.Append("</tr>");
            }

            builder.Append("</tbody>");
            builder.Append("<tfoot>");
            builder.Append("<tr>");
            builder.Append($"<td colspan='4'>Quantidade de contas: {contas.Count()}</td>");
            builder.Append("</tr>");
            builder.Append("</tfoot>");
            builder.Append("</table>");

            builder.Append("</body>");
            builder.Append("</html>");

            return builder.ToString();
        }
    }
}

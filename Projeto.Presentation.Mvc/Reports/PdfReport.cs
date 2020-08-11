using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Reports
{
    public class PdfReport
    {
        public static byte[] Convert(string html)
        {
            byte[] pdf = null;

            MemoryStream ms = new MemoryStream();
            TextReader reader = new StringReader(html);

            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 40, 40, 20, 20);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            doc.Open();
            htmlWorker.StartDocument();
            htmlWorker.Parse(reader);

            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

            pdf = ms.ToArray();
            return pdf;
        }
    }
}

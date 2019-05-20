using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication1.Models;
using WebApplication1.Models.Classes;

namespace WebApplication1.Controllers
{

  
    public class OfxController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

   

        // GET: Ofx
        public ActionResult Index()
        {

            var filename = "Ofx.xml";
            var diretorio = Server.MapPath("~").Substring(0, Server.MapPath("~").IndexOf("WebApplication1"));
            var pach = Path.Combine(diretorio, filename);

            XDocument xmlDoc = new XDocument();
            xmlDoc = XDocument.Load(pach);

        

        var imps = (from c in xmlDoc.Descendants("STMTTRN")
                      
                        where c.Element("TRNTYPE").Value == "DEBIT"

                        select new OfxDados
                        {
                            Codigo = decimal.Parse(c.Element("TRNAMT").Value.Replace("-", ""),
                                                   NumberFormatInfo.InvariantInfo),
                            DataTransacao = DateTime.ParseExact(c.Element("DTPOSTED").Value,
                                                       "yyyyMMdd", null),
                            Descricao = c.Element("MEMO").Value
                          
                        });


            return View();
        }
    }

   /* TRNTYPE- Tipo de transação: DEBITouCREDIT
   DTPOSTED - Data da transação, formatada com YYYYMMDDHHMMSS
   TRNAMT- Valor (negativo quando é um DEBIT)
   FITID- ID da transação CHECKNUM- Número do cheque ou ID da transação
   MEMO- Pequena descrição da transação; muito útil quando você usa seu cartão de débito */

}
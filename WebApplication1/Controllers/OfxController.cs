
using System.Collections.Generic;

using System.Web.Mvc;
using System.Xml;

using WebApplication1.Models.Classes;

namespace WebApplication1.Controllers
{


    public class OfxController : Controller
    {
           public ActionResult Index()
        {
            List<OfxModel> ofx = new List<OfxModel>();

       
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XML/OFX.xml"));

         
            foreach (XmlNode node in doc.SelectNodes("/OFX/BANKTRANLIST/STMTTRN"))
            {
           
                ofx.Add(new OfxModel
                {
                    ID = node["CHECKNUM"].InnerText,
                    TipoTransacao = node["TRNTYPE"].InnerText,
                    Valor = node["TRNAMT"].InnerText,
                    DataTransacao = node["DTPOSTED"].InnerText,
                    Descricao = node["MEMO"].InnerText
                });
            }

            return View(ofx);
        }

    }
}

   
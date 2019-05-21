using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Classes
{
    public class OfxModel
    {
        public string ID { get; set; }
        public string TipoTransacao { get; set; }
        public string Valor { get; set; }
        public string Descricao { get; set; }
        public string DataTransacao { get; set; }
    }
}
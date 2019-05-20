using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Classes
{
    public class OfxDados
    {
        public decimal Codigo { get; set; }
        public String Descricao { get; set; }
        public String TipoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }

    }
}
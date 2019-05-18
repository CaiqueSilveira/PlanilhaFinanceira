using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Classes
{
    public class Extrato : IComparable<Extrato>
    {
        public int Tipo { get; set; }
        [DisplayFormat(DataFormatString = "{0:c2}", ApplyFormatInEditMode = true)]
        public float Valor { get; set; }
        public DateTime DataRealizacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public String Definicao { get; set; }
        public String Pagamento { get; set; }
        public float SaldoParcial { get; set; }
        public float SaldoPar { get; set; }

        public int CompareTo(Extrato other)
        {
            return this.DataRealizacao.CompareTo(other.DataRealizacao);
        }
    }
}
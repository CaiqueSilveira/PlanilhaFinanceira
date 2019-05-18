using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Collections;
using WebApplication1.Models.Classes;
using Rotativa;

namespace WebApplication1.Controllers
{
    public class ExtratoController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();
       
        public ActionResult Index(String pesq_inicio, String pesq_fim, String credito, String debito)
        {
            float totalDespesa = 0, totalReceitas = 0;
            Extrato item;
            var lista = new List<Extrato>();
            var despesas = db.Despesas.ToList();

            foreach (var desp in despesas)
            {

                item = new Extrato();
                item.Valor = desp.Valor;
                item.SaldoPar = desp.SaldoPar - desp.Valor;
                item.Tipo = 1;
                item.DataRealizacao = desp.DataRealizacao;
                item.DataVencimento = desp.DataRealizacao;
                item.Definicao = desp.CaractDespesa.ToString() + "/" + desp.NomeDespesa;
                item.Pagamento = "Único";
                lista.Add(item);
                totalDespesa += item.Valor;

            }

            var receitas = db.Receitas.ToList();

            foreach (var rec in receitas)
            {

                item = new Extrato();
                item.Valor = rec.Valor;
                item.SaldoParcial = rec.SaldoParcial + rec.Valor ;
                item.Tipo = 2;
                item.DataRealizacao = rec.DataRecebimento;
                item.DataVencimento = rec.DataRecebimento;
                item.Definicao = rec.TipoReceita.ToString() + "/" + rec.Descricao;
                item.Pagamento = "1/6";
                lista.Add(item);
                totalReceitas += item.Valor;

            }
            lista.Sort();


            if (!String.IsNullOrEmpty(pesq_inicio) && !String.IsNullOrEmpty(pesq_fim))
            {
                DateTime date1 = DateTime.Parse(pesq_inicio);
                DateTime date2 = DateTime.Parse(pesq_fim);
                ViewBag.Lista = lista.Where(x => x.DataRealizacao.CompareTo(date1) >= 0 && x.DataVencimento.CompareTo(date2) <= 0);

            }
            else if (!String.IsNullOrEmpty(credito))
            {

                ViewBag.Lista = lista.Where(c => c.Tipo.ToString().ToLower().Contains(credito));
            }

            else if (!String.IsNullOrEmpty(debito))
            {

                ViewBag.Lista = lista.Where(d => d.Tipo.ToString().ToLower().Contains(debito));
            }
            else
            {
                ViewBag.Lista = lista;
            }
            //Git
            ViewBag.TotalDespesas = totalDespesa;
            ViewBag.TotalReceitas = totalReceitas;
            return View();
        }

    }

}
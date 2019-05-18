using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Transactions;
using WebApplication1.Models.Classes;

namespace WebApplication1.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<WebApplication1Context>
    {
        protected override void Seed(WebApplication1Context context)
        {
            TransactionOptions transactionOption = new TransactionOptions();
            transactionOption.IsolationLevel = IsolationLevel.Serializable;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOption))
            {
                try
                {
                    #region TipoDespesas
                    IList<TipoDespesa> tipoDespesas = new List<TipoDespesa>();

                    tipoDespesas.Add(new TipoDespesa() { Nome = "Alimentação", Status = EnumSts.ATIVO });
                    tipoDespesas.Add(new TipoDespesa() { Nome = "Educação", Status = EnumSts.ATIVO });
                    tipoDespesas.Add(new TipoDespesa() { Nome = "Saúde", Status = EnumSts.ATIVO });

                    context.TipoDespesas.AddRange(tipoDespesas);
                    context.SaveChanges();
                    #endregion

                    #region Clientes
                    IList<Cliente> clientes = new List<Cliente>();

                    clientes.Add(new Cliente() {
                        Nome = "Alex",
                        Email = "alex@gmail.com",
                        DataAniversario = new DateTime(1995, 02, 20),
                        ConfirmacaoEmail = "alex@gmail.com" });

                    clientes.Add(new Cliente() {
                        Nome = "Pedro",
                        Email = "pedro@gmail.com",
                        DataAniversario = new DateTime(1994, 05, 17),
                        ConfirmacaoEmail = "pedro@gmail.com" });

                    clientes.Add(new Cliente() {
                        Nome = "Tiago",
                        Email = "tiago@gmail.com",
                        DataAniversario = new DateTime(1999, 02, 16),
                        ConfirmacaoEmail = "tiago@gmail.com" });

                    context.Clientes.AddRange(clientes);
                    context.SaveChanges();
                    #endregion

                    #region Despesas
                    IList<Despesa> despesas = new List<Despesa>();

                    despesas.Add(new Despesa() {
                        NomeDespesa = "Restaurante",
                        CaractDespesa = EnumCd.FIXA,
                        IdTipoDespesa = tipoDespesas[0].Id,
                        TipoDespesa = tipoDespesas[0],
                        SaldoPar = 400,
                        Valor = 45, DataRealizacao = new DateTime(2019, 01, 01) });

                    despesas.Add(new Despesa() {
                        NomeDespesa = "Médico",
                        CaractDespesa = EnumCd.FIXA,
                        IdTipoDespesa = tipoDespesas[2].Id,
                        TipoDespesa = tipoDespesas[2],
                        Valor = 200,
                        SaldoPar = 3000,
                        DataRealizacao = new DateTime(2019, 02, 01) });

                    despesas.Add(new Despesa() {
                        NomeDespesa = "Curso",
                        CaractDespesa = EnumCd.FIXA,
                        IdTipoDespesa = tipoDespesas[1].Id,
                        TipoDespesa = tipoDespesas[1],
                        Valor = 900,
                        SaldoPar = 2000,
                        DataRealizacao = new DateTime(2019, 01, 30) });

                    context.Despesas.AddRange(despesas);
                    context.SaveChanges();
                    #endregion

                    #region
                    IList<Receita> receitas = new List<Receita>();

                    receitas.Add(new Receita()
                    {
                        TipoReceita = TipoReceita.Transferencia,
                        DataRecebimento = new DateTime(2019, 01, 06),
                        FormaRecebimento = FormaRecebimento.Dinheiro,
                        NumeroParcelas = 6,
                        Parcelamento = Parcelamento.Parcelado,
                        Valor = 300,
                        SaldoParcial = 5000,
                        Descricao = "Serviço Avulso"
                    });
                    receitas.Add(new Receita()
                    {
                        TipoReceita = TipoReceita.Indenizacao,
                        DataRecebimento = new DateTime(2019, 01, 30),
                        FormaRecebimento = FormaRecebimento.Cartao,
                        NumeroParcelas = 6,
                        Parcelamento = Parcelamento.Parcelado,
                        Valor = 900,
                        SaldoParcial = 2000,
                        Descricao = "Recebimento Salário"
                    });

                    context.Receitas.AddRange(receitas);
                    context.SaveChanges();
                    #endregion

                    scope.Complete();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            base.Seed(context);
        }
    }
}
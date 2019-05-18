using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Classes;
using Chart = DotNet.Highcharts.Options.Chart;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        public ActionResult Index()
        {
     

            Highcharts columnChart = new Highcharts("columnchart");
            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 2
            });
            columnChart.SetTitle(new Title()
            {
                Text = "Gráfico Total de Receitas e Despesas"
            });
           
            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                
                Categories = new[] { "-" }
            });
            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Valores",
                    Style = "fontWeight: 'bold', fontSize: '17px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });
            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });
            columnChart.SetSeries(new Series[]
            {
                //( db.Despesas.Sum( c=> c.Valor));

            new Series
                {
                 Name = "Receitas",
                    Data = new Data(new object[]
                    {
                        double.Parse(db.Receitas.Sum( c=> c.Valor).ToString())})
                   


                },
             new Series()
                {
            Name = "Despesas", 
                        Data = new Data(new object[]
                    {

                        double.Parse(db.Despesas.Sum( c=> c.Valor).ToString())})
                }
            }


            );
           
            return View(columnChart);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
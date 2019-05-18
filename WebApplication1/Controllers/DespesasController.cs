using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Classes;

namespace WebApplication1.Controllers
{
    public class DespesasController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: Despesas
        public ActionResult Index()
        {
            var despesas = db.Despesas.Include(d => d.TipoDespesa);
            return View(despesas.ToList());
        }

        // GET: Despesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = db.Despesas.Find(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        // GET: Despesas/Create
        public ActionResult Create()
        {
            ViewBag.IdTipoDespesa = new SelectList(db.TipoDespesas, "Id", "Nome");
            return View();
        }

        // POST: Despesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomeDespesa,Valor,SaldoPar,IdTipoDespesa,CaractDespesa,DataRealizacao")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                db.Despesas.Add(despesa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTipoDespesa = new SelectList(db.TipoDespesas, "Id", "Nome", despesa.IdTipoDespesa);
            return View(despesa);
        }

        // GET: Despesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = db.Despesas.Find(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipoDespesa = new SelectList(db.TipoDespesas, "Id", "Nome", despesa.IdTipoDespesa);
            return View(despesa);
        }

        // POST: Despesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeDespesa,Valor,SaldoPar,IdTipoDespesa,CaractDespesa,DataRealizacao")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(despesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipoDespesa = new SelectList(db.TipoDespesas, "Id", "Nome", despesa.IdTipoDespesa);
            return View(despesa);
        }

        // GET: Despesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Despesa despesa = db.Despesas.Find(id);
            if (despesa == null)
            {
                return HttpNotFound();
            }
            return View(despesa);
        }

        // POST: Despesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Despesa despesa = db.Despesas.Find(id);
            db.Despesas.Remove(despesa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

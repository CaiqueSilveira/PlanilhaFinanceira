﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Classes;
using System.Collections;

namespace WebApplication1.Controllers
{
    public class ReceitasController : Controller
    {
        private WebApplication1Context db = new WebApplication1Context();

        // GET: Receitas
        public ActionResult Index(String buscar, String buscarIni, String buscarFim)
        {
            DateTime date1, date2;

            if (String.IsNullOrEmpty(buscarIni) && String.IsNullOrEmpty(buscar) && String.IsNullOrEmpty(buscarFim))
            {
                return View(db.Receitas.ToList());
            }
            if (String.IsNullOrEmpty(buscarIni))
            {
                date1 = new DateTime(0001, 01, 01);
            }
            else
            {
                date1 = DateTime.Parse(buscarIni);
            }
            if (String.IsNullOrEmpty(buscarFim))
            {
                date2 = DateTime.Today;
            }
            else
            {
                date2 = DateTime.Parse(buscarFim);
            }

            var result = db.Receitas.Where(x => x.DataRecebimento.CompareTo(date1) >= 0 && x.DataRecebimento.CompareTo(date2) < 0 && x.TipoReceita.ToString().ToLower().Contains(buscar));

            return View(result.ToList());
        }

        // GET: Receitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receitas.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // GET: Receitas/Create
        public ActionResult Create()
        {
            ArrayList lista = new ArrayList();
            for (int i = 1; i < 7; i++)
            {
                lista.Add(i);
            }
            ViewBag.NumParcelas = new SelectList(lista);
            return View();
        }

        // POST: Receitas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TipoReceita,FormaRecebimento,DataRecebimento,Valor,SaldoParcial,Parcelamento,NumeroParcelas,Descricao,Observacao")] Receita receita, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                receita.NumeroParcelas = int.Parse(form["NumParcelas"]);
                db.Receitas.Add(receita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ArrayList lista = new ArrayList();
            for (int i = 1; i < 7; i++)
            {
                lista.Add(i);
            }
            ViewBag.NumParcelas = new SelectList(lista, receita.NumeroParcelas);
            return View(receita);
        }

        // GET: Receitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receitas.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            ArrayList lista = new ArrayList();
            for (int i = 1; i < 7; i++)
            {
                lista.Add(i);
            }
            ViewBag.NumParcelas = new SelectList(lista, receita.NumeroParcelas);
            return View(receita);
        }

        // POST: Receitas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TipoReceita,FormaRecebimento,DataRecebimento,Valor,SaldoParcial,Parcelamento,NumeroParcelas,Descricao,Observacao")] Receita receita, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                receita.NumeroParcelas = int.Parse(form["NumParcelas"]);
                db.Entry(receita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ArrayList lista = new ArrayList();
            for (int i = 1; i < 7; i++)
            {
                lista.Add(i);
            }
            ViewBag.NumParcelas = new SelectList(lista, receita.NumeroParcelas);
            return View(receita);
        }

        // GET: Receitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receita receita = db.Receitas.Find(id);
            if (receita == null)
            {
                return HttpNotFound();
            }
            return View(receita);
        }

        // POST: Receitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receita receita = db.Receitas.Find(id);
            db.Receitas.Remove(receita);
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

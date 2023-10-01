using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DevIO.ApplicationMVC.Models;
using DevIO.ApplicationMVC.ViewModels;
using DevIO.Business.Core.Notifications;
using DevIO.Business.Models.Produtos;
using DevIO.Business.Models.Produtos.Services;
using DevIO.Infra.Data.Repository;

namespace DevIO.ApplicationMVC.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository; // Para fazer leitura do banco de dados.
        private readonly IProdutoService _produtoService;       // Para fazer a persistência no banco de dados.

        public ProdutosController()
        {
            _produtoRepository = new ProdutoRepository();
            _produtoService = new ProdutoService(_produtoRepository, new Notificador());
        }

        public async Task<ActionResult> Index()
        {
            return View(await _produtoRepository.ObterTodos());
        }

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var produto = await _produtoRepository.Obter(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                _produtoService.adicionar(produtoViewModel);

                return RedirectToAction("Index");
            }

            return View(produtoViewModel);
        }

        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoViewModel produtoViewModel = await db.ProdutoViewModels.FindAsync(id);
            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FornecedorId,Nome,Descricao,Imagem,Valor,DataCadastro,Ativo")] ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produtoViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(produtoViewModel);
        }

        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoViewModel produtoViewModel = await db.ProdutoViewModels.FindAsync(id);
            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            ProdutoViewModel produtoViewModel = await db.ProdutoViewModels.FindAsync(id);
            db.ProdutoViewModels.Remove(produtoViewModel);
            await db.SaveChangesAsync();
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

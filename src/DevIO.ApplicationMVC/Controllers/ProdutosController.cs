using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevIO.ApplicationMVC.ViewModels;
using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Produtos;
using DevIO.Business.Models.Produtos.Services;

namespace DevIO.ApplicationMVC.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;         // Para fazer leitura do banco de dados.
        private readonly IProdutoService _produtoService;               // Para fazer a persistência (salvar, editar ou excluir) no banco de dados.
        private readonly IFornecedorRepository _fornecedorRepository;   // Para fazer o acesso aos dados do fornecedor.
        private readonly IMapper _mapper;                               // Para fazer o mapeamento.

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IProdutoService produtoService,
                                  IFornecedorRepository fornecedorRepository,
                                  IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [Route("lista-de-produtos")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View ( _mapper.Map<IEnumerable<ProdutoViewModel>> ( await _produtoRepository.ObterProdutosFornecedores ( ) ) );
        }

        #region Details

        [Route ("dados-de-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }

            return View(produtoViewModel);
        }

        #endregion

        #region Create

        [Route ("novo-produto")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var produtoViewModel = await PopularFornecedores ( new ProdutoViewModel ( ) );

            return View(produtoViewModel);
        }

        [Route ( "novo-produto" )]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedores ( new ProdutoViewModel ( ) );

            if (ModelState.IsValid)
            {
                await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

                return RedirectToAction("Index");
            }

            return View(produtoViewModel);
        }

        #endregion

        #region Edit

        [Route ("editar-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }

            return View(produtoViewModel);
        }

        [Route ( "editar-produto/{id:guid}" )]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));

                return RedirectToAction("Index");
            }

            return View(produtoViewModel);
        }

        #endregion

        #region Delete

        [Route ( "excluir-produto/{id:guid}" )]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }

            return View(produtoViewModel);
        }

        [Route ( "excluir-produto/{id:guid}" )]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound ( );
            }

            await _produtoService.Remover(id);

            return RedirectToAction ("Index");
        }

        #endregion

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedores(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>> ( await _fornecedorRepository.ObterTodos ( ) );
            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produto )
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>> ( await _fornecedorRepository.ObterTodos ( ) );
            return produto;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _produtoRepository.Dispose();
                _produtoService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevIO.ApplicationMVC.ViewModels;
using DevIO.Business.Core.Notificacoes;
using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Fornecedores.Services;

namespace DevIO.ApplicationMVC.Controllers
{
    [Authorize]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                      IFornecedorService fornecedorService,
                                      IMapper mapper,
                                      INotificador notificador ) : base ( notificador )
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        #region Index

        [AllowAnonymous]
        [Route ("lista-de-fornecedores")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View( _mapper.Map<IEnumerable<FornecedorViewModel>> (await _fornecedorRepository.ObterTodos()));
        }

        #endregion

        #region Datails

        [Route ("dados-do-fornecedor/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return HttpNotFound();
            }

            return View(fornecedorViewModel);
        }

        #endregion

        #region Create

        [Route ("novo-fornecedor")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Route("novo-fornecedor")]
        [HttpPost]
        public async Task<ActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            await _fornecedorService.Adicionar(fornecedor);

            if (!OperaçãoValida ( )) return View ( fornecedorViewModel );

            return RedirectToAction ("Index");
        }

        #endregion

        #region Edit

        [Route ("editar-fornecedor/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return HttpNotFound();
            }

            return View(fornecedorViewModel);
        }

        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return HttpNotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);

            if (!OperaçãoValida ( )) return View ( fornecedorViewModel );

            return RedirectToAction ("Index");
        }

        #endregion

        #region Delete

        [Authorize(Roles = "Admin")]
        [Route ("excluir-fornecedor/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return HttpNotFound();
            }

            return View(fornecedorViewModel);
        }

        [Authorize ( Roles = "Admin" )]
        [Route ( "excluir-fornecedor/{id:guid}" )]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return HttpNotFound();

            await _fornecedorService.Remover(id);

            // TODO:
            // E se não der certo?

            return RedirectToAction ("Index");
        }

        #endregion

        #region Atualizar

        [Route ("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> AtualizarEndereco(Guid id )
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if(fornecedor == null)
            {
                return HttpNotFound ();
            }

            return PartialView ( "_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco } );
        }

        [Route ( "atualizar-endereco-fornecedor/{id:guid}" )]
        [HttpPost]
        public async Task<ActionResult> AtualizarEndereco ( FornecedorViewModel fornecedorViewModel )
        {
            ModelState.Remove ( "Nome" );
            ModelState.Remove ( "Documento" );

            if (!ModelState.IsValid) return PartialView ( "_AtualizarEndereco", fornecedorViewModel );

            await _fornecedorService.AtualizarEndereco ( _mapper.Map<Endereco> ( fornecedorViewModel.Endereco ) );

            //TODO:
            // E se não der certo?

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });
            return Json ( new { success = true, url } );
        }

        #endregion

        #region ObterDadosFornecedorEndereco

        [Route ( "obter-endereco-fornecedor/{id:guid}" )]
        public async Task<ActionResult> ObterEndereco ( Guid id )
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return HttpNotFound ( );
            }

            return PartialView ( "_DetalhesEndereco", fornecedor );
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutoEndereco(id));
        }

        #endregion

        #region Dispose

        protected override void Dispose ( bool disposing )
        {
            if (disposing)
            {
                _fornecedorRepository.Dispose ( );
                _fornecedorService.Dispose ( );
            }
            base.Dispose ( disposing );
        }

        #endregion
    }
}
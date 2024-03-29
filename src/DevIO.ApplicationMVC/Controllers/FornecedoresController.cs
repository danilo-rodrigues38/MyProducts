﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevIO.ApplicationMVC.Extensions;
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

        public FornecedoresController ( IFornecedorRepository fornecedorRepository,
                                      IMapper mapper,
                                      IFornecedorService fornecedorService,
                                      INotificador notificador ) : base ( notificador )
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _fornecedorService = fornecedorService;
        }

        [AllowAnonymous]
        [Route ( "lista-de-fornecedores" )]
        public async Task<ActionResult> Index ( )
        {
            return View ( _mapper.Map<IEnumerable<FornecedorViewModel>> ( await _fornecedorRepository.ObterTodos ( ) ) );
        }

        [AllowAnonymous]
        [Route ( "dados-do-fornecedor/{id:guid}" )]
        public async Task<ActionResult> Details ( Guid id )
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return HttpNotFound ( );
            }

            return View ( fornecedorViewModel );
        }

        [ClaimsAuthorize ( "Fornecedor", "Adicionar" )]
        [Route ( "novo-fornecedor" )]
        public ActionResult Create ( )
        {
            return View ( );
        }

        [ClaimsAuthorize ( "Fornecedor", "Adicionar" )]
        [Route ( "novo-fornecedor" )]
        [HttpPost]
        public async Task<ActionResult> Create ( FornecedorViewModel fornecedorViewModel )
        {
            if (!ModelState.IsValid) return View ( fornecedorViewModel );

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar ( fornecedor );

            if (!OperacaoValida ( )) return View ( fornecedorViewModel );

            return RedirectToAction ( "Index" );
        }

        [ClaimsAuthorize ( "Fornecedor", "Editar" )]
        [Route ( "editar-fornecedor/{id:guid}" )]
        public async Task<ActionResult> Edit ( Guid id )
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return HttpNotFound ( );
            }

            return View ( fornecedorViewModel );
        }

        [ClaimsAuthorize ( "Fornecedor", "Editar" )]
        [Route ( "editar-fornecedor/{id:guid}" )]
        [HttpPost]
        public async Task<ActionResult> Edit ( Guid id, FornecedorViewModel fornecedorViewModel )
        {
            if (id != fornecedorViewModel.Id) return HttpNotFound ( );

            if (!ModelState.IsValid) return View ( fornecedorViewModel );

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar ( fornecedor );

            if (!OperacaoValida ( )) return View ( await ObterFornecedorProdutosEndereco ( id ) );

            return RedirectToAction ( "Index" );
        }

        [ClaimsAuthorize ( "Fornecedor", "Excluir" )]
        [Route ( "excluir-fornecedor/{id:guid}" )]
        public async Task<ActionResult> Delete ( Guid id )
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return HttpNotFound ( );
            }

            return View ( fornecedorViewModel );
        }

        [ClaimsAuthorize ( "Fornecedor", "Excluir" )]
        [Route ( "excluir-fornecedor/{id:guid}" )]
        [HttpPost, ActionName ( "Delete" )]
        public async Task<ActionResult> DeleteConfirmed ( Guid id )
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return HttpNotFound ( );

            await _fornecedorService.Remover ( id );

            if (!OperacaoValida ( )) return View ( fornecedor );

            return RedirectToAction ( "Index" );
        }

        [AllowAnonymous]
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

        [ClaimsAuthorize ( "Fornecedor", "Editar" )]
        [Route ( "atualizar-endereco-fornecedor/{id:guid}" )]
        [HttpGet]
        public async Task<ActionResult> AtualizarEndereco ( Guid id )
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return HttpNotFound ( );
            }

            return PartialView ( "_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco } );
        }

        [ClaimsAuthorize ( "Fornecedor", "Editar" )]
        [Route ( "atualizar-endereco-fornecedor/{id:guid}" )]
        [HttpPost]
        public async Task<ActionResult> AtualizarEndereco ( FornecedorViewModel fornecedorViewModel )
        {
            ModelState.Remove ( "Nome" );
            ModelState.Remove ( "Documento" );

            if (!ModelState.IsValid) return PartialView ( "_AtualizarEndereco", fornecedorViewModel );

            await _fornecedorService.AtualizarEndereco ( _mapper.Map<Endereco> ( fornecedorViewModel.Endereco ) );

            if (!OperacaoValida ( )) return PartialView ( "_AtualizarEndereco", fornecedorViewModel );

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });
            return Json ( new { success = true, url } );
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco ( Guid id )
        {
            return _mapper.Map<FornecedorViewModel> ( await _fornecedorRepository.ObterFornecedorEndereco ( id ) );
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco ( Guid id )
        {
            return _mapper.Map<FornecedorViewModel> ( await _fornecedorRepository.ObterFornecedorProdutoEndereco ( id ) );
        }
    }
}
using System;
using System.Threading.Tasks;
using DevIO.Business.Core.Notifications;
using DevIO.Business.Core.Services;
using DevIO.Business.Models.Fornecedores.Validations;

namespace DevIO.Business.Models.Produtos.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador) : base (notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task adicionar ( Produto produto )
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            if (true)
            {
                Notificar("Minha mensagem de erro.");
            }

            await _produtoRepository.Adicionar ( produto );
        }

        public async Task Atualizar ( Produto produto )
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            if (true)
            {
                Notificar("Minha mensagem de erro.");
            }

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover ( Guid id )
        {
            await _produtoRepository.Remover(id);
        }

        public void Dispose ( )
        {
            _produtoRepository?.Dispose();
        }
    }
}

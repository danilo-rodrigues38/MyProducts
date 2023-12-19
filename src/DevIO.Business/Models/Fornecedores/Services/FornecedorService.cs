using DevIO.Business.Core.Notificacoes;
using DevIO.Business.Core.Services;
using DevIO.Business.Models.Fornecedores.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Fornecedores.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorrepository;
        private readonly IEnderecoRepository _enderecorepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _fornecedorrepository = fornecedorRepository;
            _enderecorepository = enderecoRepository;
        }

        public async Task Adicionar ( Fornecedor fornecedor )
        {
            if (!ExecutarValidacao ( new FornecedorValidation ( ), fornecedor )
                || !ExecutarValidacao ( new EnderecoValidation ( ), fornecedor.Endereco )) return;

            if (await FornecedorExistente ( fornecedor )) return;

            await _fornecedorrepository.Adicionar ( fornecedor );
        }

        public async Task Atualizar ( Fornecedor fornecedor )
        {
            if (!ExecutarValidacao ( new FornecedorValidation ( ), fornecedor )) return;

            if (await FornecedorExistente(fornecedor )) return;

            await _fornecedorrepository.Atualizar ( fornecedor );
        }

        public async Task Remover ( Guid id )
        {
            var fornecedor = await _fornecedorrepository.ObterFornecedorProdutoEndereco(id);

            if (fornecedor.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados!");
                return;
            }

            if(fornecedor.Endereco != null)
            {
                await _enderecorepository.Remover(fornecedor.Endereco.Id);
            }

            await _fornecedorrepository.Remover(id);
        }

        public async Task AtualizarEndereco ( Endereco endereco )
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecorepository.Atualizar(endereco);
        }

        private async Task<bool> FornecedorExistente(Fornecedor fornecedor )
        {
            var fornecedorAtual = await _fornecedorrepository.Buscar(
                f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);

            if (!fornecedorAtual.Any()) return false;

            Notificar("Já existe um fornecedor com este documento informado.");
            return true;
        }

        public void Dispose ( )
        {
            _fornecedorrepository?.Dispose ( );
            _enderecorepository?.Dispose ( );
        }
    }
}

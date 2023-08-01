using DevIO.Business.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Produtos
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutoPorFornecedor ( Guid FornecedorId );
        Task<IEnumerable<Produto>> ObterProdutoFornecedores ( );
        Task<Produto> ObterProdutoFornecedores ( Guid id );
    }
}

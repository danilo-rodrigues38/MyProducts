using System;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Produtos.Services
{
    public interface IProdutoService : IDisposable
    {
        Task adicionar ( Produto produto );
        Task Atualizar ( Produto produto );
        Task Remover ( Guid id );
    }
}

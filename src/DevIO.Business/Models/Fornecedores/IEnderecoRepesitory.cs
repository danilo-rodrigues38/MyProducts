using DevIO.Business.Core.Data;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Models.Fornecedores
{
    public interface IEnderecoRepesitory : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor ( Guid FornecedorId );
    }
}

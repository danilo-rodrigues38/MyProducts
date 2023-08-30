using System;
using System.Data.Entity;
using System.Threading.Tasks;
using DevIO.Business.Models.Fornecedores;

namespace DevIO.Infra.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid FornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                           .FirstOrDefaultAsync(f => f.Id == FornecedorId);
        }
    }
}
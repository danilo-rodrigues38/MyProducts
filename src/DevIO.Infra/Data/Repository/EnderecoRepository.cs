using System;
using System.Data.Entity;
using System.Threading.Tasks;
using DevIO.Business.Models.Fornecedores;
using DevIO.Infra.Data.Context;

namespace DevIO.Infra.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository ( MyDbContext context ) : base ( context ) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid FornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                           .FirstOrDefaultAsync(f => f.Id == FornecedorId);
        }
    }
}
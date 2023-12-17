using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Models.Fornecedores;
using DevIO.Infra.Data.Context;

namespace DevIO.Infra.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository ( MyDbContext context ) : base ( context ) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                           .Include(f => f.Endereco)
                           .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutoEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                           .Include(f => f.Endereco)
                           .Include(f => f.Produtos)
                           .FirstOrDefaultAsync(f => f.Id == id);
        }
        /*
        public override async Task Remover(Guid id)
        {
            var fornecedor = await Obter(id);
            fornecedor.Ativo = false;

            await Atualizar(fornecedor);
        }
        */
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Models.Produtos;
using DevIO.Infra.Data.Context;

namespace DevIO.Infra.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository ( MyDbContext context ) : base ( context ) { }

        public async Task<Produto> ObterProdutoFornecedores(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                           .Include(f => f.Fornecedor)
                           .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await Db.Produtos.AsNoTracking()
                           .Include(f => f.Fornecedor)
                           .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutoPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
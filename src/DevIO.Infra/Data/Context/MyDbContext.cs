using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Produtos;
using DevIO.Infra.Data.Mappings;
using System.Data.Entity;
namespace DevIO.Infra.Data.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DefaultConnection")
        { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating ( DbModelBuilder modelBuilder )
        {
            //modelBuilder.Conventions.Remove (Plurali)

            modelBuilder.Configurations.Add ( new FornecedorMapping ( ) );
            modelBuilder.Configurations.Add ( new EnderecoMapping ( ) );
            modelBuilder.Configurations.Add ( new ProdutoMapping ( ) );

            //base.OnModelCreating ( modelBuilder );
        }
    }
}

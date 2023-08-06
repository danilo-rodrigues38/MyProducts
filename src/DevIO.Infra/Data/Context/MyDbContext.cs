using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Produtos;
using DevIO.Infra.Data.Mappings;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DevIO.Infra.Data.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DefaultConnection")
        {
            // Desabilita o Proxy e o LazyLoading, para não prejudicar a performance do banco de dados.
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating ( DbModelBuilder modelBuilder )
        {
            // Convenções do EntityFramework:

            // PluralizingTableNameConvention - para retirar a pluralização, pois o framework não conhece
            // o idioma português.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention> ( );

            // ManyToManyCascadeDeleteConvention - removendo a deleção em cascata de muitos para muitos.
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention> ( );

            // OneToManyCascadeDeleteConvention - removendo a deleção em cascada de um para muitos.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention> ( );


            // Todas as propriedades que forem do tipo "string" será configurada como
            // "varchar" e com 100 caracteres, salvo se indicado no mapeamento.
            modelBuilder.Properties<string> ( )
                .Configure ( p => p
                    .HasColumnType ( "varchar" )
                    .HasMaxLength ( 100 ) );

            // Força o mapeamento do banco conforme indicado nas classes de mapeamento abaixo.
            modelBuilder.Configurations.Add ( new FornecedorMapping ( ) );
            modelBuilder.Configurations.Add ( new EnderecoMapping ( ) );
            modelBuilder.Configurations.Add ( new ProdutoMapping ( ) );

            base.OnModelCreating ( modelBuilder );
        }
    }
}

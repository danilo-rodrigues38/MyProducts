using DevIO.Business.Models.Produtos;
using System.Data.Entity.ModelConfiguration;

namespace DevIO.Infra.Data.Mappings
{
    public class ProdutoMapping : EntityTypeConfiguration<Produto>
    {
        public ProdutoMapping()
        {
            HasKey ( p => p.Id );

            Property ( p => p.Nome )
                .IsRequired ( )
                .HasMaxLength ( 100 );

            Property ( p => p.Descricao )
                .IsRequired ( )
                .HasMaxLength ( 1000 );

            Property(p=>p.Imagem)
                .IsRequired ( )
                .HasMaxLength ( 100 );

            // O Fornecedor é requerido, o relacionamento é de um para muitos,
            // ou seja, um fornecedor pode ter muitos produtos,
            // e a chave estrangeira vai para FornecedorId.
            HasRequired ( p => p.Fornecedor )
                .WithMany ( f => f.Produtos )
                .HasForeignKey ( p => p.FornecedorId );

            ToTable ( "Produtos" );
        }
    }
}

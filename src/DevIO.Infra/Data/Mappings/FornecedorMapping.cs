using DevIO.Business.Models.Fornecedores;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DevIO.Infra.Data.Mappings
{
    public class FornecedorMapping : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMapping()
        {
            // Indica a chave primária
            HasKey ( f => f.Id );

            // Indica que a propriedade "Nome" é requerida e tem o tamanho máximo de 200.
            Property ( f => f.Nome )
                .IsRequired ( )
                .HasMaxLength ( 200 );

            // Indica que a propriedade "Documento" é requerida, tem o tamanho máximo e é indexado
            // para uma pesquisa mais aprimorada.
            Property ( f => f.Documento )
                .IsRequired ( )
                .HasMaxLength ( 14 )
                .HasColumnAnnotation ( "IX_Documento",
                new IndexAnnotation ( new IndexAttribute { IsUnique = true } ) );

            // Faz a relação do Fornecedor com o Endereço, ou seja, o Fornecedor tem que ter o endereço.
            HasRequired ( f => f.Endereco )
                .WithRequiredPrincipal ( e => e.Fornecedor );

            // Cria a tabela Fornecedores.
            ToTable ( "Fornecedores" );
        }
    }
}

using DevIO.Business.Models.Fornecedores;
using System.Data.Entity.ModelConfiguration;

namespace DevIO.Infra.Data.Mappings
{
    public class EnderecoMapping : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMapping()
        {
            HasKey ( e => e.Id );

            Property ( e => e.Logradouro )
                //.HasColumnName("Rua") // Caso no seu Banco de Dados exixta a coluna Rua.
                .IsRequired ( )
                .HasMaxLength ( 200 );

            Property ( e => e.Numero )
                .IsRequired ( )
                .HasMaxLength ( 5 );

            Property ( e => e.Complemento )
                .IsRequired ( )
                .HasMaxLength ( 50 );

            Property ( e => e.Cep )
                .IsRequired ( )
                .HasMaxLength ( 8 )
                .IsFixedLength ( );

            Property ( e => e.Bairro )
                .IsRequired ( )
                .HasMaxLength ( 100 );

            Property ( e => e.Cidade )
                .IsRequired ( )
                .HasMaxLength ( 100 );

            Property(e=>e.Estado)
                .IsRequired ( )
                .HasMaxLength( 100 );

            ToTable ( "Enderecos" );
        }
    }
}

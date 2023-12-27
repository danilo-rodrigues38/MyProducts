using DevIO.Business.Models.Fornecedores;
using System.Data.Entity.ModelConfiguration;

namespace DevIO.Infra.Data.Mappings
{
    public class EnderecoMapping : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMapping()
        {
            HasKey ( e => e.Id );

            Property ( c => c.Logradouro )
                //.HasColumnName("Rua") // Caso no seu Banco de Dados exista a coluna Rua.
                .IsRequired ( )
                .HasMaxLength ( 200 );

            Property ( c => c.Numero )
                .IsRequired ( )
                .HasMaxLength ( 50 );

            Property ( c => c.Cep )
                .IsRequired ( )
                .HasMaxLength ( 8 )
                .IsFixedLength ( );

            Property ( c => c.Complemento )
                .HasMaxLength ( 250 );

            Property ( c => c.Bairro )
                .IsRequired ( );

            Property ( c => c.Cidade )
                .IsRequired ( );

            Property ( c => c.Estado )
                .IsRequired ( );

            ToTable ( "Enderecos" );
        }
    }
}

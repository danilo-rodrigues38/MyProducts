using DevIO.Business.Core.Validations;
using FluentValidation;

namespace DevIO.Business.Models.Fornecedores.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            // RuleFor - Cria as regras para o campo "Nome".
            // PropertName - exibe na tela o nome do campo, neste caso "Nome".
            // MinLength - valor mínimo permitido, no caso, 2 caracteres.
            // MaxLength - valor máximo permitido, no caso, 200 caracteres.
            RuleFor ( f => f.Nome )
                .NotEmpty ( )
                .WithMessage ( "O campo {PropertName} precisa ser fornecido!" )
                .Length ( 2, 200 )
                .WithMessage ( "O campo {PropertName} precisa ter entre {MinLength} e {MaxLength} caracteres." );

            When ( f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, ( ) =>
            {
                RuleFor ( f => f.Documento.Length ).Equal ( CpfValidacao.TamanhoCpf )
                    .WithMessage ( "O campo Documento precesa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}." );

                RuleFor ( f => CpfValidacao.Validar ( f.Documento ) ).Equal ( true )
                    .WithMessage ( "O documento fornecido é inválido." );
            } );

            When ( f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, ( ) =>
            {
                RuleFor ( f => f.Documento.Length ).Equal ( CnpjValidacao.TamanhoCnpj )
                    .WithMessage ( "O campo Documento precesa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}." );

                RuleFor ( f => CnpjValidacao.Validar ( f.Documento ) ).Equal ( true )
                    .WithMessage ( "O documento fornecido é inválido." );
            } );
        }
    }
}

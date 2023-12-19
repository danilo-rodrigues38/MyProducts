using FluentValidation;

namespace DevIO.Business.Models.Produtos.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor ( c => c.Nome )
                .NotEmpty ( ).WithMessage ( "O campo {PropertyName} precisa ser fornecido." )
                .Length ( 2, 200 ).WithMessage ( "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres." );

            RuleFor ( c => c.Descricao )
                .NotEmpty ( ).WithMessage ( "O campo {PropertyName} precisa ser fornecido." )
                .Length ( 2, 1000 ).WithMessage ( "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres." );

            RuleFor ( c => c.Valor )
                .NotEmpty ( ).WithMessage ( "O campo {PropertyName} precisa ser fornecido." )
                // GreaterThan - onde o valor deve ser maior que 0 "Zero".
                // GreaterThanOrEqualTo - onde o valor de ser maior ou igual a zero, dependendo da sua regra de negócios.
                .GreaterThan (0).WithMessage ( "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres." );
        }
    }
}

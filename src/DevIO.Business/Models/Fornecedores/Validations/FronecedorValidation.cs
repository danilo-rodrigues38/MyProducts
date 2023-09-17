using FluentValidation;

namespace DevIO.Business.Models.Fornecedores.Validations
{
    internal class FronecedorValidation : AbstractValidator<Fornecedor>
    {
        public FronecedorValidation()
        {
            // FuleFor - Cria as regras para o campo "Nome".
            RuleFor ( f => f.Nome )
                .NotEmpty ( )
                // PropertName - exibe na tela o nome do campo, neste caso "Nome".
                .WithMessage ( "O campo {PropertName} precisa ser fornecido!" )
                .Length ( 2, 200 )
                // MinLength - valor mínimo permitido, no caso, 2 caracteres.
                // MaxLength - valor máximo permitido, no caso, 200 caracteres.
                .WithMessage ( "O campo {PropertName} precisa ter entre {MinLength} e {MaxLength} caracteres." );
        }
    }
}

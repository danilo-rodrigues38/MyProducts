using DevIO.Business.Core.Models;
using FluentValidation;

namespace DevIO.Business.Core.Services
{
    public abstract class BaseService
    {
        protected bool ExecutarValidacao<TV, TE> (TV validação, TE entidade ) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validador = validação.Validate(entidade);

            return validador.IsValid;
        }
    }
}

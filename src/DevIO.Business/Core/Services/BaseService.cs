using DevIO.Business.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

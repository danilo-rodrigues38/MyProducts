using System.Web.Mvc;
using DevIO.Business.Core.Notificacoes;

namespace DevIO.ApplicationMVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotificador _notificador;

        public BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperaçãoValida ( )
        {
            if (!_notificador.TemNotificacao ( )) return true;

            var notificacoes = _notificador.ObterNotificacoes();

            notificacoes.ForEach ( c => ViewData.ModelState.AddModelError ( string.Empty, c.Mensagem ) );
            return false;
        }
    }
}
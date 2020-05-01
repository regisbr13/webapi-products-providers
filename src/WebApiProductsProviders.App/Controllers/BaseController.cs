using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Notifications;

namespace WebApiProductsProviders.App.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly INotifier _notifier;

        public BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(x => x.Message)
            });
        }

        protected void NotifyModelErrors(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);

            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string msg)
        {
            _notifier.Handle(new Notification(msg));
        }

        protected ActionResult NotifyException(Exception ex, string customMessage)
        {
            NotifyError($"{ex.Message} {customMessage}");
            return CustomResponse();
        }

        private bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}
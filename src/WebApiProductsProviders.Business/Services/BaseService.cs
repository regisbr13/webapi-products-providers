using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiProductsProviders.Business.Interfaces;
using WebApiProductsProviders.Business.Models;
using WebApiProductsProviders.Business.Notifications;

namespace WebApiProductsProviders.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(string msg)
        {
            _notifier.Handle(new Notification(msg));
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : BaseEntity
        {
            var validator = validation.Validate(entity);
            if (validator.IsValid) return true;

            Notify(validator);
            return false;
        }
    }
}

using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
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

        protected static List<TE> PagedList<TE>(int page, int pageSize, List<TE> list) where TE : BaseEntity
        {
            page = page > 1 ? page - 1 : 0;
            var skip = page * pageSize;
            return list.Skip(skip).Take(pageSize).ToList();
        }
    }
}

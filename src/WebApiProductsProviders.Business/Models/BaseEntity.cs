using System;

namespace WebApiProductsProviders.Business.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        //public int PageSize { get; set; }
        //public int CurrentPage { get; set; }
        //public int TotalResults { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}

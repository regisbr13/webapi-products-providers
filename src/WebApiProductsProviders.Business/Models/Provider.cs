using System.Collections.Generic;
using WebApiProductsProviders.Business.Models.Enums;

namespace WebApiProductsProviders.Business.Models
{
    public class Provider : BaseEntity
    {
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public ProviderType ProviderType { get; set; }
        public bool Active { get; set; }
        public Address Address { get; set; }
        public List<Product> Products { get; set; }
    }
}

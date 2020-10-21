using System.Collections.Generic;

namespace S4.Entities.Models
{
    public interface ISupplier
    {
        string Address { get; set; }
        string City { get; set; }
        string CompanyName { get; set; }
        string ContactName { get; set; }
        string ContactTitle { get; set; }
        string Country { get; set; }
        string Fax { get; set; }
        string HomePage { get; set; }
        string Phone { get; set; }
        string PostalCode { get; set; }
        ICollection<Product> Products { get; set; }
        string Region { get; set; }
        int SupplierId { get; set; }
    }
}
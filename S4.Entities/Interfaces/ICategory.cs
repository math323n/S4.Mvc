using System.Collections.Generic;

namespace S4.Entities.Models
{
    public interface ICategory
    {
        int CategoryId { get; set; }
        string CategoryName { get; set; }
        string Description { get; set; }
        byte[] Picture { get; set; }
        ICollection<Product> Products { get; set; }
    }
}
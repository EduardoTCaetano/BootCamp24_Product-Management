using BootCamp24_Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootCamp24_Domain.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAll();
        Task<ProductModel> GetById(Guid id);
        Task Add(ProductModel product);
        Task Update(ProductModel product);
        Task Delete(Guid id);
        Task<bool> Exists(Guid id);
    }
}

using BootCamp24_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCamp24_Domain.Interfaces.Repository
{
    public interface IProductRepository : IRepository<ProductModel>
    {
        Task<IEnumerable<ProductModel>> GetAll();
    }
}

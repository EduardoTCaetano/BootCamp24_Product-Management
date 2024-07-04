using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCamp24_Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T model);
        Task<bool> Update(Guid id, T model);
        Task<bool> Delete(Guid id);
    }
}

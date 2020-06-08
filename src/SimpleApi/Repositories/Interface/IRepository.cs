using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPI.Repositories.Interface
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetById(int id);

        Task<T> Add(T t);

        Task<T> Update(T t);

        Task<bool> Delete(int id);
    }
}

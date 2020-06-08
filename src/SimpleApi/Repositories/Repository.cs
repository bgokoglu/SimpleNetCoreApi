using SimpleAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPI.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        public async Task<T> Add(T t) => throw new NotImplementedException();
        
        public async Task<bool> Delete(int id) => throw new NotImplementedException();
        
        public async Task<List<T>> GetAll() => throw new NotImplementedException();
        
        public async Task<T> GetById(int id) => throw new NotImplementedException();
        
        public async Task<T> Update(T t) => throw new NotImplementedException();
    }
}

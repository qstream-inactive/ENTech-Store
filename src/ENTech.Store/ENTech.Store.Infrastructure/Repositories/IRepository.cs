using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Repositories
{
    public interface IRepository
    {
        void Create<T>(int id, T data);

        void Update<T>(int id, T data);

        void Delete<T>(int id);

        bool Exists<T>(int id);

        T GetById<T>(int id);
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendFinder.Repository
{
    interface IRepository <T>
    {
         void Save();
        // T Add(T entity);
    }
}

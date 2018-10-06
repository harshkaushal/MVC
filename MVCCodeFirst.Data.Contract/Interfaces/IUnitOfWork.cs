using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Data.Contract
{
    public interface IUnitOfWork:IDisposable
    {
        IUserInfoRepository UserInfo { get; }
        void Commit();
    }
}

using MVCCodeFirst.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Manager
{
    public class CommonManager : BaseManager
    {
        public CommonManager(IUnitOfWork uow) : base(uow) { }
    }
}

using MVCCodeFirst.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Manager
{
    public class BaseManager
    {
        protected IUnitOfWork Uow { get; set; }
        public BaseManager(IUnitOfWork uow)
        {
            Uow = uow;
        }
    }
}

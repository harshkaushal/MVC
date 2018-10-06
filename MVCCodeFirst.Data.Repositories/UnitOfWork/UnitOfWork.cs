using MVCCodeFirst.Data.Contract;
using MVCCodeFirst.Data.Repositories.Context;
using MVCCodeFirst.Data.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Data.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext DbContext;

        public UnitOfWork(DataContext dbContext)
        {
            DbContext = dbContext;
        }

        public UnitOfWork()
        {
            DbContext = new DataContext();
        }

        public IUserInfoRepository UserInfo { get { return new UserInfoRepository(DbContext); } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        /// <summary>
        /// Commit all changes to Dbcontext
        /// </summary>
        public void Commit()
        {
            DbContext.SaveChanges();
        }

        void IUnitOfWork.Commit()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

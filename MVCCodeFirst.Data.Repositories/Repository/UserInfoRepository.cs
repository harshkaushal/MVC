using MVCCodeFirst.Data.Contract;
using MVCCodeFirst.Data.DataModels;
using MVCCodeFirst.Data.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Data.Repositories.Repository
{
    class UserInfoRepository : DataRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(DataContext dbContext) : base(dbContext) { }
        public List<UserInfo> GetAllUsers()
        {
            return DbContext.tbUserInfo.ToList();
        }

        public UserInfo GetUserByID(int ID)
        {
            return DbContext.tbUserInfo.Where(d => d.ID == ID).FirstOrDefault();
        }
        public int SaveUser(UserInfo userInfo)
        {
            var dbUserInfo = DbContext.tbUserInfo.Where(d => d.Email == userInfo.Email && d.ID != userInfo.ID).FirstOrDefault();
            if (dbUserInfo == null && userInfo.ID < 1)
            {
                DbContext.tbUserInfo.Add(userInfo);
                DbContext.SaveChanges();
                return 1;//record addedd successfully
            }
            else if (dbUserInfo != null && userInfo.ID < 1)
            {
                return 3;//email already exists
            }
            else
            {
                UserInfo tbUser = DbContext.tbUserInfo.Where(d => d.ID == userInfo.ID).FirstOrDefault();
                tbUser.FirstName = userInfo.FirstName;
                tbUser.LastName = userInfo.LastName;
                tbUser.Age = userInfo.Age;
                tbUser.Email = userInfo.Email;
                tbUser.IsActive = userInfo.IsActive;
                DbContext.SaveChanges();
                return 2;//record updated successfully

            }

        }
    }
}

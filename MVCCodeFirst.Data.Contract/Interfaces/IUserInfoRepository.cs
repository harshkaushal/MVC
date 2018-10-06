using MVCCodeFirst.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Data.Contract
{
    public interface IUserInfoRepository: IRepository<UserInfo>
    {
        List<UserInfo> GetAllUsers();
        int SaveUser(UserInfo userInfo);
        UserInfo GetUserByID(int ID);


    }
}

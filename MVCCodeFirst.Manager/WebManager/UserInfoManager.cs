using MVCCodeFirst.Common.ViewModels;
using MVCCodeFirst.Data.Contract;
using MVCCodeFirst.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Manager.WebManager
{
    public class UserInfoManager : BaseManager
    {
        private readonly CommonManager commonManager;
        public UserInfoManager(IUnitOfWork uow) : base(uow)
        {
            commonManager = new CommonManager(uow);

        }

        public List<UserInfoModel> GetAllUsers()
        {
            List<UserInfoModel> model = new List<UserInfoModel>();
            var dbUserInfo = Uow.UserInfo.GetAllUsers();
            model = AutoMapper.Mapper.Map(dbUserInfo, model);
            return model;
        }

        public UserInfoModel GetUserByID(int ID)
        {
            UserInfoModel model = new UserInfoModel();
            var dbUser = Uow.UserInfo.GetUserByID(ID);
            model = AutoMapper.Mapper.Map(dbUser, model);
            return model;
        }
        public int SaveUpdateUser(UserInfoModel model)
        {
            UserInfo tbUserInfo = new UserInfo();
            tbUserInfo = AutoMapper.Mapper.Map(model, tbUserInfo);
            return Uow.UserInfo.SaveUser(tbUserInfo);
        }
    }
}

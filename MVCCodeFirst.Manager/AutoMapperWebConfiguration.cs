using AutoMapper;
using MVCCodeFirst.Common.ViewModels;
using MVCCodeFirst.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCodeFirst.Manager
{
    public class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserInfoModel, UserInfo>()
               .ReverseMap();
            });

        }
    }
}

using AutoMapper;
using DowntimeRobot.Entity.Entity;
using DowntimeRobot.Service.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeRobot.Service.BLL.AutoMapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //var entityAssembly = typeof(EntityBase).Assembly;
            //var modelAssembly = typeof(DtoBase).Assembly;
            //var tp = modelAssembly.GetTypes().FirstOrDefault(a => a.BaseType == typeof(DtoBase));
            //if (tp == null) return;

            //var modelNamespace = tp.Namespace;

            //foreach (var entity in entityAssembly.GetTypes().Where(a => a.BaseType == typeof(EntityBase)))
            //{
            //    var model = modelAssembly.GetType($"{modelNamespace}.{entity.Name}{"Dto"}");
            //    if (model == null) continue;
            //    //
            //    try
            //    {
            //        //auto mapper  > 9.1  =  ReverseMap
            //        //CreateMap(entity, model).ReverseMap();
            //        CreateMap(entity, model).PreserveReferences();
            //        CreateMap(model, entity).PreserveReferences();
            //    }
            //    catch
            //    {
            //        continue;
            //    }
            //}

            AllowNullCollections = true;
            AllowNullDestinationValues = true;
            //add new data for mapping 
            CreateMap<User, UserDto>().ForAllOtherMembers(x => x.Ignore());
            CreateMap<UserDto, User>().ForAllOtherMembers(x => x.Ignore());
            CreateMap<UserNotification, UserNotificationDto>().ForAllOtherMembers(x => x.Ignore());
            CreateMap<UserNotificationDto, UserNotification>().ForAllOtherMembers(x => x.Ignore());
            CreateMap<Application, ApplicationDto>().ForAllOtherMembers(x => x.Ignore());
            CreateMap<ApplicationDto, Application>().ForAllOtherMembers(x => x.Ignore());
            CreateMap<Job, JobDto>().ForAllOtherMembers(x => x.Ignore());
            CreateMap<JobDto, Job>().ForAllOtherMembers(x => x.Ignore());
            //var user = AutoMapperInitialize.GetMapper.Map<UserDto, User>(dto);


        }

    }

    public static class AutoMapperInitialize
    {
        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            });
            config.AssertConfigurationIsValid();
            GetMapper = config.CreateMapper();
        }
        public static IMapper GetMapper { get; set; }


    }
}

using Humanizer;
using PestControl.Data;
using PestControl.Models;
using PestControl.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PestControl.Configurations

{

 
    public static class DataAccess
    {

        //private static string EncryptConnectionString(IConfiguration configuration)
        //{

        //    Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(configuration.GetConnectionString("AppDbContext"));

        //    string encryptedConnectionString = Convert.ToBase64String(b);

        //    return encryptedConnectionString;
        //}



        public static string GetConnectionString(IConfiguration configuration)
        {

            return configuration.GetConnectionString("AppDbContext");
            
        }
        private static string DecryptConnectionString(string connection)
        {
                
            Byte[] b = Convert.FromBase64String(connection);


            string decryptedConnectionString = System.Text.Encoding.ASCII.GetString(b);

            return decryptedConnectionString;

        }



        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)

        {
            string connectionString;
            connectionString = GetConnectionString(configuration);
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(DecryptConnectionString(connectionString)));
            return services;
        }

        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEnumService, EnumService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            return services;
        }

        public class DataMappingProfile : AutoMapper.Profile
        {
            public DataMappingProfile()
            {
                //CreateMap<Package, PackageDto>().ReverseMap();
                //CreateMap<Country, CountryDto>().ReverseMap();
                CreateMap<Appointment, AppointmentDto>().ReverseMap();
            }
        }
    }
}

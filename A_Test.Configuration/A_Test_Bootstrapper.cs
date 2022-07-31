using A_Test.Domain;
using A_Test.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace A_Test.Configuration
{
    public class A_Test_Bootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInfoRepository , InfoRepository>();
            services.AddDbContext<TestContext>(x => x.UseSqlServer(connectionString));
        }
    }
}

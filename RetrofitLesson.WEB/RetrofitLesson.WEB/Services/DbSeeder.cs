using Microsoft.AspNetCore.Identity;
using RetrofitLesson.WEB.Constants;
using RetrofitLesson.WEB.Data.Identity;

namespace RetrofitLesson.WEB.Services
{
    public static class DbSeeder
    {
        public static void SeedAll(this IApplicationBuilder app) 
        {
            
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) 
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                if (!roleManager.Roles.Any()) 
                {
                    roleManager.CreateAsync(new AppRole { 
                        Name = Roles.USER
                    });
                    roleManager.CreateAsync(new AppRole {
                        Name = Roles.ADMIN
                    });
                }
            }
        }
    }
}

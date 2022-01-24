using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyOwnIdentityApp.Library.Functions;
using MyOwnIdentityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOwnIdentityApp.Context
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IdentityAppContext>();

                context.Database.Migrate();
                if (!context.Users.Any())
                {
                    var user = new[]
                    {
                        new User(){ UserName="admin_example_mail@gmail.com", Password=PasswordHasher.PasswordHash("123456"), Role=RoleType.Admin},
                        new User(){ UserName="user_example_mail@gmail.com", Password=PasswordHasher.PasswordHash("1234567"), Role=RoleType.User}
                    };
                    context.Users.AddRange(user);
                    context.SaveChanges();
                }
            }
        }
    }
}

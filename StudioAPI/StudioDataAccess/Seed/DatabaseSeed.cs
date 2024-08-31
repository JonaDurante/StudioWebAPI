using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StudioModel.Domain;

namespace StudioDataAccess.Seed
{
    public static class DatabaseSeed
    {
        public static UserManager<UserApp> Manager { get; set; }
        public static RoleManager<IdentityRole> RoleManager { get; set; }

        private const string Admin = "admin";
        private const string TestUser = "user";
        private const string TestTeacher = "teacher";
        private const string CommonPassword = "Password123!";
        private const string AdminPassword = "AdminPassword123!";
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
           Manager = serviceProvider.GetRequiredService<UserManager<UserApp>>();
           RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roleNames = new string[] { Admin, TestUser, TestTeacher };

            foreach (var roleName in roleNames)
            {
                if (!await RoleManager.RoleExistsAsync(roleName))
                {
                    await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            if (await findUserExist(Admin))
            {
                var adminUser = new UserApp { UserName = Admin, Email = "admin@pegasus.net"};
                var createdUser = await Manager.CreateAsync(adminUser, AdminPassword);

                if (createdUser.Succeeded)
                {
                    await Manager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            if (await findUserExist(TestUser))
            {
                var testUser = new UserApp { UserName = TestUser, Email = "testuser@pegasus.net", Role = TestUser };
                var createdUser = await Manager.CreateAsync(testUser, CommonPassword);

                if (createdUser.Succeeded)
                {
                    await Manager.AddToRoleAsync(testUser, "Admin");
                }
            }

            if (await findUserExist(TestTeacher))
            {
                var testTeacher = new UserApp { UserName = TestTeacher, Email = "testteacher@pegasus.net", Role = TestTeacher };
                var createdUser = await Manager.CreateAsync(testTeacher, CommonPassword);

                if (createdUser.Succeeded)
                {
                    await Manager.AddToRoleAsync(testTeacher, "Admin");
                }
            }
        }

        private static async Task<bool> findUserExist(string name)
        {
            return await Manager.FindByNameAsync(name) == null;
        }
    }
}

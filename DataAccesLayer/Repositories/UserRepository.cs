using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Enteties;
using DataAccesLayer.Interfaces;
using DataAccesLayer.EF;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DataAccesLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDBContext context;

        public UserManager<User> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public SignInManager<User> SignInManager { get; set; }

        public UserRepository(AppDBContext context, UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            this.context = context;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;

        }

        public async Task<bool> Add(User entity)
        {
            var result = await UserManager.CreateAsync(entity);
            await context.SaveChangesAsync();
            await UserManager.AddToRoleAsync(entity,"User");          
            return result.Succeeded;
        }

        public async Task<bool> Delete(User entity)
        {
            var result = await UserManager.DeleteAsync(entity);
            await context.SaveChangesAsync();
            return result.Succeeded;
        }

        public async Task<bool> DeleteById(string id)
        {
            var user = await context.Users.FindAsync(id);
            return await this.Delete(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => UserManager.Users);
        }

        public async Task<User> GetById(string id)
        {
            return await UserManager.FindByIdAsync(id);
        }

        public async Task<bool> Update(User entity)
        {
            var user = await UserManager.FindByIdAsync(entity.Id);
            user.Name = entity.Name;
            user.Surname = entity.Surname;
            user.PhoneNumber = entity.PhoneNumber;
            user.Email = entity.Email;
            user.PasswordHash = entity.PasswordHash;
            var result = await UserManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await UserManager.FindByEmailAsync(email);
        }

        public async Task<User> LogIn(string email, string password)
        {
            var user = await UserManager.FindByEmailAsync(email);
            return user.PasswordHash == password ? user : null;
        }
    }
}

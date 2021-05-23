using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> Add(User entity);

        public Task<bool> Delete(User entity);

        public Task<bool> DeleteById(string id);

        public Task<IEnumerable<User>> GetAll();

        public Task<User> GetById(string id);

        public Task<User> GetByEmail(string email);

        public Task<bool> Update(User entity);

        public Task<User> LogIn(string email, string password);


    }
}

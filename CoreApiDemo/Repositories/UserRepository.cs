using CoreApiDemo.Models;
using CoreApiDemo.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiDemo.Repositories
{
    public class UserRepository
    {
        private readonly YpobDBContent _dbContext;

        #region "Construction Methods"
        public UserRepository(YpobDBContent dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region "Public Methods"
        public string getToken()
        {
            return JwtUtil.createTokenByBuilder(1);
        }

        public async Task<List<User>> getUsers()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<User> getUser(int id)
        {
            return await _dbContext.User.FindAsync(id);
        }

        public async Task<bool> addUser(User user)
        {
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> modifyUser(int id, User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> destroyUser(int id)
        {
            var User = await _dbContext.User.FindAsync(id);

            if (User == null)
            {
                return false;
            }

            _dbContext.User.Remove(User);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        #endregion

        #region "Private Methods"

        #endregion
    }
}

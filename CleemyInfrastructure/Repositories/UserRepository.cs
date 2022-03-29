using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyInfrastructure.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CleemyInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IEnumerableAdapter<DbUser, User> _dbUserToUserAdapter;
        private ApplicationContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            IEnumerableAdapter<DbUser, User> dbUserToUserAdapter,
            ApplicationContext context,
            ILogger<UserRepository> logger
            )
        {
            _dbUserToUserAdapter = dbUserToUserAdapter;
            _context = context;
            _logger = logger;
        }

        public User GetById(int userId)
        {
            if (userId < 1)
                throw new ArgumentException();

            try
            {
                var dbUser = _context.Users
                    .Include(c => c.AuthorizedCurrency)
                    .FirstOrDefault(p => p.Id == userId);
                var user = _dbUserToUserAdapter.Convert(dbUser);
                return user;
            }
            catch (Exception err)
            {
                _logger.LogError("Get User", err);
                return null;
            }
        }
    }
}
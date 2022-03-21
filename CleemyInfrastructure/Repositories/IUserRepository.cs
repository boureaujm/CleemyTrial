using CleemyCommons.Model;

namespace CleemyInfrastructure.Repositories
{
    public interface IUserRepository
    {
        User GetById(int userId);
    }
}
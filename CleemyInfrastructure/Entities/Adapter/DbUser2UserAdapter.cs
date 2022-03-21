using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using System.Collections.Generic;

namespace CleemyInfrastructure.entities.Adapter
{
    public class DbUser2UserAdapter : IEnumerableAdapter<DbUser, User>
    {
        public User Convert(DbUser source)
        {
            return new User
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                Id = source.Id,
                AuthorizedCurrency = new Currency { 
                    Code = source.AuthorizedCurrency.Code,
                    Label = source.AuthorizedCurrency.Label
                }
            };
        }

        public IEnumerable<User> Convert(IList<DbUser> list)
        {
            foreach (var item in list)
            {
                yield return Convert(item);
            }
        }
    }
}

using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IUserCollection
    {
        Task<User> GetUserById(string id);
        Task<User> GetUserByPersonAndRole(string person, string role);
        Task<User> GetUserByPerson(string person);
        Task<User> GetUserByUsername(string username);
        Task<List<User>> GetUsersByRole(string role);
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(string id);
    }
}
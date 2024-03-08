using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IRoleCollection
    {
        Task<Role> GetRoleById(string id);
        Task<Role> GetRoleByName(string name);
        Task<List<Role>> GetAllRoles();
        Task InsertRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(string id);
    }
}
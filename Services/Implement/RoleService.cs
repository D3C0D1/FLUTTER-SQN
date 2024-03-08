using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class RoleService: IRoleService
    {
        private readonly IRoleCollection _database = new RoleCollection();

        public async Task<ApiResponse> GetAll()
        {
            try
            {
                Console.WriteLine("RoleService: GetAll");
                List<Role> roles = await _database.GetAllRoles();
                if (roles.Count > 0)
                    return new ApiResponse(roles);
                return new ApiResponse(new ApiError("No Roles to show", SQNErrorCode.NoResult));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByName(string name)
        {
            Console.WriteLine($"RoleService: GetByName: id: {name}");
            try
            {
                Role role = await _database.GetRoleByName(name);
                if (role != null)
                    return new ApiResponse(role);
                return new ApiResponse(new ApiError($"The Role named {name} doesn't exist",
                    SQNErrorCode.RoleNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"Role: GetById: id: {id}");
            try
            {
                Role role = await _database.GetRoleById(id);
                if (role != null)
                    return new ApiResponse(role);
                return new ApiResponse(new ApiError($"The Role {id} doesn't exist", SQNErrorCode.RoleNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(RoleDTO roleDTO, string user)
        {
            Console.WriteLine("RoleService: Insert: RoleDTO");
            if (roleDTO == null)
                return new ApiResponse(new ApiError("A null objet can't be added for Role",
                    SQNErrorCode.NullValue));
            ApiResponse valid = await GetByName(roleDTO.Name);
            if (valid.Success)
                return new ApiResponse(new ApiError($"The Role with Name {roleDTO.Name} already exist",
                    SQNErrorCode.RoleNameAlreadyExist));
            IStatusService ss = new StatusService();
            valid = await ss.IsValid(roleDTO.Status);
            if (!valid.Success)
                return new ApiResponse(new ApiError($"The Status with id {roleDTO.Status} isn't valid",
                    SQNErrorCode.StatusIsNoValid));
            Role role = roleDTO.ToModel();
            ApiError validated = role.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            role.Creator = user;
            role.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertRole(role);
                return new ApiResponse(roleDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(RoleDTO roleDTO, string id, string user)
        {
            Console.WriteLine($"RoleService: Update: RoleDTO, id: {id}");
            if (roleDTO == null)
                return new ApiResponse(new ApiError($"A null objet can be used for update the Role {id}",
                    SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            if (roleDTO.id != string.Empty && !roleDTO.id.Equals(id))
                return new ApiResponse(new ApiError($"Trying to update an object diferent from {id}",
                    SQNErrorCode.NotMatchingValues));
            roleDTO.id = id;
            Role role = new ();
            ApiResponse valid = await GetByName(roleDTO.Name);
            if (valid.Success)
            {
                role = valid.Result;
                if(id.Equals(role.id.ToString()))
                    return new ApiResponse(new ApiError($"The Role with Name {roleDTO.Name} already exists", 
                        SQNErrorCode.RoleNameAlreadyExist));
            }
            roleDTO.id = id;
            role = roleDTO.ToModel();
            validated = role.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            role.Updater = user;
            role.UpdateDate = DateTime.Now;
            try
            {
                await _database.UpdateRole(role);
                return new ApiResponse(role);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"RoleService: Delete: id: {id}");
            try
            {
                await _database.DeleteRole(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }
    }
}
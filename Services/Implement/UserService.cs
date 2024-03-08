using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;
using System.Data;

namespace SQNBack.Services.Implement
{
    public class UserService: IUserService
    {
        private readonly IUserCollection _database = new UserCollection();

        public async Task<ApiResponse> GetByRole(string role)
        {
            Console.WriteLine($"UserService: GetByRole: role: {role}");
            try
            {
                List<User> users = await _database.GetUsersByRole(role);
                if (users.Count > 0)
                    return new ApiResponse(ToListDTO(users));
                return new ApiResponse(new ApiError($"No Users with {role} to show",
                    SQNErrorCode.UserNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByPerson(string person)
        {
            Console.WriteLine($"UserService: GetByPerson: person: {person}");
            try
            {
                User user = await _database.GetUserByPerson(person);
                if (user != null)
                    return new ApiResponse(user.ToDTO());
                else
                    return new ApiResponse(new ApiError($"The User with person {person} doesn't exist",
                        SQNErrorCode.UserNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByPersonAndRole(string person, string role)
        {
            Console.WriteLine($"UserService: GetByPersonAndRole: person: {person}, role: {role}");
            try
            {
                User user = await _database.GetUserByPersonAndRole(person, role);
                if (user != null)
                    return new ApiResponse(user);
                else
                    return new ApiResponse(
                        new ApiError($"The User with person {person} and role {role} doesn't exist",
                        SQNErrorCode.UserNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"UserService: GetByOd: Id: {id}");
            try
            {
                User user = await _database.GetUserById(id);
                if (user != null)
                    return new ApiResponse(user);
                else
                    return new ApiResponse(new ApiError($"The User with Id {id} doesn't exist",
                        SQNErrorCode.UserNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByUserName(string userName)
        {
            Console.WriteLine($"UserService: GetByUserName: userName: {userName}");
            try
            {
                User user = await _database.GetUserByUsername(userName);
                if (user != null)
                    return new ApiResponse(user);
                else
                    return new ApiResponse(new ApiError($"The User {userName} doesn't exist",
                        SQNErrorCode.UserNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(UserDTO userDTO, string creator)
        {
            Console.WriteLine("UserService: Insert: UserDTO");
            if (userDTO == null)
                return new ApiResponse(new ApiError("A null objet can¿t be added for User", 
                    SQNErrorCode.NullValue));
            User user = userDTO.ToModel();
            ApiError validated = user.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await DataValidation(userDTO, user.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            user.Creator = creator;
            user.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertUser(user);
                return new ApiResponse(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(UserDTO userDTO, string id, string editor)
        {
            Console.WriteLine($"UserService: Update: UserDTO, id: {id}");
            if (userDTO == null)
                return new ApiResponse(new ApiError($"A null objet can´t be used for update the User {id}",
                    SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            if (userDTO.id != string.Empty && !userDTO.id.Equals(id))
                return new ApiResponse(new ApiError($"Trying to update am object diferent from {id}", 
                    SQNErrorCode.NotMatchingValues));
            userDTO.id = id;
            User user = userDTO.ToModel();
            validated = user.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await DataValidation(userDTO, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            user.Updater = editor;
            user.UpdateDate = DateTime.Now;
            try
            {
                await _database.UpdateUser(user);
                return new ApiResponse(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"UserService: Delete: id: {id}");
            try
            {
                await _database.DeleteUser(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        private async Task<ApiError> DataValidation(UserDTO userDTO, string id)
        {
            Console.WriteLine($"UserService: DataValidation: UserDTO, id: {id}");
            try
            {
                User valida = await _database.GetUserByPerson(userDTO.Person);
                if (valida != null)
                    if (valida.id.ToString() != id)
                        return new ApiError($"User with person {userDTO.Person} already exist",
                            SQNErrorCode.UserAlreadyExist);
                valida = await _database.GetUserByUsername(userDTO.UserName);
                if (valida != null)
                    if (valida.id.ToString() != id)
                        return new ApiError($"User with username {userDTO.UserName} already exist",
                            SQNErrorCode.UserAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }
        private static List<UserDTO> ToListDTO(List<User> users)
        {
            Console.WriteLine($"UserService: ToListDTO");
            List<UserDTO> response = new();
            foreach (User u in users)
                response.Add(u.ToDTO());
            return response;
        }
    }
}
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class StatusService: IStatusService
    {
        private readonly IStatusCollection _database = new StatusCollection();

        public async Task<ApiResponse> GetAll()
        {
            Console.WriteLine("StatusService: GetAll");
            try
            {
                List<Status> statuses = await _database.GetAllStatus();
                if (statuses.Count > 0)
                    return new ApiResponse(ToListDTO(statuses));
                return new ApiResponse(new ApiError("No Statuses to show", SQNErrorCode.NoResult));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"StatusService: GetByid: id: {id}");
            try
            {
                Status status = await _database.GetStatusById(id);
                if (status != null)
                    return new ApiResponse(status.ToDTO());
                return new ApiResponse(new ApiError($"The Status {id} doesn't exist", SQNErrorCode.StatusNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(StatusDTO statusDTO, string user)
        {
            Console.WriteLine($"StatusService: Insert: StatusDTO");
            if (statusDTO == null)
                return new ApiResponse(new ApiError("A null objet can't be added for Status", 
                    SQNErrorCode.NullValue));
            ApiError validated = await DataValidation(statusDTO);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            Status status = statusDTO.ToModel();
            validated = status.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            status.Name = status.Name.ToUpper();
            status.Code = status.Code.ToUpper();
            status.Creator = user;
            status.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertStatus(status);
                return new ApiResponse(status.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(StatusDTO statusDTO, string id, string user)
        {
            Console.WriteLine($"StatusService: Update: StatusDTO, id: {id}");
            if (statusDTO == null)
                return new ApiResponse(new ApiError($"A null objet can´t be used for update the Status {id}", 
                    SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            if (!string.IsNullOrWhiteSpace(statusDTO.id) && !statusDTO.id.Equals(id))
                return new ApiResponse(new ApiError($"Trying to update am object diferent from {id}",
                    SQNErrorCode.NotMatchingValues));
            statusDTO.id = id;
            Status status = statusDTO.ToModel();
            validated = status.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await DataValidation(statusDTO);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            status.Name = status.Name.ToUpper();
            status.Code = status.Code.ToUpper();
            status.Updater = user;
            status.UpdateDate = DateTime.Now;
            try
            {
                await _database.Updatestatus(status);
                return new ApiResponse(status.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"StatusService: Delete: id: {id}");
            try
            {
                await _database.DeleteStatus(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> IsValid(string id)
        {
            Console.WriteLine($"StatusService: IsValid: id: {id}");
            ApiResponse verify = await GetById(id);
            if (!verify.Success)
                return verify;
            if (!verify.Result.Valid)
                return new ApiResponse(new ApiError($"The Status {id} isn't valid", SQNErrorCode.StatusIsNoValid));
            return verify;
        }

        public async Task<ApiResponse> IsForCreation(string id)
        {
            Console.WriteLine($"StatusService: IsForCreation: id: {id}");
            ApiResponse verify = await GetById(id);
            if (!verify.Success)
                return verify;
            if (!verify.Result.Creation)
                return new ApiResponse(new ApiError($"The Status {id} is not for creation", SQNErrorCode.StatusNotForCreation));
            return verify;
        }

        public async Task<ApiResponse> IsForCreation()
        {
            Console.WriteLine("StatusService: IsForCreation");
            try
            {
                Status status = await _database.IsForCreation();
                if (status != null)
                    return new ApiResponse(status.ToDTO());
                return new ApiResponse(new ApiError("The status for creation doesn't exist", SQNErrorCode.StatusNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }

        }

        public async Task<ApiResponse> ListByValid(bool valid) 
        {
            Console.WriteLine($"StatusService: ListByValid: valid: {valid}");
            try
            {
                List<Status> statuses = await _database.ListByValid(valid);
                if (statuses.Count > 0)
                    return new ApiResponse(ToListDTO(statuses));
                return new ApiResponse(new ApiError($"The statuses with valid {valid} don't exist", SQNErrorCode.StatusNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        private async Task<ApiError> DataValidation(StatusDTO dto)
        {
            Console.WriteLine("StatusService: DataValidation: StutusDTO");
            if (dto.Creation)
            {
                ApiError validated = await ForCreationValidation(dto);
                if (validated.Code != SQNErrorCode.None)
                    return validated;
            }
            try
            {
                Status valida = await _database.GetStatusByName(dto.Name.ToUpper());
                if (valida != null)
                    if (valida.id.ToString() != dto.id)
                        return new ApiError($"Status with name {dto.Name} already exist",
                            SQNErrorCode.StatusAlreadyExist);
                valida = await _database.GetStatusByCode(dto.Code.ToUpper());
                if (valida != null)
                    if (valida.id.ToString() != dto.id)
                        return new ApiError($"Status with code {dto.Code} already exist",
                            SQNErrorCode.StatusAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        private async Task<ApiError> ForCreationValidation(StatusDTO dto)
        {
            Console.WriteLine("StatusService: ForCreationValidation: StatusDTO");
            ApiResponse api = await  IsForCreation();
            if (!api.Success)
                return new ApiError();
            if (!string.IsNullOrWhiteSpace(dto.id) && dto.Creation)
                return new ApiError("Already exist a Status for creation can't exist more than one",
                    SQNErrorCode.StatusAlreadyExist);
            if (!dto.id.Equals(api.Result.id) && dto.Creation)
                return new ApiError("Already exist a Status for creation can't exist more than one",
                    SQNErrorCode.StatusAlreadyExist);
            return new ApiError();
        }

        private static List<StatusDTO> ToListDTO(List<Status> area)
        {
            Console.WriteLine("StatusService: ToListDTO");
            List<StatusDTO> response = new();
            foreach (Status s in area)
                response.Add(s.ToDTO());
            return response;
        }
    }
}
using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class ExpertService: IExpertService
    {
        private readonly IExpertCollection _database = new ExpertCollection();

        public async Task<ApiResponse> GetAll()
        {
            Console.WriteLine("In ExpertService at GetAll()");
            try
            {
                List<Expert> experts = await _database.GetAllExperts();
                if (experts.Count > 0)
                    return new ApiResponse(ToListDTO(experts));
                return new ApiResponse(new ApiError("No Experts to show", SQNErrorCode.ExpertNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"ExpertService: GetById: id {id}");
            try
            {
                Expert expert = await _database.GetExpertById(id);
                if (expert != null)
                    return new ApiResponse(expert.ToDTO());
                return new ApiResponse(new ApiError($"The Expert {id} doesn't exist", SQNErrorCode.ExpertNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string docTye, string docNumber)
        {
            Console.WriteLine($"ExpertService: GetById: docType {docTye}, {docNumber}");
            try
            {
                Expert expert = await _database.GetExpertById(docTye, docNumber);
                if (expert != null)
                    return new ApiResponse(expert.ToDTO());
                return new ApiResponse(new ApiError($"The Expert with document {docNumber} doesn't exist",
                    SQNErrorCode.ExpertNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByCellPhone(string cellPhone)
        {
            Console.WriteLine($"ExpertService: GetByCellPhone: cellPhone {cellPhone}");
            try
            {
                Expert expert = await _database.GetExpertByCellPhone(cellPhone);
                if (expert != null)
                    return new ApiResponse(expert.ToDTO());
                return new ApiResponse(new ApiError($"The Expert with cellular {cellPhone} doesn't exist",
                    SQNErrorCode.ExpertNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByEmail(string email)
        {
            Console.WriteLine($"ExpertService: GetByEmail: email: {email}");
            try
            {
                Expert expert = await _database.GetExpertByEmail(email.ToLower());
                if (expert != null)
                    return new ApiResponse(expert.ToDTO());
                return new ApiResponse(new ApiError($"The Expert with email {email} doesn't exist",
                    SQNErrorCode.ExpertNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> NotifyExperts(UbicationDTO ubication)
        {
            Console.WriteLine("ExpertService: NotifyExperts: UbicationDTO");
            List<Expert> result = new();
            try
            {
                List<Expert> experts = await _database.GetAllExperts();
                if (experts.Count > 0)
                {
                    foreach (Expert e in experts)
                        if (e.Ubication.Latitude <= ubication.Latitude && e.Ubication.Longitude <= ubication.Latitude)
                            result.Add(e);
                    return new ApiResponse(ToListDTO(experts));
                }
                return new ApiResponse(new ApiError("No Experts to notify", SQNErrorCode.ExpertNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(ExpertDTO expertDTO, string user)
        {
            Console.WriteLine($"ExpertService: Insert: ExpertDTO");
            if (expertDTO == null)
                return new ApiResponse(new ApiError("A null objet can be added for Expert", 
                    SQNErrorCode.NullValue));
            expertDTO.Email = expertDTO.Email.ToLower();
            Expert expert = expertDTO.ToModel();
            ApiError validated = expert.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ExpertEmailValidation(expert.Email, expert.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ExpertCellPhoneValidation(expert.CellPhone, expert.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ExpertIdValidation(expert.DocType, expert.DocNumber, expert.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            expert.Creator = user;
            expert.Status = String.Empty;
            expert.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertExpert(expert);
                return new ApiResponse(expert.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(ExpertDTO expertDTO, string user, string id)
        {
            Console.WriteLine($"ExpertService: Update: id {id}");
            if (expertDTO == null)
                return new ApiResponse(new ApiError($"A null objet can be used for update the Expert {id}", 
                    SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            expertDTO.Email = expertDTO.Email.ToLower();
            Expert expert = expertDTO.ToModel();
            validated = expert.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ExpertEmailValidation(expert.Email, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ExpertCellPhoneValidation(expert.CellPhone, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ExpertIdValidation(expert.DocType, expert.DocNumber, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            expert.id = new ObjectId(id);
            expert.Updater = user;
            expert.UpdateDate = DateTime.Now;
            try
            {
                await _database.UpdateExpert(expert);
                return new ApiResponse(expert.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> AddUbication(UbicationDTO ubication, string id, string user)
        {
            Console.WriteLine($"ExpertService: AddUbication: UbicationDTO,  id: {id}");
            if (ubication == null)
                return new ApiResponse(new ApiError($"A null objet can't be used for update the Expert {id}", 
                    SQNErrorCode.NullValue));
            Expert expert = await _database.GetExpertById(id);
            if (expert == null)
                return new ApiResponse(new ApiError($"The Expert {id} doesn't exist", 
                    SQNErrorCode.ExpertNotFound));
            ApiError apiError = await StatusValidation(expert.id.ToString());
            if (apiError.Code != SQNErrorCode.None)
                return new ApiResponse(apiError);
            apiError = ubication.ValidateDTO();
            if (apiError.Code != SQNErrorCode.None)
                return new ApiResponse(apiError);
            expert.Ubication = ubication;
            expert.Updater = user;
            expert.UpdateDate = DateTime.Now;
            try
            {
                await _database.UpdateExpert(expert);
                return new ApiResponse(expert.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"ExpertService: Delete id {id}");
            try
            {
                await _database.DeleteExpert(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        private async Task<ApiError> ExpertCellPhoneValidation(string cellPhone, string id)
        {
            Console.WriteLine($"ExpertService: ExpertCellPhoneValidation: cellPhone: {cellPhone}, id: {id}");
            ApiResponse cellResp = await GetByCellPhone(cellPhone);
            if (cellResp.Success)
            {
                ExpertDTO dto = cellResp.Result;
                if (!id.Equals(dto.id))
                    return new ApiError($"An Expert with the cellular cellPhone {cellPhone} already exist",
                        SQNErrorCode.CellPhoneAlreadyExisit);
            }
            return new ApiError();
        }

        private async Task<ApiError> ExpertEmailValidation(string email, string id)
        {
            Console.WriteLine($"ExpertService: ExpertEmailValidation: email: {email}, id: {id}");
            bool valid = GeneralValidatons.ValidateEmail(email);
            if (!valid)
                return new ApiError("Invalid email format", SQNErrorCode.InvalidEmail);
            ApiResponse emailResp = await GetByEmail(email.ToLower());
            if (emailResp.Success)
            {
                ExpertDTO dto = emailResp.Result;
                if (!id.Equals(dto.id))
                    return new ApiError($"The Expert with the email {email} already exist",
                        SQNErrorCode.EmailAlreadyExist);
            }
            return new ApiError();
        }

        private async Task<ApiError> ExpertIdValidation(string docType, string docNumber, string id)
        {
            Console.WriteLine($"ExpertService: ExpertIdValidation: docType: {docType}, docNumber: {docNumber}");
            ApiResponse idResp = await GetById(docType, docNumber);
            if (idResp.Success)
            {
                ExpertDTO dto = idResp.Result;
                if (!id.Equals(dto.id))
                    return new ApiError($"The Expert with the identication {docNumber} already exist",
                        SQNErrorCode.ExpertAlreadyExist);
            }
            return new ApiError();
        }

        private static async Task<ApiError> StatusValidation(string status)
        {
            Console.WriteLine($"ExpertService: StatusValidation: status: {status}");
            StatusService ss = new();
            ApiResponse sar = await ss.IsValid(status);
            if (!sar.Success)
                return new ApiError("The Expert's status isn't valid", SQNErrorCode.ExpertNotValid);
            return new ApiError();
        }

        private static List<ExpertDTO> ToListDTO(List<Expert> experts)
        {
            Console.WriteLine($"ExpertService at ToListDTO Experts ");
            List<ExpertDTO> response = new();
            foreach (Expert e in experts)
                response.Add(e.ToDTO());
            return response;
        }
    }
}
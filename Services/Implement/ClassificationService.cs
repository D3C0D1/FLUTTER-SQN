using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class ClassificationService: IClassificationService
    {
        private readonly IClassificationCollection _database = new ClassificationCollection();

        public async Task<ApiResponse> GetAll()
        {
            List<Classification> classifications = await _database.GetAllClassifications();
            if (classifications.Count > 0)
                return new ApiResponse(ToListDTO(classifications));
            return new ApiResponse(new ApiError("No classifications to show", SQNErrorCode.ClassificationNotFound));
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Classification classification = await _database.GetClassificationById(id);
            if (classification != null)
                return new ApiResponse(classification.ToDTO());
            return new ApiResponse(new ApiError("The Classification " + id + " doesn't exist", 
                SQNErrorCode.ClassificationNotFound));
        }

        public async Task<ApiResponse> Insert(ClassificationDTO classificationDTO, string user)
        {
            if (classificationDTO == null)
                return new ApiResponse(new ApiError("A null objet can be added for Classification", 
                    SQNErrorCode.NullValue));
            Classification classification = classificationDTO.ToModel();
            ApiError validated = classification.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await DataValidation(classification.Name);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            classification.Creator = user;
            classification.CreationDate = DateTime.Now;
            await _database.InsertClassification(classification);
            return new ApiResponse(classification.ToDTO());
        }

        public async Task<ApiResponse> Update(ClassificationDTO classificationDTO, string id, string user)
        {
            if (classificationDTO == null)
                return new ApiResponse(new ApiError("A null objet can be used for update the Classification " + id,
                    SQNErrorCode.NullValue));
            Classification classification = classificationDTO.ToModel();
            ApiError validated = classification.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await DataValidation(classification.Name);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            classification.id = new ObjectId(id);
            classification.Updater = user;
            classification.UpdateDate = DateTime.Now;
            await _database.UpdateClassification(classification);
            return new ApiResponse(classification.ToDTO());
        }

        public async Task<ApiResponse> Delete(string id)
        {
            try
            {
                await _database.DeleteClassification(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> validateClassifications(List<DataElementDTO> classifications)
        {
            StatusService ss = new();
            List<ClassificationDTO> response = new List<ClassificationDTO>();
            foreach (DataElementDTO de in classifications)
            {
                ApiResponse data = await GetById(de.Id);
                if (!data.Success)
                    return data;
                Classification cl = data.Result;
                response.Add(cl.ToDTO());
                data = await ss.GetById(cl.Status);
                Status st = data.Result;
                if (!st.Valid)
                    return new ApiResponse(new ApiError("The classification " + cl.id.ToString() + " is not valid",
                        SQNErrorCode.StatusIsNoValid));
            }
            return new ApiResponse(response);
        }

        private async Task<ApiError> DataValidation(string name)
        {
            Classification classification = await _database.GetClassificationByName(name);
            if (classification != null)
                return new ApiError("The Classification " + name + " already exist", SQNErrorCode.ClassificationAlreadyExist);
            return new ApiError();
        }

        private List<ClassificationDTO> ToListDTO(List<Classification> classifications)
        {
            List<ClassificationDTO> response = new();
            foreach (Classification c in classifications)
                response.Add(c.ToDTO());
            return response;
        }
    }
}
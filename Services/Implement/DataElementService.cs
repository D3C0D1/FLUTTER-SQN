using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class DataElementService: IDataElementService
    {
        private readonly IDataElementCollection _database = new DateElementCollection();

        public async Task<ApiResponse> GetAll()
        {
            Console.WriteLine("DataElementService: GetAll");
            try
            {
                List<DataElement> datas = await _database.GetAllDocumentElements();
                if (datas.Count > 0)
                    return new ApiResponse(ToListDTO(datas));
                return new ApiResponse(new ApiError("No Document Elements to show", SQNErrorCode.NoResult));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"DataElementService: GetById: id {id}");
            try
            {
                DataElement dateElement = await _database.GetDocumentElementById(id);
                if (dateElement != null)
                    return new ApiResponse(dateElement.ToDTO());
                return new ApiResponse(new ApiError($"The document Element {id} doesn't exist", 
                    SQNErrorCode.DataElementNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByGroup(string group)
        {
            Console.WriteLine($"DataElementService: GetByGroup: group {group}");
            try
            {
                List<DataElement> datas = await _database.GetDocumentElementsByGroup(group.ToUpper());
                if (datas.Count > 0)
                    return new ApiResponse(ToListDTO(datas));
                return new ApiResponse(new ApiError("No Document Elements to show",
                    SQNErrorCode.DataElementNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(DataElementDTO dataDTO, string user)
        {
            Console.WriteLine("DataElementService: Insert");
            if (dataDTO == null)
                return new ApiResponse(new ApiError("A null objet can be used for creating a new Data Element", 
                    SQNErrorCode.NullValue));
            DataElement data = dataDTO.ToModel();
            ApiError validated = data.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await DataValidation(dataDTO.Name.ToUpper(), dataDTO.Group.ToUpper(), string.Empty);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            data.Name = data.Name.ToUpper();
            data.Group = data.Group.ToUpper();
            data.Creator = user;
            data.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertDocumentElement(data);
                return new ApiResponse(data.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(DataElementDTO dataDTO, string id, string user)
        {
            Console.WriteLine($"DataElementService: Update: id {id}");
            if (dataDTO == null)
                return new ApiResponse(new ApiError($"A null objet can be used for updating the Document Element {id}", 
                    SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            if (!string.IsNullOrWhiteSpace(dataDTO.Id) && !dataDTO.Id.Equals(id))
                return new ApiResponse(new ApiError($"Trying to update an object diferent from {id}", 
                    SQNErrorCode.NotMatchingValues));
            validated = await DataValidation(dataDTO.Name.ToUpper(), dataDTO.Group.ToUpper(), id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            dataDTO.Id = id;
            try {
                DataElement de = await Update(dataDTO, user);
                await _database.UpdateDocumentElement(de);
                return new ApiResponse(de.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"DataElementService: Deleete: id {id}");
            try
            {
                await _database.DeleteDocumentElement(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }
        
        private async Task<ApiError> DataValidation(string name, string group, string? id)
        {
            Console.WriteLine($"DataElementService: DataValidation: id {id}");
            try
            {
                DataElement data = await _database.GetDocumentElementsByNameAndGroup(name.ToUpper(), group.ToUpper());
                if (data != null && string.IsNullOrEmpty(id))                  
                        return new ApiError($"Data Element with name {name} and group {group} already exist",
                            SQNErrorCode.DataElementAlreadyExist);
                if (data != null && !string.IsNullOrEmpty(id) && id.Equals(data.Id.ToString()))
                    return new ApiError($"Data Element with name {name} and group {group} already exist",
                        SQNErrorCode.DataElementAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

         private async Task<DataElement> Update (DataElementDTO dataDTO, string user)
         {
            Console.WriteLine($"DataElementService: Update: Before Updating");
            DataElement dElement = dataDTO.ToModel();
            ApiError validated = dElement.ValidateModel();
            try
            {
                if (validated.Code != SQNErrorCode.None)
                    throw new Exception($"{{Message: {validated.Message}, Code: {validated.Code} }}");
                validated = await DataValidation(dataDTO.Name, dataDTO.Group, dataDTO.Id);
                if (validated.Code != SQNErrorCode.None)
                    throw new Exception($"{{Message: {validated.Message}, Code: {validated.Code} }}");
                dElement.Updater = user;
                dElement.UpdateDate = DateTime.Now;
                DataElement de = await _database.GetDocumentElementById(dataDTO.Id);
                dElement.Creator = de.Creator;
                dElement.CreationDate = de.CreationDate;
                dElement.Name = string.IsNullOrWhiteSpace(dElement.Name) ? de.Name : dElement.Name.ToUpper();
                dElement.Group = string.IsNullOrWhiteSpace(dElement.Group) ? de.Group : dElement.Group.ToUpper();
                dElement.Status = string.IsNullOrWhiteSpace(dElement.Status) ? de.Status : dElement.Status;
                dElement.Icon = string.IsNullOrWhiteSpace(dElement.Icon) ? de.Icon : dElement.Icon;
                return dElement;
            }
            catch (Exception)
            {
                throw;
            }
         }

        private static List<DataElementDTO> ToListDTO(List<DataElement> area)
        {
            Console.WriteLine($"DataElementService: ToListDTO");
            List<DataElementDTO> response = new();
            foreach (DataElement dt in area)
                response.Add(dt.ToDTO());
            return response;
        }
    }
}
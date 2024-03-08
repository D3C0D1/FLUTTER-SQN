using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class AreaService: IAreaService
    {
        private readonly IAreaCollection _database = new AreaCollection();

        public async Task<ApiResponse> GetAll()
        {
            Console.WriteLine("AreaService: GetAll");
            try
            {
                List<Area> areas = await _database.GetAllAreas();
                if (areas.Count > 0)
                    return new ApiResponse(ToListDTO(areas));
                return new ApiResponse(new ApiError("No Areas to show", SQNErrorCode.AreaNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"AreaService: GetById: id {id}");
            try
            {
                Area area = await _database.GetAreaById(id);
                if (area == null)
                    return new ApiResponse(new ApiError($"The Area {id} doesn't exist", SQNErrorCode.AreaNotFound));
                return new ApiResponse(area.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByName(string name)
        {
            Console.WriteLine($"AreaService: GetByName: name {name}");
            try
            {
                Area area = await _database.GetAreaByName(name.ToUpper());
                if (area == null)
                    return new ApiResponse(new ApiError($"The Area {name} Doesn't exist", SQNErrorCode.AreaNotFound));
                return new ApiResponse(area.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByNameAndOrder(string name, int order)
        {
            Console.WriteLine($"AreaService: GetByNameAndOrder: name {name} order {order}");
            try
            {
                ApiResponse apiRes = await GetByName(name);
                if (!apiRes.Success)
                    return new ApiResponse(new ApiError($"Area with Name {name} and Order {order} doesn't exist",
                        SQNErrorCode.RangeNotFound));
                AreaDTO area = apiRes.Result;
                RangeDTO range = new();
                bool found = false;
                foreach (RangeDTO r in area.Ranges)
                    if (r.Order == order)
                    {
                        range = r;
                        found = true;
                    }
                if (!found)
                    return new ApiResponse(new ApiError($"Area with Name {name} and Order {order} doesn't exist",
                        SQNErrorCode.RangeNotFound));
                return new ApiResponse(range);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(AreaDTO dto, string user)
        {
            Console.WriteLine("AreaService: Insert");
            if (dto == null)
                return new ApiResponse(new ApiError("A null objet can't be added for Area", 
                    SQNErrorCode.NullValue));
            ApiError validated = await DataValidation(dto);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            Area area = dto.ToModel();
            validated = area.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            area.Name = area.Name.ToUpper();
            area.Creator = user;
            area.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertArea(area);
                return new ApiResponse(area.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> InsertRange(RangeDTO dto, string user)
        {
            Console.WriteLine("AreaService: InsertRange");
            if (dto == null)
                return new ApiResponse(new ApiError("A null Range objet can't be added to Area",
                    SQNErrorCode.NullValue));
            ApiError validated = dto.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            ApiResponse resp = await GetByName(dto.Area);
            if (!resp.Success)
                return resp;
            AreaDTO area = resp.Result;
            List<RangeDTO> compare = area.Ranges.FindAll(r => r.Order == dto.Order);
            if (compare.Count > 0)
                return new ApiResponse(new ApiError($"Area with name {dto.Area} and order {dto.Order} allready exist",
                    SQNErrorCode.AreaAlreadyExist));
            area.Ranges.Add(dto);
            try
            {
                await _database.UpdateArea(area.ToModel());
                return new ApiResponse(area);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(AreaDTO dto, string id, string user)
        {
            Console.WriteLine("AreaService: Update");
            if (dto == null)
                return new ApiResponse(new ApiError($"A null objet can't be used for update the Area {id}",
                    SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            if (!string.IsNullOrWhiteSpace(dto.id) && !dto.id.Equals(id))
                return new ApiResponse(new ApiError($"Trying to update a Area diferent from {id}",
                    SQNErrorCode.UpdateIdNotMatch));
            dto.id = id;
            try
            {
                Area upd = await Update(dto, user);
                await _database.UpdateArea(upd);
                return new ApiResponse(upd.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"AreaService: Delete: id {id}");
            try
            {
                await _database.DeleteArea(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        private static List<AreaDTO> ToListDTO(List<Area> area)
        {
            Console.WriteLine("AreaService: ToListDTO");
            List<AreaDTO> response = new();
            foreach (Area a in area)
                response.Add(a.ToDTO());
            return response;
        }

        private async Task<ApiError> DataValidation(AreaDTO dto)
        {
            Console.WriteLine("AreaService: DataValidation");
            ApiResponse validated = await GetByName(dto.Name.ToUpper());
            if (validated.Success)
            {
                if (string.IsNullOrWhiteSpace(dto.id))
                    return new ApiError($"Area with name {dto.Name} allready exist",
                            SQNErrorCode.AreaAlreadyExist);
                Area area = validated.Result;
                if (!dto.id.Equals(area.id.ToString()))
                    return new ApiError($"Area with name {dto.Name} allready exist",
                            SQNErrorCode.AreaAlreadyExist);
                foreach (RangeDTO ra in area.Ranges)
                {
                    List<RangeDTO> compare = dto.Ranges.FindAll(r => r.Order == ra.Order);
                    if (compare.Count > 1)
                        return new ApiError($"Area with name {ra.Area} and order {ra.Order} allready exist",
                            SQNErrorCode.AreaAlreadyExist);
                }
            }
            return new ApiError();
        }

        private async Task<Area> Update(AreaDTO areaDTO, string user)
        {
            Console.WriteLine("AreaService: Update: Before to Update");
            Area aDTO = areaDTO.ToModel();
            ApiError validated = aDTO.ValidateModel();
            try
            {
                if (validated.Code != SQNErrorCode.None)
                    throw new Exception($"{{Message: {validated.Message}, Code: {validated.Code} }}");
                validated = await DataValidation(areaDTO);
                if (validated.Code != SQNErrorCode.None)
                    throw new Exception($"{{Message: {validated.Message}, Code: {validated.Code} }}");
                aDTO.Updater = user;
                aDTO.UpdateDate = DateTime.Now;
                Area area = await _database.GetAreaById(areaDTO.id);
                aDTO.Creator = area.Creator;
                aDTO.CreationDate = area.CreationDate;
                aDTO.Name = string.IsNullOrWhiteSpace(aDTO.Name) ? area.Name : aDTO.Name.ToUpper();
                return aDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
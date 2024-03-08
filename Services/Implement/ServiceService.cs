using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class ServiceService: IServiceService
    {
        private readonly IServicesCollection _database = new ServicesCollection();

        public async Task<ApiResponse> GetAll()
        {
            Console.WriteLine("ServiceService: GetAll");
            try
            {
                List<Service> services = await _database.GetAllServices();
                if (services.Count > 0)
                    return new ApiResponse(ToListDTO(services));
                return new ApiResponse(new ApiError("No Services to show", SQNErrorCode.NoResult));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByCustomer(string customer)
        {
            Console.WriteLine($"ServiceService: GetBycustomer: customer: {customer}");
            try
            {
                List<Service> services = await _database.GetServicesByCustomer(customer);
                if (services.Count > 0)
                    return new ApiResponse(ToListDTO(services));
                return new ApiResponse(new ApiError($" the customer {customer} hasn't Services to show", SQNErrorCode.ServiceNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByExpert(string expert)
        {
            Console.WriteLine($"ServiceService: GetByExpert: expert: {expert}");
            try
            {
                List<Service> services = await _database.GetServicesByExpert(expert);
                if (services.Count > 0)
                    return new ApiResponse(ToListDTO(services));
                return new ApiResponse(new ApiError($"The Customer {expert} hassn't services to show",
                    SQNErrorCode.ServiceNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByCustomerAndStatus(string customer, string status)
        {
            Console.WriteLine($"ServiceService: GetByCustomerAndStatus: custome: {customer}, estatus {status}");
            try
            {
                List<Service> service = await _database.GetServiceByCustomerAndStatus(customer, status);
                if (service.Count > 0)
                    return new ApiResponse(ToListDTO(service));
                return new ApiResponse(new ApiError($"The Service with customer {customer} and status {status} doesn't exist",
                    SQNErrorCode.ServiceNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByExpertAndStatus(string expert, string status)
        {
            Console.WriteLine($"ServiceService: GetByExpertAndStatus: custome: {expert}, estatus {status}");
            try
            {
                List<Service> service = await _database.GetServiceByExpertAndStatus(expert, status);
                if (service.Count > 0)
                    return new ApiResponse(ToListDTO(service));
                return new ApiResponse(new ApiError($"The Service with expert {expert} and status {status} doesn't exist",
                    SQNErrorCode.ServiceNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"ServiceService: GetById: id: {id}");
            try
            {
                Service service = await _database.GetServiceById(id);
                if (service != null)
                    return new ApiResponse(service.ToDTO());
                return new ApiResponse(new ApiError($"The Service {id} doesn't exist", SQNErrorCode.ServiceNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> CreateService(ServiceDTO serviceDTO, string user)
        {
            Console.WriteLine("ServiceService: CreateService: ServiceDTO");
            if (user == string.Empty)
                return new ApiResponse(new ApiError("User can't be empty", SQNErrorCode.NullValue));
            if (serviceDTO == null)
                return new ApiResponse(new ApiError("A null objet can be added for Service", SQNErrorCode.NullValue));
            Service service = serviceDTO.ToModel();
            ApiError validated = service.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            StatusService ss = new();
            ApiResponse valido = await ss.IsForCreation(service.Status);
            if (!valido.Success)
                return valido;
            service.Creator = user;
            service.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertService(service);
                return new ApiResponse(service.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(ServiceDTO serviceDTO, string id, string user)
        {
            Console.WriteLine($"ServiceService: Upsate: Service DTO, id: {id}");
            if (user == string.Empty)
                return new ApiResponse(new ApiError("User can't be empty", SQNErrorCode.NullValue));
            if (serviceDTO == null)
                return new ApiResponse(new ApiError($"A null objet can't be used for update the Service {id}",
                    SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            if (serviceDTO.id != string.Empty && !serviceDTO.id.Equals(id))
                return new ApiResponse(new ApiError($"Trying to update an object diferent from {id}",
                    SQNErrorCode.NotMatchingValues));
            Service service = serviceDTO.ToModel();
            service = Updater(service, id, user);
            validated = service.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await ValidationForUpdate(service, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            try
            {
                await _database.UpdateService(service);
                return new ApiResponse(service.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> AddClassifications(List<DataElementDTO> classifications, string id, string user)
        {
            Console.WriteLine($"ServiceService: AddClassifications: List<DataElementDTO>, id: {id}");
            if (user == string.Empty)
                return new ApiResponse(new ApiError("User can't be empty", SQNErrorCode.NullValue));
            if (classifications.Count <= 0)
                return new ApiResponse(new ApiError("Classifications can't be empty", SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            IClassificationService cs = new ClassificationService();
            ApiResponse response = await cs.validateClassifications(classifications);
            if (!response.Success)
                return response;
            Service service = new();
            service.Classifications = classifications;
            service = Updater(service, id, user);
            try
            {
                await _database.UpdateService(service);
                return new ApiResponse(service.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> AddCalification(CalificationDTO calification, string id, string user)
        {
            Console.WriteLine($"ServiceService: AddCalification: CalificationDTO, id: {id}");
            if (user == string.Empty)
                return new ApiResponse(new ApiError("User can't be empty", SQNErrorCode.NullValue));
            ApiError validated = calification.ValidateDTO();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            Service service = new();
            service = Updater(service, id, user);
            service.Calification = calification;
            try
            {
                await _database.UpdateService(service);
                return new ApiResponse(service.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> AddExpert(string expert, string user, string id)
        {
            Console.WriteLine($"ServiceService: AddExpert: expert {expert}, id: {id}");
            if (user == string.Empty)
                return new ApiResponse(new ApiError("User can't be empty", SQNErrorCode.NullValue));
            if (expert == string.Empty)
                return new ApiResponse(new ApiError("Expert can't be empty", SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            Service service = new();
            service = Updater(service, id, user);
            service.ExpertId = expert;
            try
            {
                await _database.UpdateService(service);
                return new ApiResponse(service.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> ChangeStatus(string status, string id, string user)
        {
            Console.WriteLine($"ServiceService: ChangeStatus: status {status}, id: {id}");
            if (user == string.Empty)
                return new ApiResponse(new ApiError("User can't be empty", SQNErrorCode.NullValue));
            ApiError validated = GeneralValidatons.ValidateObjectId(id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            Service service = new();
            service = Updater(service, id, user);
            service.Status = status;
            try
            {
                await _database.UpdateService(service);
                return new ApiResponse(service.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"ServiceService: Delete: id: {id}");
            try
            {
                await _database.DeleteService(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        private async Task<ApiError> ValidationForUpdate(Service service, string id)
        {
            Console.WriteLine($"ServiceService: ValidationForUpdate: Service, id: {id}");
            try
            {
                List<Service> valida = await _database.GetServiceByCustomerAndStatus(service.CustomerId, service.Status);
                StatusService ss = new();
                ApiResponse val = await ss.IsForCreation(service.Status);
                if (valida.Count > 0)
                    if (valida[0].id.ToString() != id && val.Result)
                        return new ApiError("The customer " + service.CustomerId + " has a service with status " + service.Status + " already exist",
                            SQNErrorCode.ServiceAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        private static List<ServiceDTO> ToListDTO(List<Service> services)
        {
            Console.WriteLine($"ServiceService: ToLestDTO");
            List<ServiceDTO> response = new();
            foreach (Service s in services)
                response.Add(s.ToDTO());
            return response;
        }

        private static Service Updater(Service service, string id, string user)
        {
            Console.WriteLine($"ServiceService: Updater: Service, id: {id}");
            service.id = new ObjectId(id);
            service.Updater = user;
            service.UpdateDate = DateTime.Now;
            return service;
        }
    }
}
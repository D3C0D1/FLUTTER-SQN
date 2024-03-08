using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerCollection _database = new CustomerCollection();

        public async Task<ApiResponse> GetAll()
        {
            Console.WriteLine("CustomerService: GetAll");
            try
            {
                List<Customer> customers = await _database.GetAllCustomers();
                if (customers.Count > 0)
                    return new ApiResponse(ToListDTO(customers));
                return new ApiResponse(new ApiError("No Customers to show", SQNErrorCode.CustomerNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"CustomerService: GetById: id {id}");
            try
            {
                Customer customer = await _database.GetCustomerById(id);
                if (customer != null)
                    return new ApiResponse(customer.ToDTO());
                return new ApiResponse(new ApiError($"The Customer {id} doesn't exist",
                    SQNErrorCode.CustomerNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByCellPhone(string cellPhone)
        {
            Console.WriteLine($"CustomerService: GetByCellPhone: cellPhone {cellPhone}");
            try
            {
                Customer customer = await _database.GetCustomerByCellPhone(cellPhone);
                if (customer != null)
                    return new ApiResponse(customer.ToDTO());
                return new ApiResponse(new ApiError($"The customer whith cellular {cellPhone} not found",
                    SQNErrorCode.CustomerNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByEmail(string email)
        {
            Console.WriteLine($"CustomerService: GetByEmail: email {email}");
            try
            {
                Customer customer = await _database.GetCustomerByEmail(email);
                if (customer != null)
                    return new ApiResponse(customer.ToDTO());
                return new ApiResponse(new ApiError($"The customer whith email {email} not found",
                    SQNErrorCode.CustomerNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(CustomerDTO customerDTO, string user)
        {
            Console.WriteLine("CustomerService: Insert");
            if (customerDTO == null)
                return new ApiResponse(new ApiError("A null objet can be added for Customer",
                    SQNErrorCode.NullValue));
            Customer customer = customerDTO.ToModel();
            ApiError validated = customer.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await CustomerEmailValidation(customer.Email, customer.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await CustomerCellPhoneValidation(customer.CellPhone, customer.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            customer.Creator = user;
            customer.CreationDate = DateTime.Now;
            try
            {
                await _database.InsertCustomer(customer);
                return new ApiResponse(customer.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(CustomerDTO customerDTO, string id, string user)
        {
            Console.WriteLine($"CustomerService: Update: id {id}");
            if (customerDTO == null)
                return new ApiResponse(new ApiError($"A null objet can be used for update the Customer {id}", SQNErrorCode.NullValue));
            Customer customer = customerDTO.ToModel();
            ApiError validated = customer.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await CustomerEmailValidation(customer.Email, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await CustomerCellPhoneValidation(customer.CellPhone, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            customer.id = new ObjectId(id);
            customer.Updater = user;
            customer.UpdateDate = DateTime.Now;
            try
            {
                await _database.UpdateCustomer(customer);
                return new ApiResponse(customer.ToDTO());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"CustomerService: Delete: id {id}");
            try
            {
                await _database.DeleteCustomer(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        private async Task<ApiError> CustomerEmailValidation(string email, string id)
        {
            Console.WriteLine($"CustomerService: CustomerEmailValidation: id {id}");
            try
            {
                Customer customer = await _database.GetCustomerByEmail(email);
                if (customer != null)
                    if (customer.id.ToString() != id)
                        return new ApiError($"The email {email} is used", SQNErrorCode.EmailAlreadyExist);
                return new ApiError($"The email {email} isn't used", SQNErrorCode.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        private async Task<ApiError> CustomerCellPhoneValidation(string cellPhone, string id)
        {
            Console.WriteLine($"CustomerService: CustomerCellPhoneValidation: id {id}");
            try
            {
                Customer customer = await _database.GetCustomerByCellPhone(cellPhone);
                if (customer != null)
                    if (customer.id.ToString() != id)
                        return new ApiError($"The Customer with the cellular {cellPhone} already exist",
                            SQNErrorCode.CellPhoneAlreadyExisit);
                return new ApiError($"The cellular {cellPhone} isn't used", SQNErrorCode.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        private static List<CustomerDTO> ToListDTO(List<Customer> customer)
        {
            Console.WriteLine($"CustomerService: ToListDTO");
            List<CustomerDTO> response = new();
            foreach (Customer c in customer)
                response.Add(c.ToDTO());
            return response;
        }
    }
}
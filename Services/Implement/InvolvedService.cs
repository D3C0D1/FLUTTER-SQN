using MongoDB.Bson;
using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class InvolvedService: IInvolvedService
    {
        private readonly IInvolvedCollection _database = new InvolvedCollection();

        public async Task<ApiResponse> GetAllByReport(string report)
        {
            Console.WriteLine("InvolvedService: GetAllByReport");
            try
            {
                List<Involved> formsInvolved = await _database.GetAllInvolvedByReport(report);
                if (formsInvolved.Count > 0)
                    return new ApiResponse(ToListDTO(formsInvolved));
                return new ApiResponse(new ApiError("Don't exist Forms to list", SQNErrorCode.NoResult));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetAllByPlateID(string plateNumber)
        {
            Console.WriteLine($"InvolvedService: GetAllByPlate: plateNumber: {plateNumber}");
            try
            {
                List<Involved> formsInvolved = await _database.GetAllInvolvedByPlateNumber(plateNumber);
                if (formsInvolved.Count > 0)
                    return new ApiResponse(ToListDTO(formsInvolved));
                return new ApiResponse(new ApiError($"Didn't find the Plate {plateNumber} in the system", SQNErrorCode.NoResult));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"InvolvedService: GetById: id: {id}");
            try
            {
                Involved formInvolved = await _database.GetInvolvedById(id);
                if (formInvolved != null)
                    return new ApiResponse(formInvolved.ToDTO());
                return new ApiResponse(new ApiError($"The Involved {id} wasn't found", 
                    SQNErrorCode.NoResult));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Insert(InvolvedDTO involvedDTO, string PlateNumber, int IdenficationNumber)
        {
            Console.WriteLine("InvolvedService: Insert: InvolvedDTO");
            if (involvedDTO == null)
                return new ApiResponse(new ApiError("A null objet can be added for Formulary", SQNErrorCode.NullValue));
            Involved formInvolved = involvedDTO.ToModel();
            ApiError validated = formInvolved.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await InvolvedIdValidation(involvedDTO.id, involvedDTO.Report);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await InvolvedReportValidation(formInvolved.Report, formInvolved.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await InvolvedPlateNumberValidation(formInvolved.PlateID, formInvolved.id.ToString());
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            formInvolved.IdentificationID = IdenficationNumber;
            formInvolved.PlateID = PlateNumber;
            try
            {
                await _database.InsertInvolved(formInvolved);
                return new ApiResponse(formInvolved);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Update(InvolvedDTO involvedDTO, string id, string PlateNumber, int IdentificationNumber, string report)
        {
            Console.WriteLine("InvolvedService: Update: InvolvedDTO");
            if (involvedDTO == null)
                return new ApiResponse(new ApiError("A null objet can´t be used for update the Formulary ", SQNErrorCode.NullValue));
            Involved formInvolved = involvedDTO.ToModel();
            ApiError validated = formInvolved.ValidateModel();
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await InvolvedIdValidation(id, report);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await InvolvedReportValidation(formInvolved.Report, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            validated = await InvolvedPlateNumberValidation(formInvolved.PlateID, id);
            if (validated.Code != SQNErrorCode.None)
                return new ApiResponse(validated);
            formInvolved.id = new ObjectId(id);
            formInvolved.PlateID = PlateNumber;
            formInvolved.IdentificationID = IdentificationNumber;
            try
            {
                await _database.UpdateInvolved(formInvolved);
                return new ApiResponse(formInvolved);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"InvolvedService: Delete: id: {id}");
            try
            {
                await _database.DeleteInvolved(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiError> InvolvedIdValidation(string id, string report)
        {
            Console.WriteLine($"InvolvedService: InvolvedValidation: id: {id}, report: {report}");
            try
            {
                Involved formInvolved = await _database.GetInvolvedById(id);
                if (formInvolved != null)
                    if (formInvolved.Report == report)
                        return new ApiError($"The involved {id} already exist into the report {report}",
                            SQNErrorCode.InvolvedAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        //Revisar este metodo con SPineda
        public async Task<ApiError> InvolvedReportValidation(string report, string id)
        {
            Console.WriteLine($"InvolvedService: InvolvedReportValidation: report: {report}, id: {id}");
            try
            {
                List<Involved> formInvolved = await _database.GetAllInvolvedByReport(report);
                if (formInvolved != null)
                    if (formInvolved.ToString() != id)
                        return new ApiError($"The Report {report}", SQNErrorCode.EmailAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        public async Task<ApiError> InvolvedPlateNumberValidation(string plateNumber, string id)
        {
            Console.WriteLine($"InvolvedService: InvolvedPlateNumberValidation: plateNumber: {plateNumber}, id: {id}");
            try
            {
                List<Involved> formInvolved = await _database.GetAllInvolvedByPlateNumber(plateNumber);
                if (formInvolved != null)
                    if (formInvolved.ToString() != id)
                        return new ApiError("", SQNErrorCode.EmailAlreadyExist);
                return new ApiError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiError(ex);
            }
        }

        private static List<InvolvedDTO> ToListDTO(List<Involved> formsInvolved)
        {
            Console.WriteLine("InvolvedService: ToListDTO");
            List<InvolvedDTO> response = new();
            foreach (Involved e in formsInvolved)
                response.Add(e.ToDTO());
            return response;
        }
    }
}

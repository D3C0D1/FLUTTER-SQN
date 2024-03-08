using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SQNBack.Models.DTO;
using SQNBack.Utils;
using System.ComponentModel.DataAnnotations;

namespace SQNBack.Models
{
    public class Involved
    {
        // MongoDB identifier attribute
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }

        // Propiedades para el vehículo

        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        [Required]
        public string PlateID { get; set; }

        // Propiedad adicional para el vehículo
        public string LineVehicle { get; set; }
        public string BodyType { get; set; }
        public int Tonnage { get; set; }
        public string RegisteredIn { get; set; }

        // Propiedades para la licencia y registro del vehículo
        public string TransitLicense { get; set; }
        public string Company { get; set; }
        public int NIT { get; set; }
        public int RegistrationCard { get; set; }

        // Propiedades para pólizas de seguro
        public int SOATReg { get; set; }
        public string SOATCompany { get; set; }
        public int RCCReg { get; set; }
        public string RCCCompany { get; set; }
        public int RCEReg { get; set; }
        public string RCECompany { get; set; }

        // Fechas de vencimiento
        public DateTime ExpirationSOAT { get; set; }
        public DateTime ExpirationRCC { get; set; }
        public DateTime ExpirationRCE { get; set; }

        // Propiedades para inspecciones y revisiones
        public int TechMechReview { get; set; }
        public DateTime ExpirationRTM { get; set; }

        public string ImmobilizedIn { get; set; }
        public string AvailableTo { get; set; }
        public int Occupants { get; set; }

        // Propiedades para la persona implicada
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocType { get; set; }
        [Required]
        public int IdentificationID { get; set; }
        public string Nacionality { get; set; }
        public int ContactDetails { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime Birthdate { get; set; }
        public string Genere { get; set; }
        public string Gravity { get; set; }
        public string CellPhone { get; set; }
        public string AlcoholLevel { get; set; }
        public string Report { get; set; }
        public List<string> OtherFiles { get; set; }

        public ApiError ValidateModel()
        {
            Console.WriteLine("ValidationModel");
            // Validation checks for each property
            // Returning an error if any validation fails

            if (string.IsNullOrWhiteSpace(this.PlateID))
                return new ApiError("PlateNumber in FormInvolved can't empty", SQNErrorCode.ValueMustBeUpper);
            if (string.IsNullOrWhiteSpace(this.FirstName))
                return new ApiError("FirstName in FormInvolved can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.LastName))
                return new ApiError("LastName in FormInvolved can't be empty", SQNErrorCode.MissingLastname);
            if (this.IdentificationID < 0)
                return new ApiError("IdentificationNumber in FormInvolved must be greater than or equal to 0", SQNErrorCode.MissingIdNumber);
            if (this.ContactDetails < 0)
                return new ApiError("ContactDetails in FormInvolved Must be greater than or equal to 0", SQNErrorCode.MissingName);
            if (this.Tonnage <= 0)
                return new ApiError("Tonnage in FormInvolved el peso debe ser mayor a 0", SQNErrorCode.MissingTonnage);
            if (string.IsNullOrWhiteSpace(this.CellPhone))
                return new ApiError("FormInvolved's cellular phone number can't be empty", SQNErrorCode.MissingPhoneNumber);
            if (string.IsNullOrWhiteSpace(this.Address))
                return new ApiError("Address in FormInvolved can't be empty", SQNErrorCode.MissingAddress);
            if (this.TechMechReview < 0)
                return new ApiError("TechnicalMechanicalReview in FormInvolved Please enter your TechnicalMechanicalReview in full.", SQNErrorCode.MissingTechMechReview);
            if (this.NIT < 0)
                return new ApiError("NIT in FormInvolved Please enter your NIT in full.", SQNErrorCode.MissingNIT);
            if (this.AlcoholLevel == null)
                return new ApiError("Grade in FormInvolved Please enter Grade.", SQNErrorCode.MissingAlcoholLevel);
            if (this.SOATReg < 0)
                return new ApiError("SOATPolicy in FormInvolved Please enter your SOAT policy in full.", SQNErrorCode.MissingSoat);
            if (this.RCCReg < 0)
                return new ApiError("RCCPolicy in FormInvolved Please enter your RCC policy in full.", SQNErrorCode.MissingRCC);
            if (this.RCEReg < 0)
                return new ApiError("RCEPolicy in FormInvolved Please enter your RCE policy in full.", SQNErrorCode.MissingRCE);
            if (string.IsNullOrWhiteSpace(this.SOATCompany))
                return new ApiError("InsuranceCompanySOAT in FormInvolved can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.RCCCompany))
                return new ApiError("InsuranceCompanyRCC in FormInvolved can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.RCECompany))
                return new ApiError("InsuranceCompanyRCE in FormInvolved can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.Company))
                return new ApiError("Company in FormInvolved can't be empty", SQNErrorCode.MissingName);
            if (string.IsNullOrWhiteSpace(this.Report))
                return new ApiError("Report in FormInvolved can't be empty", SQNErrorCode.MissingReport);


            // If all validations pass, return a success status
            return new ApiError();
        }

        // Data transfer object (DTO) method to convert to a simplified representation
        public InvolvedDTO ToDTO()
        {
            return new InvolvedDTO
            {
                id = this.id.ToString(),
                Brand = this.Brand,
                Model = this.Model,
                Color = this.Color,
                PlateID = this.PlateID,
                FirstName = this.FirstName,
                LastName = this.LastName,
                IdentificationNumber = this.IdentificationID,
                ContactDetails = this.ContactDetails,
                Tonnage = this.Tonnage,
                BodyType = this.BodyType,
                RegisteredIn = this.RegisteredIn,
                TransitLicense = this.TransitLicense,
                Company = this.Company,
                NIT = this.NIT,
                RegistrationCard = this.RegistrationCard,
                SOATReg = this.SOATReg,
                SOATCompany = this.SOATCompany,
                RCCReg = this.RCCReg,
                RCCCompany = this.RCCCompany,
                RCEReg = this.RCEReg,
                RCECompany = this.RCECompany,
                ExpirationSOAT = this.ExpirationSOAT,
                ExpirationRCC = this.ExpirationRCC,
                ExpirationRCE = this.ExpirationRCE,
                TechMechReview = this.TechMechReview,
                ExpirationTMR = this.ExpirationRTM,
                ImmobilizedIn = this.ImmobilizedIn,
                AvailableTo = this.AvailableTo,
                Occupants = this.Occupants,
                Address = this.Address,
                City = this.City,
                Birthdate = this.Birthdate,
                Genere = this.Genere,
                Gravity = this.Gravity,
                CellPhone = this.CellPhone,
                AlcoholLevel = this.AlcoholLevel,
                Report = this.Report,
                OtherFiles = this.OtherFiles


            };
        }


    }
}

using MongoDB.Bson;


namespace SQNBack.Models.DTO
{
    public class InvolvedDTO
    {

        public string id { get; set; }

        // Propiedades para el vehículo
        // Propiedades para el vehículo
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
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
        public DateTime ExpirationTMR { get; set; }

        public string ImmobilizedIn { get; set; }
        public string AvailableTo { get; set; }
        public int Occupants { get; set; }

        // Propiedades para la persona implicada
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocType { get; set; }

        public int IdentificationNumber { get; set; }
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

        // Método para convertir a un objeto 
        public Involved ToModel()
        {
            Involved response = new();
            if (!string.IsNullOrWhiteSpace(this.id))
                response.id = new ObjectId(this.id);
            response.Brand = this.Brand;
            response.Model = this.Model;
            response.Color = this.Color;
            response.PlateID = this.PlateID;
            response.FirstName = this.FirstName;
            response.LastName = this.LastName;
            response.IdentificationID = this.IdentificationNumber;
            response.ContactDetails = this.ContactDetails;
            response.BodyType = this.BodyType;
            response.Tonnage = this.Tonnage;
            response.RegisteredIn = this.RegisteredIn;
            response.TransitLicense = this.TransitLicense;
            response.Company = this.Company;
            response.NIT = this.NIT;
            response.RegistrationCard = this.RegistrationCard;
            response.SOATReg = this.SOATReg;
            response.SOATCompany = this.SOATCompany;
            response.RCCReg = this.RCCReg;
            response.RCCCompany = this.RCCCompany;
            response.RCEReg = this.RCEReg;
            response.RCECompany = this.RCECompany;
            response.ExpirationSOAT = this.ExpirationSOAT;
            response.ExpirationRCC = this.ExpirationRCC;
            response.ExpirationRCE = this.ExpirationRCE;
            response.TechMechReview = this.TechMechReview;
            response.ExpirationRTM = this.ExpirationTMR;
            response.ImmobilizedIn = this.ImmobilizedIn;
            response.AvailableTo = this.AvailableTo;
            response.Occupants = this.Occupants;
            response.Address = this.Address;
            response.City = this.City;
            response.Birthdate = this.Birthdate;
            response.Genere = this.Genere;
            response.Gravity = this.Gravity;
            response.CellPhone = this.CellPhone;
            response.AlcoholLevel = this.AlcoholLevel;
            response.Report = this.Report;
            response.OtherFiles = this.OtherFiles;


            return response;
        }
    }
}

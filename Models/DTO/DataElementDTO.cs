using MongoDB.Bson;

namespace SQNBack.Models.DTO
{
    public class DataElementDTO
    {
        //Data Element's Id
        public string Id { get; set; }

        //Data Element's Name
        public string Name { get; set; }

        //Data Element's Status
        public string Status { get; set; }

        //Data Element's icon and this can be null
        public string Icon { get; set; }

        //Data element's type  
        //AC = Accident          //AR = Area             //AI = Atificial Ilumination     //CR = Crash
        //CL = Climate           //CN = Conditions       //CT = Controls                  //DS = Dessign
        //DT = Document Type     //FO = Fixed Object     //GM = Geometric                 //GR = Classifications
        //LN = Lanes             //LC = Location         //RS = Running surface           //SC = Sector
        //SA = State             //SW = Sidewalk         //ST = Service Type              //TM = Transport mode
        //UT = Utilization       //VC = Vehicle class    //VS = Visibility                //ZN = Zone
        public string Group { get; set; }

        public DataElement ToModel()
        {
            DataElement response = new();
            if (!string.IsNullOrEmpty(this.Id))
                response.Id = new ObjectId(this.Id);
            response.Name = this.Name;
            response.Status = this.Status;
            response.Group = this.Group;
            response.Icon = this.Icon;
            return response;
        }
    }
}

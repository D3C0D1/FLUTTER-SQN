namespace SQNBack.Models.DTO
{
    public class MediaDTO
    {
        public string id { get; set; }
        public string Vehicle { get; set; }
        public string Report { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public UbicationDTO Location { get; set; }
        public string Comments { get; set; }

        public Media ToModel()
        {
            Media response = new();
            //   if (this.Id != string.Empty)
            //       response.Id = new ObjectId(this.Id);
            response.FileName = this.FileName;
            response.FileType = this.FileType;
            response.FilePath = this.FilePath;
            response.FileSize = this.FileSize;
            response.Location = this.Location;
            response.Comments = this.Comments;
            return response;
        }
    }
}

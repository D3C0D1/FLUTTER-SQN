using SQNBack.Models;
using SQNBack.Models.DTO;
using SQNBack.Repositories.Collections;
using SQNBack.Repositories.Collections.Implement;
using SQNBack.Utils;

namespace SQNBack.Services.Implement
{
    public class MediaService: IMediaService
    {
        private readonly IMediaCollection _database = new MediaCollection();

        public async Task<ApiResponse> GetById(string id)
        {
            Console.WriteLine($"MediaService: GetById: id: {id}");
            try
            {
                Media media = await _database.GetMediaById(id);
                if (media != null)
                    return new ApiResponse(media.ToDTO());
                return new ApiResponse(new ApiError($"The Media {id} doesn't exist",
                    SQNErrorCode.MediaNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        public async Task<ApiResponse> GetByVehicle(string vehicle)
        {
            Console.WriteLine($"MediaService: GetByVehicle: vehicle: {vehicle}");
            try
            {
                List<Media> medias = await _database.GetMediaByVehicle(vehicle);
                if (medias.Count > 0)
                    return new ApiResponse(ToListDTO(medias));
                return new ApiResponse(new ApiError("No Media Files to show", SQNErrorCode.MediaNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }
        public async Task<ApiResponse> GetByReport(string report)
        {
            Console.WriteLine($"MediaService: GetByReport: id: {report}");
            try
            {
                List<Media> medias = await _database.GetMediaByReport();
                if (medias.Count > 0)
                    return new ApiResponse(ToListDTO(medias));
                return new ApiResponse(new ApiError("No Media files to show", SQNErrorCode.MediaNotFound));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }
        public async Task<ApiResponse> Insert(MediaDTO mediaDTO, byte[] fileBytes)
        {
            Console.WriteLine("MediaService: Insert");
            var mediaup = new Media
            {
                FileName = mediaDTO.FileName,
                FileType = mediaDTO.FileType,
                FilePath = $"/Media/Reports/{mediaDTO.Report}/",
                FileSize = fileBytes.Length,
                UploadDateTime = DateTime.Now,
                Location = mediaDTO.Location,
                Comments = mediaDTO.Comments
            };
            try
            {
                await _database.InsertOne(mediaup);
                return new ApiResponse(mediaup);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }
        
        public async Task<ApiResponse> Delete(string id)
        {
            Console.WriteLine($"MediaService: Delete: id: {id}");
            try
            {
                await _database.DeleteMedia(id);
                return new ApiResponse(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new ApiResponse(ex);
            }
        }

        private static List<MediaDTO> ToListDTO(List<Media> medias)
        {
            Console.WriteLine("MediaService: ToListDto");
            List<MediaDTO> response = new();
            foreach (Media m in medias)
                response.Add(m.ToDTO());
            return response;
        }
    }

}

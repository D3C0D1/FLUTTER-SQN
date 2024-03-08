using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IErrorMessageCollection
    {
        Task<ErrorMessage> GetErrorMessageById(string id);
        Task<ErrorMessage> GetErrorMessageByCode(string code);
        Task<ErrorMessage> GetErrorMessageByValue(int value);
        Task<List<ErrorMessage>> GetAllErrorMessage();
        Task InsertErrorMessage(ErrorMessage errorMessage);
        Task UpdateErrorMessage(ErrorMessage errorMessage);
        Task DeleteErrorMessage(string id);
    }
}
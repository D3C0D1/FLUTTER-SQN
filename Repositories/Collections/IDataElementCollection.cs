using SQNBack.Models;

namespace SQNBack.Repositories.Collections
{
    public interface IDataElementCollection
    {
        Task<DataElement> GetDocumentElementById(string id);
        Task<List<DataElement>> GetAllDocumentElements();
        Task<List<DataElement>> GetDocumentElementsByGroup(string group);
        Task<DataElement> GetDocumentElementsByNameAndGroup(string name, string group);
        Task InsertDocumentElement(DataElement dataElement);
        Task UpdateDocumentElement(DataElement dataElement);
        Task DeleteDocumentElement(string id);
    }
}
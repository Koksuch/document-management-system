using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Payloads;
using server.Dtos.Responses;

namespace server.Interfaces
{
    public interface IDocumentItemRepository
    {
        Task<IEnumerable<DocumentItemResponse>> GetAllDocumentItemsAsync();
        Task<IEnumerable<DocumentItemResponse>?> GetAllDocumentItemsByDocumentIdAsync(int documentId);
        Task<DocumentItemResponse?> GetItemByIdAsync(int id);
        Task<DocumentItemResponse?> AddDocumentItemAsync(int documentId, DocumentItemPayload item);
        Task<DocumentItemResponse?> UpdateDocumentItemAsync(int id, DocumentItemPayload item);
        Task<MessageResponse?> DeleteDocumentItemAsync(int id);
    }
}
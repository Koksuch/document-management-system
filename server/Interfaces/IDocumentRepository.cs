using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Payloads;
using server.Dtos.Responses;

namespace server.Interfaces
{
    public interface IDocumentRepository
    {
        Task<PagedResponse<DocumentResponse>> GetAllDocumentsAsync(int? page, int? pageSize, string sortBy, bool descending, string? search, DateTime? dateFrom, DateTime? dateTo);
        Task<DocumentResponse?> GetDocumentByIdAsync(int id);
        Task<DocumentResponse?> AddDocumentAsync(DocumentPayload document);
        Task<DocumentResponse?> UpdateDocumentAsync(int id, DocumentPayload document);
        Task<MessageResponse?> DeleteDocumentAsync(int id);
    }
}
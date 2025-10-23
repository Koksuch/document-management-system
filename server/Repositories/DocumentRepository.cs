using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Payloads;
using server.Dtos.Responses;
using server.Interfaces;
using server.Models;

namespace server.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DocumentResponse?> AddDocumentAsync(DocumentPayload document)
        {

            var newDocument = new Document
            {
                Type = document.Type,
                Date = document.Date,
                FirstName = document.FirstName,
                LastName = document.LastName,
                City = document.City
            };

            await _dbContext.Documents.AddAsync(newDocument);
            await _dbContext.SaveChangesAsync();

            return new DocumentResponse
            (
                newDocument.Id,
                newDocument.Type,
                newDocument.Date,
                newDocument.FirstName,
                newDocument.LastName,
                newDocument.City
            );
        }

        public async Task<MessageResponse?> DeleteDocumentAsync(int id)
        {
            var document = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Id == id);
            if (document == null) return null;

            _dbContext.Documents.Remove(document);
            await _dbContext.SaveChangesAsync();

            return new MessageResponse("Document deleted successfully.");
        }

        public async Task<PagedResponse<DocumentResponse>> GetAllDocumentsAsync(
            int? page,
            int? pageSize,
            string sortBy,
            bool descending,
            string? search,
            DateTime? dateFrom,
            DateTime? dateTo
        )
        {
            var query = _dbContext.Documents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(d =>
                    d.Type.ToLower().Contains(search) ||
                    d.FirstName.ToLower().Contains(search) ||
                    d.LastName.ToLower().Contains(search) ||
                    d.City.ToLower().Contains(search)
                );
            }

            if (dateFrom.HasValue)
                query = query.Where(d => d.Date >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(d => d.Date <= dateTo.Value);

            sortBy = sortBy.ToLower();
            query = sortBy switch
            {
                "id" => descending ? query.OrderByDescending(d => d.Id) : query.OrderBy(d => d.Id),
                "type" => descending ? query.OrderByDescending(d => d.Type) : query.OrderBy(d => d.Type),
                "date" => descending ? query.OrderByDescending(d => d.Date) : query.OrderBy(d => d.Date),
                "firstname" => descending ? query.OrderByDescending(d => d.FirstName) : query.OrderBy(d => d.FirstName),
                "lastname" => descending ? query.OrderByDescending(d => d.LastName) : query.OrderBy(d => d.LastName),
                "city" => descending ? query.OrderByDescending(d => d.City) : query.OrderBy(d => d.City),
                _ => descending ? query.OrderByDescending(d => d.Id) : query.OrderBy(d => d.Id),
            };

            var totalCount = await query.CountAsync();
            var documents = await query
                .Skip(((page ?? 1) - 1) * (pageSize ?? totalCount))
                .Take(pageSize ?? totalCount)
                .ToListAsync();

            return new PagedResponse<DocumentResponse>
            (
                documents.Select(d => new DocumentResponse
                (
                    d.Id,
                    d.Type,
                    d.Date,
                    d.FirstName,
                    d.LastName,
                    d.City
                )),
                totalCount,
                page ?? 1,
                pageSize ?? totalCount,
                (int)Math.Ceiling((double)totalCount / (pageSize ?? totalCount))
            );
        }

        public async Task<DocumentResponse?> GetDocumentByIdAsync(int id)
        {
            var document = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Id == id);

            if (document == null) return null;

            return new DocumentResponse
            (
                document.Id,
                document.Type,
                document.Date,
                document.FirstName,
                document.LastName,
                document.City
            );
        }

        public async Task<DocumentResponse?> UpdateDocumentAsync(int id, DocumentPayload document)
        {
            var existingDocument = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Id == id);
            if (existingDocument == null) return null;

            existingDocument.Type = document.Type;
            existingDocument.Date = document.Date;
            existingDocument.FirstName = document.FirstName;
            existingDocument.LastName = document.LastName;
            existingDocument.City = document.City;

            _dbContext.Documents.Update(existingDocument);
            await _dbContext.SaveChangesAsync();

            return new DocumentResponse
            (
                existingDocument.Id,
                existingDocument.Type,
                existingDocument.Date,
                existingDocument.FirstName,
                existingDocument.LastName,
                existingDocument.City
            );
        }
    }
}
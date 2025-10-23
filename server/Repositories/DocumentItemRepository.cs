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
    public class DocumentItemRepository : IDocumentItemRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DocumentItemResponse?> AddDocumentItemAsync(int documentId, DocumentItemPayload item)
        {
            var document = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Id == documentId);
            if (document == null) return null;

            var newItem = new DocumentItem
            {
                DocumentId = documentId,
                Ordinal = item.Ordinal,
                Product = item.Product,
                Quantity = item.Quantity,
                Price = item.Price,
                TaxRate = item.TaxRate
            };

            await _dbContext.DocumentItems.AddAsync(newItem);
            await _dbContext.SaveChangesAsync();

            return new DocumentItemResponse
            (
                newItem.Id,
                newItem.DocumentId,
                newItem.Ordinal,
                newItem.Product,
                newItem.Quantity,
                newItem.Price,
                newItem.TaxRate
            );
        }

        public async Task<MessageResponse?> DeleteDocumentItemAsync(int id)
        {
            var item = await _dbContext.DocumentItems.FirstOrDefaultAsync(di => di.Id == id);
            if (item == null) return null;

            _dbContext.DocumentItems.Remove(item);
            await _dbContext.SaveChangesAsync();

            return new MessageResponse("Document item deleted successfully.");
        }

        public Task<IEnumerable<DocumentItemResponse>> GetAllDocumentItemsAsync()
        {
            var items = _dbContext.DocumentItems.ToListAsync();

            return items.ContinueWith(t => t.Result.Select(item => new DocumentItemResponse
            (
                item.Id,
                item.DocumentId,
                item.Ordinal,
                item.Product,
                item.Quantity,
                item.Price,
                item.TaxRate
            )));
        }

        public async Task<DocumentItemResponse?> GetItemByIdAsync(int id)
        {
            var item = await _dbContext.DocumentItems.FirstOrDefaultAsync(di => di.Id == id);
            if (item == null) return null;

            return new DocumentItemResponse
            (
                item.Id,
                item.DocumentId,
                item.Ordinal,
                item.Product,
                item.Quantity,
                item.Price,
                item.TaxRate
            );
        }

        public async Task<IEnumerable<DocumentItemResponse>?> GetAllDocumentItemsByDocumentIdAsync(int documentId)
        {
            var document = await _dbContext.Documents.FirstOrDefaultAsync(d => d.Id == documentId);
            if (document == null) return null;

            var items = await _dbContext.DocumentItems
                .Where(di => di.DocumentId == documentId)
                .OrderBy(di => di.Ordinal)
                .ToListAsync();

            return items.Select(item => new DocumentItemResponse
            (
                item.Id,
                item.DocumentId,
                item.Ordinal,
                item.Product,
                item.Quantity,
                item.Price,
                item.TaxRate
            ));
        }

        public async Task<DocumentItemResponse?> UpdateDocumentItemAsync(int id, DocumentItemPayload item)
        {
            var existingItem = await _dbContext.DocumentItems.FirstOrDefaultAsync(di => di.Id == id);
            if (existingItem == null) return null;

            existingItem.Ordinal = item.Ordinal;
            existingItem.Product = item.Product;
            existingItem.Quantity = item.Quantity;
            existingItem.Price = item.Price;
            existingItem.TaxRate = item.TaxRate;

            await _dbContext.SaveChangesAsync();

            return new DocumentItemResponse
            (
                existingItem.Id,
                existingItem.DocumentId,
                existingItem.Ordinal,
                existingItem.Product,
                existingItem.Quantity,
                existingItem.Price,
                existingItem.TaxRate
            );
        }
    }
}
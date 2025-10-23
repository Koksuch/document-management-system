using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dtos.Responses;
using server.Interfaces;
using server.Models;

namespace server.Services
{
    public class ImportService : IImportService
    {
        private readonly AppDbContext _dbContext;
        public ImportService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MessageResponse> ImportCsvAsync(List<IFormFile> files)
        {
            if (await _dbContext.Documents.AnyAsync())
                return new MessageResponse("Database already contains data — import allowed only for empty database.");

            if (files == null || files.Count != 2)
                return new MessageResponse("You must upload exactly two CSV files.");

            var csvConfig = new CsvConfiguration(new CultureInfo("pl-PL"))
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                BadDataFound = null,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            List<Document>? documents = null;
            List<DocumentItem>? items = null;

            foreach (var file in files)
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csvHeaderReader = new CsvReader(reader, csvConfig);

                csvHeaderReader.Read();
                csvHeaderReader.ReadHeader();
                var headers = csvHeaderReader.HeaderRecord;

                if (headers == null || headers.Length == 0)
                    return new MessageResponse($"File {file.FileName} has no headers.");

                var headerSet = headers.Select(h => h.Trim().ToLower()).ToHashSet();

                bool isDocument = headerSet.SetEquals(["id", "type", "date", "firstname", "lastname", "city"]);
                bool isItem = headerSet.SetEquals(["documentid", "ordinal", "product", "quantity", "price", "taxrate"]);

                reader.BaseStream.Position = 0;
                reader.DiscardBufferedData();

                using var csv = new CsvReader(reader, csvConfig);

                if (isDocument)
                {
                    documents = csv.GetRecords<Document>().ToList();
                    Console.WriteLine($"Recognized {file.FileName} as Documents file.");
                }
                else if (isItem)
                {
                    items = csv.GetRecords<DocumentItem>().ToList();
                    Console.WriteLine($"Recognized {file.FileName} as DocumentItems file.");
                }
                else
                {
                    return new MessageResponse($"File {file.FileName} has an unknown CSV structure. Headers: {string.Join(", ", headers)}");
                }
            }

            if (documents == null || items == null)
                return new MessageResponse("Both 'documents' and 'documentItems' files are required and must have valid headers.");

            if (documents.Count == 0 || items.Count == 0)
                return new MessageResponse("CSV files must contain at least one record.");

            var allDocIds = documents.Select(d => d.Id).ToHashSet();
            var missingDocRefs = items.Select(i => i.DocumentId).Where(id => !allDocIds.Contains(id)).Distinct().ToList();

            if (missingDocRefs.Any())
                return new MessageResponse($"DocumentItems refer to missing Document IDs: {string.Join(", ", missingDocRefs)}");

            await _dbContext.Database.OpenConnectionAsync();
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [Documents] ON;");
                await _dbContext.Documents.AddRangeAsync(documents);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [Documents] OFF;");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }

            foreach (var item in items)
            {
                item.Document = null;
            }

            await _dbContext.DocumentItems.AddRangeAsync(items);
            await _dbContext.SaveChangesAsync();

            return new MessageResponse($"Successfully imported {documents.Count} documents and {items.Count} items.");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Dtos.Responses;

namespace server.Interfaces
{
    public interface IImportService
    {
        Task<MessageResponse> ImportCsvAsync(List<IFormFile> files);
    }
}
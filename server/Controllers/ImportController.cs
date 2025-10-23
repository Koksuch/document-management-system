using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Dtos.Responses;
using server.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api")]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<MessageResponse>> ImportCsv([FromForm] List<IFormFile> files)
        {
            var response = await _importService.ImportCsvAsync(files);

            if (response.Message.StartsWith("Successfully")) return Ok(response);

            return BadRequest(response);
        }
    }
}
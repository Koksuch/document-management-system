using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using server.Dtos.Payloads;
using server.Dtos.Responses;
using server.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentItemRepository _documentItemRepository;

        public DocumentController(IDocumentRepository documentRepository, IDocumentItemRepository documentItemRepository)
        {
            _documentRepository = documentRepository;
            _documentItemRepository = documentItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentResponse>>> GetAllDocuments(
            [FromQuery] int? page,
            [FromQuery] int? pageSize,
            [FromQuery] string sortBy = "id",
            [FromQuery] bool descending = false,
            [FromQuery] string? search = null,
            [FromQuery] DateTime? dateFrom = null,
            [FromQuery] DateTime? dateTo = null
        )
        {
            var documents = await _documentRepository.GetAllDocumentsAsync(page, pageSize, sortBy, descending, search, dateFrom, dateTo);
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentResponse>> GetDocumentById(int id)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(id);
            if (document == null) return NotFound(new MessageResponse("Document not found."));

            return Ok(document);
        }

        [HttpGet("{id}/documentitems")]
        public async Task<ActionResult<IEnumerable<DocumentItemResponse>>> GetDocumentItems(int id)
        {
            var items = await _documentItemRepository.GetAllDocumentItemsByDocumentIdAsync(id);
            if (items == null) return NotFound(new MessageResponse("Document not found."));

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentResponse>> CreateDocument([FromBody] DocumentPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdDocument = await _documentRepository.AddDocumentAsync(payload);
            return Ok(createdDocument);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentResponse>> UpdateDocument(int id, [FromBody] DocumentPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedDocument = await _documentRepository.UpdateDocumentAsync(id, payload);
            if (updatedDocument == null) return NotFound(new MessageResponse("Document not found."));

            return Ok(updatedDocument);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteDocument(int id)
        {
            var result = await _documentRepository.DeleteDocumentAsync(id);
            if (result == null) return NotFound(new MessageResponse("Document not found."));

            return Ok(result);
        }
    }
}
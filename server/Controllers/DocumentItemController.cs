using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Dtos.Payloads;
using server.Dtos.Responses;
using server.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("api/documentitems")]
    public class DocumentItemController : ControllerBase
    {
        private readonly IDocumentItemRepository _documentItemRepository;
        public DocumentItemController(IDocumentItemRepository documentItemRepository)
        {
            _documentItemRepository = documentItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentItemResponse>>> GetAllDocumentItems()
        {
            var items = await _documentItemRepository.GetAllDocumentItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentItemResponse>> GetDocumentItemById(int id)
        {
            var item = await _documentItemRepository.GetItemByIdAsync(id);
            if (item == null) return NotFound(new MessageResponse("Document item not found."));

            return Ok(item);
        }

        [HttpPost("{documentId}")]
        public async Task<ActionResult<DocumentItemResponse>> CreateDocumentItem(int documentId, [FromBody] DocumentItemPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdItem = await _documentItemRepository.AddDocumentItemAsync(documentId, payload);
            if (createdItem == null) return NotFound(new MessageResponse("Document not found."));

            return Ok(createdItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentItemResponse>> UpdateDocumentItem(int id, [FromBody] DocumentItemPayload payload)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedItem = await _documentItemRepository.UpdateDocumentItemAsync(id, payload);
            if (updatedItem == null) return NotFound(new MessageResponse("Document item not found."));

            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteDocumentItem(int id)
        {
            var result = await _documentItemRepository.DeleteDocumentItemAsync(id);
            if (result == null) return NotFound(new MessageResponse("Document item not found."));

            return Ok(result);
        }
    }
}
using AniBento.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AniBento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController(ICollectionService collectionService) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCollectionById(int id, CancellationToken ct)
        {
            var collection = await collectionService.GetCollectionByIdAsync(id, ct);
            if (collection is null)
                return NotFound();
            return Ok(collection);
        }
    }
}

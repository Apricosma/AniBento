using System.Net;
using AniBento.Api.Dtos.Collection;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniBento.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionsController(ICollectionService collectionService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<CollectionSummaryResponse>>> GetMine(
            CancellationToken ct
        )
        {
            var collections = await collectionService.GetMyCollectionsAsync(ct);
            return Ok(collections);
        }

        [HttpGet("{collectionId:int}")]
        public async Task<ActionResult> GetCollectionById(int collectionId, CancellationToken ct)
        {
            var collection = await collectionService.GetCollectionByIdAsync(collectionId, ct);
            if (collection is null)
                return NotFound();
            return Ok(collection);
        }

        [HttpPost]
        public async Task<ActionResult<CollectionResponse>> CreateCollection(
            [FromBody] CreateCollectionRequest request,
            CancellationToken ct
        )
        {
            var created = await collectionService.CreateAsync(request, ct);
            return CreatedAtAction(
                nameof(GetCollectionById),
                new { collectionID = created.Id },
                created
            );
        }

        [HttpPut("{collectionId:int}")]
        public async Task<ActionResult<CollectionSummaryResponse>> UpdateCollection(
            int collectionId,
            [FromBody] UpdateCollectionRequest request,
            CancellationToken ct
        )
        {
            var collection = await collectionService.UpdateCollectionAsync(
                collectionId,
                request,
                ct
            );
            if (collection is null)
                return NotFound();

            return Ok(collection);
        }

        [HttpDelete("{collectionId:int}")]
        public async Task<IActionResult> DeleteCollection(int collectionId, CancellationToken ct)
        {
            var ok = await collectionService.DeleteCollectionAsync(collectionId, ct);
            if (!ok)
                return NotFound();
            return NoContent();
        }

        [HttpPost("{collectionId:int}/items")]
        public async Task<IActionResult> AddItemToCollection(
            int collectionId,
            [FromBody] AddCollectionItemRequest request,
            CancellationToken ct
        )
        {
            var added = await collectionService.AddItemToCollectionAsync(collectionId, request, ct);

            return added ? NoContent() : Conflict("Item already exists in collection.");
        }

        [HttpDelete("{collectionId:int}/items/{collectionItemId}")]
        public async Task<IActionResult> DeleteItemFromCollection(
            int collectionId,
            int collectionItemId,
            CancellationToken ct
        )
        {
            await collectionService.RemoveMediaFromCollectionAsync(
                collectionId,
                collectionItemId,
                ct
            );
            return NoContent();
        }

        [HttpPut("{collectionId:int}/items/{collectionItemId:int}")]
        public async Task<IActionResult> UpdateCollectionItem(
            int collectionId,
            int collectionItemId,
            [FromBody] UpdateCollectionItemRequest request,
            CancellationToken ct
        )
        {
            var updated = await collectionService.CollectionItemUpdateAsync(
                collectionId,
                collectionItemId,
                request,
                ct
            );
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{collectionId:int}/pin")]
        public async Task<IActionResult> PinCollection(int collectionId, CancellationToken ct)
        {
            var ok = await collectionService.TogglePinnedAsync(collectionId, ct);
            if (!ok)
                return NotFound();
            return NoContent();
        }
    }
}

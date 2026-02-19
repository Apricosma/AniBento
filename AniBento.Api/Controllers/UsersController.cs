using AniBento.Api.Dtos.Collection;
using AniBento.Api.Dtos.User;
using AniBento.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AniBento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService, ICollectionService collectionService)
        : ControllerBase
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetPublicUserInfo(string username)
        {
            var result = await userService.GetPublicUserInfoByUsernameAsync(
                new GetPublicUserInfoRequest { UserName = username }
            );

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{username}/collections")]
        public async Task<
            ActionResult<IReadOnlyList<CollectionSummaryResponse>>
        > GetUserCollections(string username, CancellationToken ct)
        {
            var collections = await collectionService.GetCollectionsForUserAsync(username, ct);
            if (collections == null)
            {
                return NotFound();
            }
            return Ok(collections);
        }

        [HttpGet("{username}/collections/{collectionId:int}")]
        public async Task<ActionResult<CollectionResponse>> GetUserCollectionById(
            string username,
            int collectionId,
            CancellationToken ct
        )
        {
            var collection = await collectionService.GetCollectionForUserByIdAsync(
                username,
                collectionId,
                ct
            );
            if (collection == null)
            {
                return NotFound();
            }
            return Ok(collection);
        }
    }
}

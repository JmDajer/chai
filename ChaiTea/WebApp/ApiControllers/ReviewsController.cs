using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Beverages/{beverageId}/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private IAppBll _appBll;
        private PublicReviewMapper _mapper;


        public ReviewsController(IAppBll appBll, IMapper autoMapper)
        {
            _appBll = appBll;
            _mapper = new PublicReviewMapper(autoMapper);
        }

        /// <summary>
        /// Get reviews.
        /// </summary>
        /// <response code="200">Return a list of reviews.</response>
        /// <response code="400">Beverage id is null.</response>
        /// <returns>List of <c>PublicReview</c> objects.</returns>
        // GET: api/v1/Beverages/1/Reviews/
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<PublicReview?>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PublicReview?>>> GetReviews(Guid? beverageId)
        {
            if (beverageId == null) return BadRequest();

            var data = await _appBll.ReviewService.GetBeverageReviewsAsync(beverageId.Value);

            var res = data
                .Select(r => _mapper.Map(r))
                .ToList();

            return Ok(res);
        }

        /// <summary>
        /// Get review.
        /// </summary>
        /// <param name="id">Review id.</param>
        /// <param name="beverageId">Beverage id.</param>
        /// <response code="200">Return a list of reviews.</response>
        /// <response code="400">review id or beverage id is null.</response>
        /// <response code="404">Review doesn't exist.</response>
        /// <returns>A <c>PublicReview</c> object.</returns>
        // GET: api/v1/Beverages/1/Reviews/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<PublicReview?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicReview?>> GetReview(Guid? id, Guid? beverageId)
        {
            if (id == null || beverageId == null) return BadRequest();

            var review = await _appBll.ReviewService.FindAsync(id.Value);

            if (review == null) return NotFound();

            var res = _mapper.Map(review);

            return Ok(res);
        }

        /// <summary>
        /// Update review.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="review"></param>
        /// <response code="200">Return a list of reviews.</response>
        /// <response code="400">review id or beverage id is null.</response>
        /// <response code="404">Review doesn't exist.</response>
        // PUT: api/v1/Beverages/1/Reviews/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutReview(Guid? id, PublicReview? review)
        {
            if (id == null || review == null) return BadRequest();
            if (id != review.Id) return BadRequest();

            var bllReview = _mapper.Map(review)!;
            _appBll.ReviewService.Update(bllReview);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post review.
        /// </summary>
        /// <param name="review">Review id.</param>
        /// <param name="beverageId">Beverage id.</param>
        /// <response code="201">Return created review.</response>
        /// <response code="400">Review or beverage id is null.</response>
        /// <response code="401">User is un authorized.</response>
        /// <returns>Created <c>PublicReview</c> object.</returns>
        // POST: api/v1/Beverages/1/Reviews
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ActionResult<PublicReview>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicReview>> PostReview(PublicReview? review, Guid? beverageId)
        {
            if (review == null || beverageId == null) return BadRequest();
            
            var bllReview = _mapper.Map(review)!;
            _appBll.ReviewService.Add(bllReview);

            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        /// <summary>
        /// Delete review.
        /// </summary>
        /// <param name="id">Review id.</param>
        /// <response code="201">Successfully deleted review.</response>
        /// <response code="400">Review id is null.</response>
        /// <response code="401">User is un authorized.</response>
        /// <response code="404">Review doesn't exist.</response>
        // DELETE: api/Beverages/v1/Reviews/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReview(Guid? id)
        {
            if (id == null) return BadRequest();
            
            var review = _appBll.ReviewService.FindAsync(id.Value).Result;

            if (review == null) return NotFound();

            await _appBll.ReviewService.RemoveAsync(id.Value);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}
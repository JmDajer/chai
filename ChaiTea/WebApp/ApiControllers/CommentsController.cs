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
    [Route("api/v{version:apiVersion}/Reviews/{reviewId}/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly PublicCommentMapper _mapper;

        public CommentsController(IAppBll appBll, IMapper autoMapper)
        {
            _appBll = appBll;
            _mapper = new PublicCommentMapper(autoMapper);
        }
        
        /// <summary>
        /// Get all reviews comments.
        /// </summary>
        /// <response code="200">Returns the users comments.</response>
        // GET: api/v1/Reviews/1/Comments
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicComment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PublicComment>>> GetComments(Guid reviewId)
        {
            var data = await _appBll.CommentService.GetReviewsComments(reviewId);
            
            var res = data
                .Select(c => _mapper.Map(c))
                .ToList();

            return Ok(res);
        }

        /// <summary>
        /// Get comment.
        /// </summary>
        /// <param name="id">Comment id</param>
        /// <param name="reviewId">Review id</param>
        /// <response code="200">Returns the users comments.</response>
        /// <response code="400">If there is something wrong with the requested comment.</response>
        /// <response code="401">If a user route is accessed without an authentication token.</response>
        /// <response code="404">If comment doesn't exist.</response>
        /// <returns>PublicComment object</returns>
        // GET: api/v1/Reviews/1/Comments/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicComment), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicComment?>> GetComment(Guid? id, Guid? reviewId)
        {
            if (id == null || reviewId == null) return NotFound();

            var comment =  await _appBll.CommentService.FindAsync(id.Value);

            if (comment == null) return NotFound();

            if (comment.Id != id) return BadRequest();

            var res = _mapper.Map(comment);
            
            return Ok(res);
        }
        
        /// <summary>
        /// Update comment.
        /// </summary>
        /// <param name="id">Comment id</param>
        /// <param name="reviewId">Reviews id</param>
        /// <param name="comment">Comment that gets updated</param>
        /// <response code="204">If PUT was successful.</response>
        /// <response code="400">If the id and the comments id don't match.</response>
        /// <response code="401">If the user is not authorized.</response>
        // PUT: api/v1/Reviews/1/Comments/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutComment(Guid? id, PublicComment? comment, Guid? reviewId)
        {
            if (reviewId == null || id == null || comment == null) return BadRequest();
            if (id != comment.Id) return BadRequest();

            var bllComment = _mapper.Map(comment)!;
            _appBll.CommentService.Update(bllComment);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post comment.
        /// </summary>
        /// <param name="comment">Comment that gets posted</param>
        /// <param name="reviewId">Review id where the comment is under</param>
        /// <response code="201">If POST was successful.</response>
        /// <response code="400">If the id or review id are null.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <returns>Created <c>PublicComment</c></returns>
        // POST: api/v1/Reviews/1/Comments
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CreatedResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicComment>> PostComment(PublicComment? comment, Guid? reviewId)
        {
            if (comment == null || reviewId == null) return BadRequest();
            
            var bllComment = _mapper.Map(comment)!;
            _appBll.CommentService.Add(bllComment);
            
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        /// <summary>
        /// Delete comment.
        /// </summary>
        /// <param name="id">Comment id</param>
        /// <response code="204">If DELETE was successful.</response>
        /// <response code="400">If the id or review id are null.</response>
        /// <response code="401">If the user is not authorized.</response>
        // DELETE: api/v1/Reviews/1/Comments/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteComment(Guid? id)
        {
            if (id == null ) return BadRequest();
            
            var comment = _appBll.CommentService.RemoveAsync(id.Value);
            
            await _appBll.SaveChangesAsync();

            return NoContent();
            
            
        }
    }
}

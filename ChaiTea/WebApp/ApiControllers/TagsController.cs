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
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TagsController : ControllerBase
    {
        private IAppBll _appBll;
        private PublicTagMapper _mapper;

        public TagsController(IAppBll appBll, IMapper autoMapper)
        {
            _appBll = appBll;
            _mapper = new PublicTagMapper(autoMapper);
        }

        /// <summary>
        /// Get tags.
        /// </summary>
        /// <response code="200">Returns list of tags.</response>
        /// <returns>A list of <c>PublicTags</c> objects.</returns>
        // GET: api/v1/Tags/
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<PublicTag?>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicTag?>>> GetTags()
        {
            var data = await _appBll.TagService.AllAsync();

            var res = data
                .Select(t => _mapper.Map(t))
                .ToList();

            return Ok(res);
        }

        /// <summary>
        /// Get tag.
        /// </summary>
        /// <param name="id">Tags id.</param>
        /// <response code="200">Returns a tag.</response>
        /// <response code="400">Id is null.</response>
        /// <response code="404">Tag doesn't exist.</response>
        /// <returns>A <c>PublicTag</c> object.</returns>
        // GET: api/v1/Tags/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<PublicTag?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicTag?>> GetTag(Guid? id)
        {
            if (id == null) return BadRequest();

            var tag = await _appBll.TagService.FindAsync(id.Value);

            if (tag == null) return NotFound();

            var res = _mapper.Map(tag);

            return Ok(res);
        }

        /// <summary>
        /// Update tag.
        /// </summary>
        /// <param name="id">Tag id.</param>
        /// <param name="tag">Tag to be updated.</param>
        /// <response code="204">Tag was updated successfully.</response>
        /// <response code="400">Tag doesn't exist.</response>
        /// <response code="401">User is unauthorized.</response>
        // PUT: api/v1/Tags/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTag(Guid? id, PublicTag? tag)
        {
            if (id == null || tag == null) return BadRequest();
            if (id != tag.Id) return BadRequest();

            var bllTag = _mapper.Map(tag)!;
            _appBll.TagService.Update(bllTag);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post tag.
        /// </summary>
        /// <param name="tag">Tag to be posted.</param>
        /// <response code="201">Tag was posted successfully.</response>
        /// <response code="400">Tag is missing.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <returns>Created <c>PublicTag</c> object.</returns>
        // POST: api/v1/Tags
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ActionResult<PublicTag>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicTag>> PostTag(PublicTag? tag)
        {
            if (tag == null) return BadRequest();
            
            var bllTag = _mapper.Map(tag)!;
            _appBll.TagService.Add(bllTag);

            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
        }

        /// <summary>
        /// Delete tag.
        /// </summary>
        /// <param name="id">Tag id.</param>
        /// <response code="205">Tag was deleted successfully.</response>
        /// <response code="400">Id is missing.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="404">Tag doesn't exist.</response>
        // DELETE: api/v1/Tags/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTag(Guid? id)
        {
            if (id == null) return BadRequest();
            
            var tag = _appBll.TagService.FindAsync(id.Value).Result;

            if (tag == null) return NotFound();

            _appBll.TagService.Remove(tag);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}
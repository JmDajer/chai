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
    public class TagTypesController : ControllerBase
    {
        private IAppBll _appBll;
        private PublicTagTypeMapper _mapper;
        
        public TagTypesController(IAppBll appBll, IMapper autoMapper)
        {
            _appBll = appBll;
            _mapper = new PublicTagTypeMapper(autoMapper);
        }

        /// <summary>
        /// Get all tag types.
        /// </summary>
        /// <response code="200">Return a list of Tag Types.</response>
        /// <returns>A list of <c>PublicTagTypes</c> objects.</returns>
        // GET: api/v1/TagTypes/
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<PublicTagType?>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicTagType?>>> GetTagTypes()
        {
            var data = await _appBll.TagTypeService.AllAsync();

            var res = data
                .Select(tt => _mapper.Map(tt))
                .ToList();

            return Ok(res);
        }

        /// <summary>
        /// Get tag type
        /// </summary>
        /// <param name="id">Tag types id.</param>
        /// <response code="200">Return a Tag type.</response>
        /// <response code="404">If Tag type is missing.</response>
        /// <returns>A <c>PublicTagType</c> object.</returns>
        // GET: api/v1/TagTypes/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<PublicTagType?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicTagType?>> GetTagType(Guid? id)
        {
            if (id == null) return NotFound();

            var tagType = await _appBll.TagTypeService.FindAsync(id.Value);

            if (tagType == null) return NotFound();

            var res = _mapper.Map(tagType);

            return Ok(res);
        }

        /// <summary>
        /// Update tag type.
        /// </summary>
        /// <param name="id">Tag types id.</param>
        /// <param name="tagType">Tag type to update.</param>
        /// <response code="204">If the updating was successful.</response>
        /// <response code="400">If the id and the tag types id don't match.</response>
        /// <response code="401">If the user is not authorized.</response>
        // PUT: api/v1/TagTypes/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutTagType(Guid? id, PublicTagType? tagType)
        {
            if (id == null || tagType == null) return BadRequest();
            if (id != tagType.Id) return BadRequest();

            var bllTagType = _mapper.Map(tagType)!;
            _appBll.TagTypeService.Update(bllTagType);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add tag type.
        /// </summary>
        /// <param name="tagType">Tag type to add.</param>
        /// <response code="201">If the POST was successful.</response>
        /// <response code="400">If the tag type in null.</response>
        /// <response code="401">If the user in unauthorized.</response>
        /// <returns>Created <c>PublicTagType</c> object.</returns>
        // POST: api/v1/TagTypes
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CreatedResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicTagType>> PostTagType(PublicTagType? tagType)
        {
            if (tagType == null) return BadRequest();
            
            var bllTagType = _mapper.Map(tagType)!;
            _appBll.TagTypeService.Add(bllTagType);

            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetTagType", new { id = tagType.Id }, tagType);
        }

        /// <summary>
        /// Delete tag type.
        /// </summary>
        /// <param name="id">Tag types id</param>
        /// <response code="204">If the DELETE was successful.</response> 
        /// <response code="400">If the tag type in null.</response>    
        /// <response code="401">If the user in unauthorized.</response>
        /// <response code="404">If the tag type is doesn't exist.</response>
        // DELETE: api/v1/TagTypes/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTagType(Guid? id)
        {
            if (id == null) return BadRequest();

            var tagType = _appBll.TagTypeService.FindAsync(id.Value).Result;

            if (tagType == null) return NotFound();

            _appBll.TagTypeService.Remove(tagType);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}
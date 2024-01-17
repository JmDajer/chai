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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PicturesController : ControllerBase
    {
        private IAppBll _appBll;
        private PublicPictureMapper _mapper;

        public PicturesController(IAppBll appBll, IMapper autoMapper)
        {
            _appBll = appBll;
            _mapper = new PublicPictureMapper(autoMapper);
        }

        /// <summary>
        /// Get all beverages pictures.
        /// </summary>
        /// <param name="beverageId">Beverages id that the pictures belong to.</param>
        /// <response code="200">Returns beverages pictures.</response>
        /// <response code="400">If the beverage id is missing.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <returns>A list of picture objects.</returns>
        // GET: api/v1/Beverages/1/Pictures
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<PublicPicture>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PublicPicture>>> GetPictures(Guid? beverageId)
        {
            if (beverageId == null) return BadRequest();
            
            var data = await _appBll.PictureService.AllAsync();

            var res = data
                .Select(p => _mapper.Map(p))
                .ToList();

            return Ok(res);
        }

        /// <summary>
        /// Get picture.
        /// </summary>
        /// <param name="id">Picture id.</param>
        /// <param name="beverageId">Beverage id.</param>
        /// <response code="200">Returns beverages picture.</response>
        /// <response code="400">If the beverage or picture id is missing.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="404">If the picture is null.</response>
        /// <returns>A picture object.</returns>
        // GET: api/v1/Beverages/1/Pictures/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<PublicPicture?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicPicture?>> GetPicture(Guid? id, Guid? beverageId)
        {
            if (id == null || beverageId == null) return BadRequest();

            var picture = await _appBll.PictureService.FindAsync(id.Value);

            if (picture == null) return NotFound();

            var res = _mapper.Map(picture);

            return Ok(res);
        }


        /// <summary>
        /// Get a picture.
        /// </summary>
        /// <param name="id">Picture id.</param>
        /// <param name="beverageId">Beverage id.</param>
        /// <param name="picture">Picture for updating.</param>
        /// <response code="200">Returns beverages picture.</response>
        /// <response code="400">If the beverage or picture id is missing.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="404">If the picture is null.</response>
        // GET: api/v1/Beverages/1/Pictures/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutPicture(Guid? id, PublicPicture? picture, Guid? beverageId)
        {
            if (id == null || beverageId == null || picture == null) return BadRequest();
            if (id != picture.Id) return BadRequest();

            var bllPicture = _mapper.Map(picture)!;
            _appBll.PictureService.Update(bllPicture);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post picture.
        /// </summary>
        /// <param name="picture">Picture to be added.</param>
        /// <param name="beverageId">Beverage id for who the picture belongs to.</param>
        /// <response code="201">Returns created beverages picture.</response>
        /// <response code="400">If the beverage or picture id is missing.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <returns>Created <c>PublicPicture</c> object.</returns>
        // POST: api/v1/Beverages/1/Pictures
        [HttpPost]
        [ProducesResponseType(typeof(CreatedResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicPicture>> PostPicture(PublicPicture? picture, Guid? beverageId)
        {
            if (picture == null || beverageId == null) return BadRequest();
            
            var bllPicture = _mapper.Map(picture)!;
            _appBll.PictureService.Add(bllPicture);

            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetPicture", new { id = picture.Id }, picture);
        }

        /// <summary>
        /// Delete picture.
        /// </summary>
        /// <param name="id">Picture id.</param>
        /// <param name="beverageId">Beverage id of the picture.</param>
        /// <response code="204">If the deletion was successful</response>
        /// <response code="400">If the beverage or picture id is missing.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="404">Review doesn't exist</response>
        // DELETE: api/v1/Beverages/1/Pictures/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePicture(Guid? id, Guid? beverageId)
        {
            if (id == null || beverageId == null) return BadRequest();
            
            var review = _appBll.PictureService.FindAsync(id.Value).Result;

            if (review == null) return NotFound();

            await _appBll.PictureService.RemoveAsync(id.Value);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}
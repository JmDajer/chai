using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Helpers.Base;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BeveragesController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly PublicBeverageMapper _mapper;
        private readonly Guid _adminId = Guid.Parse("69a435c1-9d50-4d38-af58-c9406356efad");

        public BeveragesController(IAppBll appBll, IMapper autoMapper)
        {
            _appBll = appBll;
            _mapper = new PublicBeverageMapper(autoMapper);
        }

        /// <summary>
        /// Get all of the beverages that are the for public use.
        /// </summary>
        /// <response code="200">Return beverage.</response>
        /// <returns>A list of <c>PublicBeverage</c> objects.</returns>
        // GET: api/v1/Beverages
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<PublicBeverage?>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicBeverage?>>> GetBeverages()
        {
            var data = await _appBll.BeverageService.AllAsync();

            var res = data
                .Select(b => _mapper.Map(b))
                .ToList();
            
            return Ok(res);
        }

        /// <summary>
        /// Get beverage.
        /// </summary>
        /// <param name="id">Beverages id</param>
        /// <response code="200">Created beverage.</response>
        /// <response code="403">User is not authorized to get the beverage.</response>
        /// <response code="404">Beverage doesn't exist</response>
        /// <returns>A <c>PublicBeverage</c> object.</returns>
        // GET: api/v1/Beverages/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<PublicBeverage?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicBeverage?>> GetBeverage(Guid? id)
        {
            if (id == null) return NotFound();

            var beverage =  await _appBll.BeverageService.FindAsync(id.Value);

            if (beverage == null) return NotFound();
            
            var res = _mapper.Map(beverage);
            
            return Ok(res);
        }

        /// <summary>
        /// Update beverage.
        /// </summary>
        /// <param name="id">Beverage id</param>
        /// <param name="beverage">Beverage to be updated</param>
        /// <response code="201">Created beverage.</response>
        /// <response code="400">If given id and beverage id don't match</response>
        /// <response code="401">If the users is not authorized.</response>
        // PUT: api/v1/Beverages/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutBeverage(Guid id, PublicBeverage beverage)
        {
            if (id != beverage.Id) return BadRequest(new RestApiErrorResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Error = "Provided id and the beverage id don't match."
            });

            var bllBeverage = _mapper.Map(beverage)!;
            _appBll.BeverageService.Update(bllBeverage);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post beverage.
        /// </summary>
        /// <param name="beverage"></param>
        /// <response code="201">Created beverage.</response>
        /// <response code="401">User is not authorized.</response>
        /// <returns>The added <c>PublicBeverage</c> object.</returns>
        // POST: api/v1/Beverages
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof( CreatedAtActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicBeverage>> PostBeverage(PublicBeverage beverage)
        {
            var bllBeverage = _mapper.Map(beverage)!;
            _appBll.BeverageService.Add(bllBeverage);
            
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetBeverage", new { id = beverage.Id }, beverage);
        }

        /// <summary>
        /// Delete beverage.
        /// </summary>
        /// <param name="id">Beverage id</param>
        /// <response code="204">Returns when deletion was successful.</response>
        /// <response code="401">If the users is not authorized.</response>
        // DELETE: api/v1/Beverages/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBeverage(Guid id)
        {
            await _appBll.BeverageService.RemoveAsync(id);
            
            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get a list of recommended beverages.
        /// </summary>
        /// <param name="userId">Users id that recommended beverages are for</param>
        /// <response code="200">Returns the users beverages.</response>
        /// <response code="401">If the users is not authorized.</response>
        /// <response code="403">If the users id doesn't match with the logged in id.</response>
        /// <returns>List of beverages that are recommended for the user!</returns>
        // POST: api/v1/Beverages/Users/1/Recommended
        [HttpGet("Users/{userId}/Recommended")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IEnumerable<PublicBeverage?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicBeverage?>>> GetRecommendedBeverages(Guid userId)
        {
            if (User.GetUserId() != userId) return Forbid();
            
            var recommendedBeverages =
                (await _appBll.BeverageService.GetUserRecommendedBeverages(userId))
                .Select(b => _mapper.Map(b));

            return Ok(recommendedBeverages);
        }
        
        /// <summary>
        /// Get a list of users beverages.
        /// </summary>
        /// <param name="userId">Users id of the beverages</param>
        /// <response code="200">Returns the users beverages.</response>
        /// <response code="401">If the users is not authorized.</response>
        /// <response code="403">If the users id doesn't match with the logged in id.</response>
        /// <returns>List of users beverages.</returns>
        // POST: api/v1/Beverages/Users/1
        [HttpGet("Users/{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IEnumerable<PublicBeverage?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PublicBeverage?>>> GetUserBeverages(Guid userId)
        {
            if (User.GetUserId() != userId) return Forbid();

            var userBeverages =  await _appBll.BeverageService.GetUserBeverages(userId);

            return Ok(userBeverages);
        }
    }
}

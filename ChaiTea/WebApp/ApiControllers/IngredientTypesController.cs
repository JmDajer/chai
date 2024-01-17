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
    public class IngredientTypesController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly PublicIngredientTypeMapper _mapper;

        public IngredientTypesController(IAppBll appBll, IMapper autoMapper)
        {
            _appBll = appBll;
            _mapper = new PublicIngredientTypeMapper(autoMapper);
        }
        
        /// <summary>
        /// Get ingredient types.
        /// </summary>
        /// <response code="200">Return a list of ingredient types.</response>
        /// <returns>A list of <c>PublicIngredientType</c> objects.</returns>
        // GET: api/v1/IngredientTypes
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<PublicIngredientType?>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicIngredientType?>>> GetIngredientTypes()
        {
            var data = await _appBll.IngredientTypeService.AllAsync();

            var res = data
                .Select(it => _mapper.Map(it))
                .ToList();
            
            return Ok(res);
        }

        /// <summary>
        /// Get ingredient type.
        /// </summary>
        /// <param name="id">Ingredient type id.</param>
        /// <response code="200">Return ingredient type.</response>
        /// <returns>A <c>PublicIngredientType</c> object.</returns>
        // GET: api/v1/IngredientTypes/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<PublicIngredientType?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicIngredientType?>> GetIngredientType(Guid? id)
        {
            if (id == null) return BadRequest();

            var ingredientType =  await _appBll.IngredientTypeService.FindAsync(id.Value);

            if (ingredientType == null) return NotFound();

            var res = _mapper.Map(ingredientType);
            
            return Ok(res);
        }
        
        /// <summary>
        /// Update ingredient type.
        /// </summary>
        /// <param name="id">Ingredient type id</param>
        /// <param name="ingredientType">Ingredient type to be updated</param>
        /// <response code="204">Successfully added ingredient type.</response>
        /// <response code="400">Ingredient type is null.</response>
        /// <response code="401">User is unauthorized.</response>
        // PUT: api/v1/IngredientTypes/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutIngredientType(Guid id, PublicIngredientType ingredientType)
        {
            if (id != ingredientType.Id) return BadRequest();

            var bllIngredientType = _mapper.Map(ingredientType)!;
            _appBll.IngredientTypeService.Update(bllIngredientType);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post ingredient type.
        /// </summary>
        /// <param name="ingredientType">Ingredient type to be added.</param>
        /// <returns>Created <c>PublicIngredientType</c> object.</returns>
        /// <response code="201">Successfully added ingredient type.</response>
        /// <response code="400">Ingredient type is null.</response>
        /// <response code="401">User is unauthorized.</response>
        // POST: api/v1/IngredientTypes
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CreatedResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicIngredientType>> PostIngredientType(PublicIngredientType? ingredientType)
        {
            if (ingredientType == null) return BadRequest();
            
            var bllIngredientType = _mapper.Map(ingredientType)!;
            _appBll.IngredientTypeService.Add(bllIngredientType);
            
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetIngredientType", new { id = ingredientType.Id }, ingredientType);
        }

        /// <summary>
        /// Delete ingredient type.
        /// </summary>
        /// <param name="id">Ingredient type id</param>
        /// <response code="204">Successfully delete ingredient type.</response>
        /// <response code="400">Ingredient type is null.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="404">Ingredient type doesn't exist.</response>
        // DELETE: api/v1/IngredientTypes/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteIngredientType(Guid? id)
        {
            if (id == null) return BadRequest();
            
            var ingredientType = _appBll.IngredientTypeService.FindAsync(id.Value).Result;
            
            if (ingredientType == null)
            {
                return NotFound();
            }

            _appBll.IngredientTypeService.Remove(ingredientType);
            
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}

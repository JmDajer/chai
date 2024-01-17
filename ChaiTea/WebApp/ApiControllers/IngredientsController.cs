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
    public class IngredientsController : ControllerBase
    {
        private readonly IAppBll _appBll;
        private readonly PublicIngredientMapper _mapper;

        public IngredientsController(IAppBll appBll, IMapper autoMapper)
        {
            _appBll = appBll;
            _mapper = new PublicIngredientMapper(autoMapper);
        }
        
        /// <summary>
        /// Get ingredients.
        /// </summary>
        /// <response code="200">Return a list of ingredients.</response>
        /// <returns>A list of <c>PublicIngredients</c> objects.</returns>
        // GET: api/v1/Ingredients
        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<PublicIngredient?>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicIngredient?>>> GetIngredients()
        {
            var data = await _appBll.IngredientService.AllAsync();

            var res = data
                .Select(i => _mapper.Map(i))
                .ToList();
            
            return Ok(res);
        }

        /// <summary>
        /// Get ingredient.
        /// </summary>
        /// <param name="id">Ingredient id.</param>
        /// <returns>A <c>PublicIngredient</c> object.</returns>
        /// <response code="200">Return ingredient.</response>
        /// <response code="400">Id is null.</response>
        /// <response code="404">Ingredient doesn't exist.</response>
        // GET: api/v1/Ingredients/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResult<PublicIngredient?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicIngredient?>> GetIngredient(Guid? id)
        {
            if (id == null) return BadRequest();

            var ingredient =  await _appBll.IngredientService.FindAsync(id.Value);

            if (ingredient == null) return NotFound();

            var res = _mapper.Map(ingredient);
            
            return Ok(res);
        }
        
        /// <summary>
        /// Update ingredient.
        /// </summary>
        /// <param name="id">Ingredient id</param>
        /// <param name="ingredient">Ingredient to be updated.</param>
        /// <response code="204">Return ingredient.</response>
        /// <response code="400">Id or ingredient is null.</response>
        /// <response code="401">User is unauthorized.</response>
        // PUT: api/v1/Ingredients/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutIngredient(Guid? id, PublicIngredient? ingredient)
        {
            if (id == null || ingredient == null) return BadRequest();
            if (id != ingredient.Id) return BadRequest();

            var bllIngredient = _mapper.Map(ingredient)!;
            _appBll.IngredientService.Update(bllIngredient);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post ingredient.
        /// </summary>
        /// <param name="ingredient">Ingredient to be posted.</param>
        /// <returns>Created <c>PublicIngredient</c> object.</returns>
        /// <response code="201">Return ingredient.</response>
        /// <response code="400">Ingredient is null.</response>
        /// <response code="401">User is unauthorized.</response>
        // POST: api/v1/Ingredients
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ActionResult<PublicIngredient>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicIngredient>> PostIngredient(PublicIngredient? ingredient)
        {
            if (ingredient == null) return BadRequest();
            
            var bllIngredient = _mapper.Map(ingredient)!;
            _appBll.IngredientService.Add(bllIngredient);
            
            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        /// <summary>
        /// Delete ingredient.
        /// </summary>
        /// <param name="id">Ingredient id.</param>
        /// <response code="204">Ingredient deleted.</response>
        /// <response code="400">Id is null.</response>
        /// <response code="401">User is unauthorized.</response>
        /// <response code="404">Ingredient doesn't exist.</response>
        // DELETE: api/v1/Ingredients/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteIngredient(Guid? id)
        {
            if (id == null) return BadRequest();
            
            var ingredient = _appBll.IngredientService.FindAsync(id.Value).Result;
            
            if (ingredient == null) return NotFound();

            _appBll.IngredientService.Remove(ingredient);
            
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}

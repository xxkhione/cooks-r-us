using Microsoft.AspNetCore.Mvc;
using ThirdPartyIntegration.API.Services;

namespace ThirdPartyIntegration.API.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeApiService _recipeApiService;

        public RecipeController(RecipeApiService recipeApiService)
        {
            _recipeApiService = recipeApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes([FromQuery] string? searchQuery)
        {
            var recipes = await _recipeApiService.GetMealsAsync(searchQuery);

            if(recipes == null || recipes.Count == 0)
            {
                return NotFound("No recipes found.");
            }

            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeDetails(int id)
        {
            var recipe = await _recipeApiService.GetMealAsync(id);

            if(recipe == null)
            {
                return NotFound("Recipe not found.");
            }

            return Ok(recipe);
        }
    }
}

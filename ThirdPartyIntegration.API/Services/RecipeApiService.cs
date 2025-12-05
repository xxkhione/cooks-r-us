using System.Numerics;
using System.Text.Json;
using ThirdPartyIntegration.API.Models;

namespace ThirdPartyIntegration.API.Services
{
    public class RecipeApiService
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey = "1"; //I refuse to pay for an upgrade of this simple API
        private readonly string baseUrl = "https://www.themealdb.com/api/json/v1/";

        public RecipeApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<Recipe>> GetMealsAsync(string searchQuery = null)
        {
            string url = $"{apiKey}/";
            if(!string.IsNullOrEmpty(searchQuery))
            {
                url += $"search.php?s={searchQuery}";
            }

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                using JsonDocument json = JsonDocument.Parse(content);
                JsonElement root = json.RootElement;
                JsonElement meals = root.GetProperty("meals");

                List<Recipe> recipes = new List<Recipe>();
                var tasks = meals.EnumerateArray().Select(async recipeElement =>
                {
                    string strApiRecipeId = recipeElement.GetProperty("idMeal").GetString();
                    int apiRecipeId = int.Parse(strApiRecipeId);
                    return await GetMealAsync(apiRecipeId);
                });

                recipes = (await Task.WhenAll(tasks)).ToList();

                return recipes;
            }
            catch (HttpRequestException ex)
            {
                return new List<Recipe>();
            }
            catch (JsonException ex)
            {
                return new List<Recipe>();
            }
        }

        public async Task<Recipe> GetMealAsync(int apiRecipeId)
        {
            string url = $"{apiKey}/lookup.php?i={apiRecipeId}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var recipeResponse = await response.Content.ReadAsStringAsync();
                using JsonDocument json = JsonDocument.Parse(recipeResponse);
                JsonElement root = json.RootElement;
                JsonElement meals = root.GetProperty("meals")[0];
                

                return new Recipe
                {
                    ApiRecipeId = int.Parse(meals.GetProperty("idMeal").GetString()),
                    RecipeName = meals.GetProperty("strMeal").GetString(),
                    Image = meals.GetProperty("strMealThumb").GetString(),
                    Directions = meals.GetProperty("strInstructions").GetString(),
                    IngredientList = GetIngredientsAndMeasurements(meals)
                };
            }
            return null;
        }

        private List<Ingredient> GetIngredientsAndMeasurements(JsonElement recipeElement)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            for (int i = 1; i < 20; i++)
            {
                if (!string.IsNullOrWhiteSpace(recipeElement.GetProperty($"strIngredient{i}").ToString()))
                {
                    string ingredientName = recipeElement.GetProperty($"strIngredient{i}").ToString();
                    string measurement = recipeElement.GetProperty($"strMeasure{i}").ToString();

                    Ingredient newIngredient = new Ingredient
                    {
                        IngredientName = ingredientName,
                        Measurement = measurement
                    };
                    ingredients.Add(newIngredient);
                } else
                {
                    break;
                }
            }
            return ingredients;
        }
    }
}

using CooksRUs.Database;
using CooksRUs.Entities;
using CooksRUs.Models;

namespace CooksRUs.Services
{
    public class LocalSaveService
    {
        private readonly CooksRUsDbContext _db;
        private readonly UserRecipeService _userRecipeService;
        private readonly HttpClient _http;

        public LocalSaveService(CooksRUsDbContext db, UserRecipeService userRecipeService, HttpClient http)
        {
            _db = db;
            _userRecipeService = userRecipeService;
            _http = http;
        }

        private async Task<byte[]?> DownloadImageBytesAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadAsByteArrayAsync();
        }

        public async Task<int> SaveRecipeAsync(Recipe dto)
        {
            var imageBytes = await DownloadImageBytesAsync(dto.Image);
            var recipeEntity = dto.ToRecepieEntity(creatorId: 0, imageBytes: null);

            var listEntity = new ingredient_list
            {
                recepie = recipeEntity
            };

            foreach (var ingDto in dto.IngredientList)
            {
                listEntity.ingredients.Add(ingDto.ToIngredientEntity(listId: 0));
            }

            await _userRecipeService.SaveRecipeAsync(recipeEntity);

            return recipeEntity.id;
        }
    }
}

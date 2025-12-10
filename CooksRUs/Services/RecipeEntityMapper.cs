using CooksRUs.Entities;
using CooksRUs.Models;

namespace CooksRUs.Services
{
    public static class RecipeEntityMapper
    {
        public static recepie ToRecepieEntity(this Recipe dto, int creatorId, byte[]? imageBytes = null)
        {
            return new recepie
            {
                recepie_name = dto.RecipeName,
                directions = dto.Directions,
                image = imageBytes,
                image_url = dto.Image,
                creatorid = creatorId
            };
        }

        public static ingredient_list ToIngredientListEntity(this recepie recipe)
        {
            return new ingredient_list
            {
                recepie_id = recipe.id
            };
        }

        public static ingredient ToIngredientEntity(this Ingredient dto, int listId)
        {
            return new ingredient
            {
                list_id = listId,
                ingredient_name = dto.IngredientName,
                amount = dto.Measurement
            };
        }
    }
}

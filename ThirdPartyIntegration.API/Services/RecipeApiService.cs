namespace ThirdPartyIntegration.API.Services
{
    public class RecipeApiService
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey = "1"; //I refuse to pay for an upgrade of this simple API
        private readonly string baseUrl = "www.themealdb.com/api/json/v1/";

        public RecipeApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(baseUrl);
        }

        //Search query (potentially multiple) -> search.php?s=
        //Search query (single) -> lookup.php?i=
        //Category query -> filter.php?c=
        //Each ingredient's measurement is associated with the number supplied
        //Use the tags to search by type of food (most likely will use RegEx for this bit
        //Allow the search of categories (could make a drop down for this in the front-end?)

        public async Task GetMealsAsync(string searchQuery = null)
        {
            //Overall search
        }

        public async Task GetMealAsync(string lookupQuery = null)
        {
            //For this, we would take the id given when doing a basic search
            //More like the details for a meal if that makes sense
        }

        public async Task GetCategoriesAsync(string filterQuery = null)
        {
            //Search for specific categories
            //Filter out the results based on the chosen category, if applicable
        }
    }
}

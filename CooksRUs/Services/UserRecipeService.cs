using CooksRUs.Components.Cookie;
using CooksRUs.Database;
using CooksRUs.Entities;
using Microsoft.EntityFrameworkCore;

namespace CooksRUs.Services
{
    public class UserRecipeService
    {
        private readonly CooksRUsDbContext dbContext;
        private readonly ICookie cookie;

        public UserRecipeService(CooksRUsDbContext dbContext, ICookie cookie)
        {
            this.dbContext = dbContext;
            this.cookie = cookie;
        }

        public async Task SaveRecipeAsync(recepie r)
        {
            int userID = await cookie.GetValue();
            var user = await dbContext.users
                .Include(u => u.recepies)
                .FirstAsync(u => u.id == userID);
            user.recepies.Add(r);
            await dbContext.SaveChangesAsync();
        }
    }
}

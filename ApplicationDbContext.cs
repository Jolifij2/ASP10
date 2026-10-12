using Microsoft.EntityFrameworkCore;
using RecipesApp.Models;

namespace RecipesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Начальные данные
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, Name = "Борщ", Cuisine = "Русская", CookingTime = 120, Difficulty = 4, Description = "Традиционный свекольный суп с мясом и овощами." },
                new Recipe { Id = 2, Name = "Паста Карбонара", Cuisine = "Итальянская", CookingTime = 30, Difficulty = 2, Description = "Спагетти с беконом, яйцом и сыром пармезан." },
                new Recipe { Id = 3, Name = "Суши Филадельфия", Cuisine = "Японская", CookingTime = 60, Difficulty = 5, Description = "Роллы с лососем, сливочным сыром и огурцом." },
                new Recipe { Id = 4, Name = "Пельмени", Cuisine = "Русская", CookingTime = 90, Difficulty = 3, Description = "Тесто с мясной начинкой, отварные или жареные." },
                new Recipe { Id = 5, Name = "Тирамису", Cuisine = "Итальянская", CookingTime = 45, Difficulty = 3, Description = "Десерт из печенья савоярди, кофе и маскарпоне." }
            );
        }
    }
}

using AutoMapper;
using CookBook.DbManager;
using CookBook.Models;
using CookBook.Models.Entities;
using CookBook.Repositories.Classes;
using Microsoft.EntityFrameworkCore;

namespace CookBookTests
{
    public class RecipeRepositoryTests
    {
        [Fact]
        public async Task GetRecipe_ShouldReturnRecipe_WhenRecipeExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes1")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                var sampleRecipe = new Recipe { RecipeID = 1, Name = "Sample Recipe", Time = "2h", Ingredients = "Ingredient1", Preparation = "Step1", IsFollowed = false };
                context.Recipes.Add(sampleRecipe);
                context.SaveChanges();

                // Act
                var result = await repository.GetRecipe(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Sample Recipe", result.Name);
            }
        }

        [Fact]
        public async Task GetRecipe_ShouldReturnNull_WhenRecipeDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes2")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                // Act
                var result = await repository.GetRecipe(1);

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetAllRecipes_ShouldReturnAllRecipesOrderedByName()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes3")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                var recipes = new List<Recipe>
                {
                    new Recipe { RecipeID = 1, Name = "Recipe A", Time = "2h", Ingredients = "Ingredient A", Preparation = "Step A", IsFollowed = false },
                    new Recipe { RecipeID = 2, Name = "Recipe B", Time = "3h", Ingredients = "Ingredient B", Preparation = "Step B", IsFollowed = true },
                    new Recipe { RecipeID = 3, Name = "Recipe C", Time = "4h", Ingredients = "Ingredient C", Preparation = "Step C", IsFollowed = false }
                };
                context.Recipes.AddRange(recipes);
                context.SaveChanges();

                // Act
                var result = await repository.GetAllRecipes();
                var resultList = result.ToList();

                // Assert
                Assert.Equal(3, resultList.Count);
                Assert.Equal("Recipe A", resultList[0].Name);
                Assert.Equal("Recipe B", resultList[1].Name);
                Assert.Equal("Recipe C", resultList[2].Name);
            }
        }

        [Fact]
        public async Task GetFavourites_ShouldReturnFavouriteRecipesOrderedByName()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes4")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                var recipes = new List<Recipe>
                {
                    new Recipe { RecipeID = 1, Name = "Recipe A", Time = "2h", Ingredients = "Ingredient A", Preparation = "Step A", IsFollowed = true },
                    new Recipe { RecipeID = 2, Name = "Recipe B", Time = "3h", Ingredients = "Ingredient B", Preparation = "Step B", IsFollowed = false },
                    new Recipe { RecipeID = 3, Name = "Recipe C", Time = "4h", Ingredients = "Ingredient C", Preparation = "Step C", IsFollowed = true }
                };
                context.Recipes.AddRange(recipes);
                context.SaveChanges();

                // Act
                var result = await repository.GetFavourites();
                var resultList = result.ToList();

                // Assert
                Assert.Equal(2, result.Count());
                Assert.Equal("Recipe A", resultList[0].Name);
                Assert.Equal("Recipe C", resultList[1].Name);
            }
        }

        [Fact]
        public async Task AddRecipe_ShouldAddRecipeToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes5")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);
                var newRecipe = new RecipeDTO { Name = "New Recipe", Time = "2h", Ingredients = "New Ingredient", Preparation = "New Step", IsFollowed = false };

                // Act
                await repository.AddRecipe(newRecipe);

                // Assert
                Assert.Equal(1, context.Recipes.Count());
                Assert.Equal("New Recipe", context.Recipes.First().Name);
            }
        }

        [Fact]
        public async Task UpdateRecipe_ShouldUpdateExistingRecipe()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes6")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                var sampleRecipe = new Recipe { RecipeID = 1, Name = "Sample Recipe", Time = "2h", Ingredients = "Ingredient1", Preparation = "Step1", IsFollowed = false };
                context.Recipes.Add(sampleRecipe);
                context.SaveChanges();

                var updatedRecipe = new RecipeDTO { Name = "Updated Recipe", Time = "3h", Ingredients = "Updated Ingredient", Preparation = "Updated Step", IsFollowed = true };

                // Act
                await repository.UpdateRecipe(1, updatedRecipe);

                // Assert
                var result = context.Recipes.First();
                Assert.Equal("Updated Recipe", result.Name);
                Assert.Equal("3h", result.Time);
                Assert.Equal("Updated Ingredient", result.Ingredients);
                Assert.Equal("Updated Step", result.Preparation);
                Assert.False(result.IsFollowed);
            }
        }

        [Fact]
        public async Task RemoveRecipe_ShouldRemoveRecipeFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes7")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                var sampleRecipe = new Recipe { RecipeID = 1, Name = "Sample Recipe", Time = "2h", Ingredients = "Ingredient1", Preparation = "Step1", IsFollowed = false };
                context.Recipes.Add(sampleRecipe);
                context.SaveChanges();

                // Act
                await repository.RemoveRecipe(1);

                // Assert
                Assert.Equal(0, context.Recipes.Count());
            }
        }

        [Fact]
        public async Task FollowRecipe_ShouldSetIsFollowedToTrue_WhenRecipeExists()
        {
            // Arrange
            var recipeId = 1;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes8")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                var recipe = new Recipe { RecipeID = recipeId, Name = "Test Recipe", Time = "2h", Ingredients = "Test Ingredients", Preparation = "Test Preparation", IsFollowed = false };

                context.Recipes.Add(recipe);
                context.SaveChanges();

                // Act
                await repository.FollowRecipe(recipeId);

                // Assert
                var result = context.Recipes.SingleOrDefault(r => r.RecipeID == recipeId);
                Assert.NotNull(result);
                Assert.True(result.IsFollowed);
            }
        }

        [Fact]
        public async Task UnfollowRecipe_ShouldSetIsFollowedToFalse_WhenRecipeExists()
        {
            // Arrange
            var recipeId = 1;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes9")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                var recipe = new Recipe { RecipeID = recipeId, Name = "Test Recipe", Time = "2h", Ingredients = "Test Ingredients", Preparation = "Test Preparation", IsFollowed = true };

                context.Recipes.Add(recipe);
                context.SaveChanges();

                // Act
                await repository.UnfollowRecipe(recipeId);

                // Assert
                var result = context.Recipes.SingleOrDefault(r => r.RecipeID == recipeId);
                Assert.NotNull(result);
                Assert.False(result.IsFollowed);
            }
        }

        [Fact]
        public async Task SearchRecipes_ShouldReturnMatchingRecipes_WhenSearchTermExists()
        {
            // Arrange
            var searchTerm = "Chicken";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Recipes10")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            using (var context = new AppDbContext(options))
            {
                var repository = new RecipeRepository(context, mapper);

                var recipes = new List<Recipe>
                {
                    new Recipe { RecipeID = 1, Name = "Chicken Curry", Time = "2h", Ingredients = "Chicken, Curry Powder", Preparation = "Cooking steps", IsFollowed = false },
                    new Recipe { RecipeID = 2, Name = "Grilled Chicken", Time = "3h", Ingredients = "Chicken, Olive Oil", Preparation = "Grilling steps", IsFollowed = true },
                    new Recipe { RecipeID = 3, Name = "Vegetarian Pasta", Time = "4h", Ingredients = "Pasta, Tomato Sauce", Preparation = "Boiling steps", IsFollowed = false }
                };

                context.Recipes.AddRange(recipes);
                context.SaveChanges();

                // Act
                var result = await repository.SearchRecipes(searchTerm);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
                Assert.Contains(result, r => r.Name.Contains(searchTerm));
            }
        }
    }
}

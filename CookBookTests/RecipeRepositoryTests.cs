using CookBook.Models;
using CookBook.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CookBookTests
{
    public class RecipeRepositoryTests
    {
        [Fact]
        public void GetRecipe_ShouldReturnRecipe_WhenRecipeExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RecipeManagerContext>()
                .UseInMemoryDatabase(databaseName: "Recipes1")
                .Options;

            using (var context = new RecipeManagerContext(options))
            {
                var repository = new RecipeRepository(context);

                var sampleRecipe = new RecipeModel { RecipeID = 1, Name = "Sample Recipe", Time = "2h", Ingredients = "Ingredient1", Preparation = "Step1", IsFollowed = false };
                context.Recipes.Add(sampleRecipe);
                context.SaveChanges();

                // Act
                var result = repository.GetRecipe(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Sample Recipe", result.Name);
            }
        }

        [Fact]
        public void GetRecipe_ShouldReturnNull_WhenRecipeDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RecipeManagerContext>()
                .UseInMemoryDatabase(databaseName: "Recipes2")
                .Options;

            using (var context = new RecipeManagerContext(options))
            {
                var repository = new RecipeRepository(context);

                // Act
                var result = repository.GetRecipe(1);

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void GetAllRecipes_ShouldReturnAllRecipesOrderedByName()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RecipeManagerContext>()
                .UseInMemoryDatabase(databaseName: "Recipes3")
                .Options;

            using (var context = new RecipeManagerContext(options))
            {
                var repository = new RecipeRepository(context);

                var recipes = new List<RecipeModel>
            {
                new RecipeModel { RecipeID = 1, Name = "Recipe A", Time = "2h", Ingredients = "Ingredient A", Preparation = "Step A", IsFollowed = false },
                new RecipeModel { RecipeID = 2, Name = "Recipe B", Time = "3h", Ingredients = "Ingredient B", Preparation = "Step B", IsFollowed = true },
                new RecipeModel { RecipeID = 3, Name = "Recipe C", Time = "4h", Ingredients = "Ingredient C", Preparation = "Step C", IsFollowed = false }
            };
                context.Recipes.AddRange(recipes);
                context.SaveChanges();

                // Act
                var result = repository.GetAllRecipes().ToList();

                // Assert
                Assert.Equal(3, result.Count);
                Assert.Equal("Recipe A", result[0].Name);
                Assert.Equal("Recipe B", result[1].Name);
                Assert.Equal("Recipe C", result[2].Name);
            }
        }

        [Fact]
        public void GetFavourites_ShouldReturnFavouriteRecipesOrderedByName()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RecipeManagerContext>()
                .UseInMemoryDatabase(databaseName: "Recipes4")
                .Options;

            using (var context = new RecipeManagerContext(options))
            {
                var repository = new RecipeRepository(context);

                var recipes = new List<RecipeModel>
            {
                new RecipeModel { RecipeID = 1, Name = "Recipe A", Time = "2h", Ingredients = "Ingredient A", Preparation = "Step A", IsFollowed = true },
                new RecipeModel { RecipeID = 2, Name = "Recipe B", Time = "3h", Ingredients = "Ingredient B", Preparation = "Step B", IsFollowed = false },
                new RecipeModel { RecipeID = 3, Name = "Recipe C", Time = "4h", Ingredients = "Ingredient C", Preparation = "Step C", IsFollowed = true }
            };
                context.Recipes.AddRange(recipes);
                context.SaveChanges();

                // Act
                var result = repository.GetFavourites().ToList();

                // Assert
                Assert.Equal(2, result.Count);
                Assert.Equal("Recipe A", result[0].Name);
                Assert.Equal("Recipe C", result[1].Name);
            }
        }

        [Fact]
        public void AddRecipe_ShouldAddRecipeToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RecipeManagerContext>()
                .UseInMemoryDatabase(databaseName: "Recipes5")
                .Options;

            using (var context = new RecipeManagerContext(options))
            {
                var repository = new RecipeRepository(context);
                var newRecipe = new RecipeModel { Name = "New Recipe", Time = "2h", Ingredients = "New Ingredient", Preparation = "New Step", IsFollowed = false };

                // Act
                repository.AddRecipe(newRecipe);

                // Assert
                Assert.Equal(1, context.Recipes.Count());
                Assert.Equal("New Recipe", context.Recipes.First().Name);
            }
        }

        [Fact]
        public void UpdateRecipe_ShouldUpdateExistingRecipe()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RecipeManagerContext>()
                .UseInMemoryDatabase(databaseName: "Recipes6")
                .Options;

            using (var context = new RecipeManagerContext(options))
            {
                var repository = new RecipeRepository(context);

                var sampleRecipe = new RecipeModel { RecipeID = 1, Name = "Sample Recipe", Time = "2h", Ingredients = "Ingredient1", Preparation = "Step1", IsFollowed = false };
                context.Recipes.Add(sampleRecipe);
                context.SaveChanges();

                var updatedRecipe = new RecipeModel { Name = "Updated Recipe", Time = "3h", Ingredients = "Updated Ingredient", Preparation = "Updated Step", IsFollowed = true };

                // Act
                repository.UpdateRecipe(1, updatedRecipe);

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
        public void RemoveRecipe_ShouldRemoveRecipeFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RecipeManagerContext>()
                .UseInMemoryDatabase(databaseName: "Recipes7")
                .Options;

            using (var context = new RecipeManagerContext(options))
            {
                var repository = new RecipeRepository(context);

                var sampleRecipe = new RecipeModel { RecipeID = 1, Name = "Sample Recipe", Time = "2h", Ingredients = "Ingredient1", Preparation = "Step1", IsFollowed = false };
                context.Recipes.Add(sampleRecipe);
                context.SaveChanges();

                // Act
                repository.RemoveRecipe(1);

                // Assert
                Assert.Equal(0, context.Recipes.Count());
            }
        }
    }
}

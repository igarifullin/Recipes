using Microsoft.EntityFrameworkCore;
using Recipe.Data.Auth;
using Recipe.Data.Recipes;

namespace Recipe.Data
{
    public class RecipeContext : DbContext
    {
        public DbSet<Recipes.Recipe> Recipes { get; set; }
        public DbSet<RecipeVariation> RecipeVariations { get; set; }
        
        public DbSet<RecipeHistory> RecipeHistories { get; set; }

        public DbSet<User> Users { get; set; }

        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipes.Recipe>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<Recipes.Recipe>()
                .Property(r => r.Name)
                .HasMaxLength(200);
            modelBuilder.Entity<Recipes.Recipe>()
                .HasMany<RecipeVariation>()
                .WithOne(rv => rv.Recipe)
                .HasForeignKey(rv => rv.RecipeId);

            modelBuilder.Entity<RecipeVariation>()
                .HasKey(rv => rv.Id);
            modelBuilder.Entity<RecipeVariation>()
                .HasOne<User>(rv => rv.Author)
                .WithMany(u => u.Recipes)
                .HasForeignKey(rv => rv.CreatedBy);

            modelBuilder.Entity<RecipeHistory>()
                .HasKey(rh => rh.Id);
            modelBuilder.Entity<RecipeHistory>()
                .HasOne(rh => rh.Recipe)
                .WithMany(r => r.History)
                .HasForeignKey(rh => rh.RecipeId);
            modelBuilder.Entity<RecipeHistory>()
                .HasOne<User>(rv => rv.Changer)
                .WithMany(u => u.RecipeChanges)
                .HasForeignKey(rv => rv.ChangedBy);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.Login)
                .HasMaxLength(200);

            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.RecipeVariation)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(i => i.RecipeVariationId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
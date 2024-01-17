using Domain.App;
using Domain.App.Identity;
using Domain.App.ManyToMany;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Beverage> Beverages { get; set; } = default!;
    public DbSet<Picture> Pictures { get; set; } = default!;
    public DbSet<Review> Reviews { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<TagType> TagTypes { get; set; } = default!;
    public DbSet<Ingredient> Ingredients { get; set; } = default!;
    public DbSet<IngredientType> IngredientTypes { get; set; } = default!;
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;

    public DbSet<BeverageTag> BeverageTags { get; set; } = default!;
    public DbSet<BeverageIngredient> BeverageIngredients { get; set; } = default!;
    public DbSet<BeverageBeverage> BeverageBeverages { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {   
        // let the initial run
        base.OnModelCreating(builder);
        
        // Create M:M table for BeverageTags
        builder.Entity<Beverage>()
            .HasMany(b => b.Tags)
            .WithMany(t => t.Beverages)
            .UsingEntity<BeverageTag>(
                j => j.ToTable("BeverageTag")
            );
        
        // Create M:M table for BeverageIngredients
        builder.Entity<Beverage>()
            .HasMany(b => b.Ingredients)
            .WithMany(i => i.Beverages)
            .UsingEntity<BeverageIngredient>(
                j => j.ToTable("BeverageIngredient")
            );

        // Create M:M table for BeverageBeverages
        builder.Entity<Beverage>()
            .HasMany(b => b.SubBeverages)
            .WithMany(b => b.ParentBeverages)
            .UsingEntity<BeverageBeverage>(
                j => j
                    .HasOne(b => b.SubBeverage)
                    .WithMany()
                    .HasForeignKey(b => b.SubBeverageId),
        j => j
                    .HasOne(b => b.ParentBeverage)
                    .WithMany()
                    .HasForeignKey(b => b.ParentBeverageId),
        j =>
                {
                    j.HasKey(b => new { b.ParentBeverageId, b.SubBeverageId });
                    j.ToTable("BeverageBeverage");
                }
            );
    }
    
}
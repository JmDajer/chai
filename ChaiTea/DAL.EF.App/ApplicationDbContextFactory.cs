// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
//
// namespace DAL.EF.App;
//
// public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
// {
//     // Only needed for generating controllers and APIs. 
//     
//     public ApplicationDbContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//         // Does not connect to the database
//         optionsBuilder.UseNpgsql("");
//         return new ApplicationDbContext(optionsBuilder.Options);
//     }
// }
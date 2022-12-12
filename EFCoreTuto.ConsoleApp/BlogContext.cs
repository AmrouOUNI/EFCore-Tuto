using EFCoreTuto.ConsoleApp.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EFCoreTuto.ConsoleApp
{
    internal class BlogContext : DbContext
    {

        private static string connString = "Data Source=localhost; Initial Catalog=efcore_tuto; Integrated Security=SSPI;TrustServerCertificate=True";

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Author> Authors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connString)
                 .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                 .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfigaration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
    }
}

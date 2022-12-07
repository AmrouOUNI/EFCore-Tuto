using Microsoft.EntityFrameworkCore;

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
            options.UseSqlServer(connString);
        }

    }
}

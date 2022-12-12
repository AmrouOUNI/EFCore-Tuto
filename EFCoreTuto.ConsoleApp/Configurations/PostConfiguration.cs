using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTuto.ConsoleApp.Configurations
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
               .Property(b => b.Title)
               .HasDefaultValue("No title");
            builder.HasIndex(b => b.Title);

            var posts = new List<Post> {
                new Post
                {
                    Id = 100,
                    Title = "EF Core migarations",
                    Content= "Lorem Ipsum",
                    BlogId = 2,
                },

                 new Post
                {
                      Id = 101,
                    Title = "EF Core Model",
                    Content= "Lorem Ipsum",
                    BlogId = 2,
                 },
            };

            
            builder.HasData(posts);
        }
    }
}

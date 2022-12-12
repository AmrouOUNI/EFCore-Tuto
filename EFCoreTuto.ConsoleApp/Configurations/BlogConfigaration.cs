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
    internal class BlogConfigaration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasQueryFilter(b => !b.Deleted);
            //                .HasMany(b => b.Posts).WithOne( p => p.Blog);


            builder.Property(b => b.Title)
                .HasDefaultValue("No title");

            builder.HasData(
                new Blog
                {
                    Id = 2,
                    Url = "https://learn.microsoft.com/en-us/ef/efcore-and-ef6/support"
                }
                );

        }
    }
}

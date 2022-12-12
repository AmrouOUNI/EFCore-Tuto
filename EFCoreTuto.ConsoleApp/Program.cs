using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFCoreTuto.ConsoleApp
{
    internal class Program
    {

        static private BlogContext context = new BlogContext();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start!");

            /*await ElementFromDB();*/

            //// Operations
           /* await SimpleCreateBlogRecord();

            await SimpleUpdateBlogRecord();

            await InsertMutiplePosts();

            SimpleProjection();*/

            //// Jointures 
            /*Join();

            GroupJoin();

            GroupBy();*/

            //// Loading
           /* EagerLoading();*/

            //// SQL 
            /*await ExecuteSQLStoredProcedure();*/

            // Navigation
            IgnoreBlogsQueryFilters();
            
            // Traking vs No Traking
            /*
            await Traking();
            */

            // search

            Console.WriteLine("enter key to exit");
            Console.ReadKey();
        }

        static void IgnoreBlogsQueryFilters()
        {
            var blogs = context.Blogs
                .IgnoreQueryFilters()
                .ToList();

            blogs.ForEach(b => Console.WriteLine($"{b.Id} - {b.Title}"));
        }

        static async Task SafeDelete(int id)
        {
            ////Retrieve Record
            var blog = await context.Blogs
                .FindAsync(id);


            blog.Deleted = true;

            await context.SaveChangesAsync();
        }

        static void Join()
        {
            var query = from blog in context.Set<Blog>()
                        join post in context.Set<Post>()
                        on blog.Id equals post.BlogId
                        select new
                        {
                            id = post.Id,
                            blogUrl = blog.Url,
                            postTitle = post.Title,
                        };

            query.ToList().ForEach(p => Console.WriteLine(
                $"{p.id}-{p.postTitle}-{p.blogUrl}"
                ));
        }

        static void GroupJoin()
        {
            var query = from b in context.Set<Blog>()
                        join p in context.Set<Post>()
                            on b.Id equals p.BlogId into grouping
                        select new { b, grouping };
            var result = query.ToList();

            result.ForEach(p => Console.WriteLine(
               $"{p.b.Id}-{p.b.Url}-{p.grouping.Count()}"
               ));
        }

        static void GroupBy()
        {
            var query = from p in context.Set<Post>()
                        group p by p.BlogId
                        into g
                        select new { g.Key, Count = g.Count() };

            var result = query.ToList();

            result.ForEach(p => Console.WriteLine(
               $"{p.Key}- Count = {p.Count}"
               ));
        }

        static void SearchPostByToken(string token)
        {
            // String.Contains
            var res1 = context.Posts
                .Where(p => p.Title.Contains(token))
            .ToList();
            res1.ForEach(p => Console.WriteLine($"{p.Id}-{p.Title}"));

            // Custom function
            var res2 = context.Posts
                 .ToList()
                 .Where(p => CustomContains(token, p.Title));
            res2.ToList().ForEach(p => Console.WriteLine($"{p.Id}-{p.Title}"));

            // EF Functions
            var res3 = context.Posts
                .Where(p => EF.Functions.Like(p.Title, token))
                .ToList();
            res3.ForEach(p => Console.WriteLine($"{p.Id}-{p.Title}"));

            // SQL.Contains <> String.Contains
            var res4 = context.Posts
                .Where(p => EF.Functions.Contains(p.Title, token))
                .ToList();
            res4.ForEach(p => Console.WriteLine($"{p.Id}-{p.Title}"));
        }

        static async Task InsertMutiplePosts()
        {

            var posts = new List<Post>
            {
                new Post
                {
                    BlogId = 1,
                    Title = "EF CORE Model",
                    Content = "Lorem Ipsum"
                },
                new Post
               {
                     BlogId = 1,
                    Title = "EF CORE Migration",
                    Content = "Lorem Ipsum"
                },
                 new Post
               {
                      BlogId = 1,
                    Title = "EF CORE Log",
                    Content = "Lorem Ipsum"
                },
            };

            await context.AddRangeAsync(posts);

        }

        static async Task ElementFromDB()
        {
            var posts = context.Posts;
            var list = await posts.ToListAsync();
            var first = await posts.FirstAsync();
            var firstOrDefault = await posts.FirstOrDefaultAsync();
            // first = await posts.Where(b => b.Id == 500).FirstAsync();
            firstOrDefault = await posts.Where(b => b.Id == 500).FirstOrDefaultAsync();
            // var single = await posts.SingleAsync();
            // var singleOrDefault = await posts.SingleOrDefaultAsync();

            var count = await posts.CountAsync();
            var longCount = await posts.LongCountAsync();

            var post = await posts.FindAsync(1);
        }

        static async Task SimpleCreateBlogRecord()
        {
            var blog = new Blog() { Url = "Default" };
            await context.AddAsync(blog);
            await context.SaveChangesAsync();
        }

        static async Task SimpleUpdateBlogRecord()
        {
            // New Blog
            var blog = new Blog
            {
                Id = 1,
                Url = "https://visualstudio.microsoft.com/tuto55"
            };

            // Update
            context.Blogs.Update(blog);

            ///Save Changes
            await context.SaveChangesAsync();

            await DisplayBlog(1);
        }

        static void SimpleProjection()
        {
            var blogs = context.Blogs.Select(b => new
            {
                b.Id,
                ReviewCount = b.Posts.Count,
            })
                .OrderBy(x => x.ReviewCount)
                .ToList();
        }

        static void QuerySplitting()
        {
            Console.Write("WITHOUT QUERY SPLITTING");
            var blog = context.Blogs.Include(b => b.Posts).ToList();

            Console.Write("WITHOUT QUERY SPLITTING");
            blog = context.Blogs.Include(b => b.Posts).AsSplitQuery().ToList();
        }

        static async Task ExecuteSQLStoredProcedure()
        {
            var first = new SqlParameter("first", "john");
            var last = new SqlParameter("last", "doe");
            var val = context.Database.SqlQuery<string>($"EXEC dbo.getFullName @FirstName={first} , @LastName={last}");

            await val.ForEachAsync(v => Console.Write(v));
        }

        static void EagerLoading()
        {
            var blogs = context.Blogs
                .ToList();
            
            blogs.ForEach(b => Console.WriteLine($"{b.Id} - {b.Posts?.Count()}"));


            blogs = context.Blogs
                .Include(blog => blog.Posts)
                .ToList();
            blogs.ForEach(b => Console.WriteLine($"{b.Id} - {b.Posts?.Count()}"));
        }

        static async Task DisplayBlogPost(int id)
        {
            var blog = await context.Blogs.FindAsync(id);
            Console.WriteLine($"{blog.Id} - {blog.Title} - {blog.Url}");
        }

        static async Task DisplayBlog(int id)
        {
            var blog = await context.Blogs.FindAsync(id);
            Console.WriteLine($"{blog.Id} - {blog.Title} - {blog.Url}");
        }

        static private bool CustomContains(string token, string value)
        {
            return value.Contains(token);
        }

        static async Task Traking()
        {
            var blog = await context.Blogs.FindAsync(1);
            blog.Deleted = false;
            blog = context.Blogs.AsNoTracking().FirstOrDefault(b => b.Id == 1);
            await context.SaveChangesAsync();

            blog.Deleted = false;
            await context.SaveChangesAsync();
        }

    }
}
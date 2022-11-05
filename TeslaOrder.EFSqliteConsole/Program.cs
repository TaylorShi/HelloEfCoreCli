using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TeslaOrder.Domain.AggregatesModel;
using TeslaOrder.Infrastructure.Contexts;

namespace TeslaOrder.EFSqliteConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            var DbPath = System.IO.Path.Join(path, "EFSqliteConsole.db");

            var services = new ServiceCollection();
            services.AddDbContext<BloggingContext>(opt => opt.UseSqlite($"Data Source={DbPath}"));

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<BloggingContext>();
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var blog = new Blog
                {
                    BlogId = new Random(9999999).Next(),
                    Url = "https://www.cnblogs.com/taylorshi/p/16843914.html"
                };
                context.Add(blog);
                context.SaveChanges();

                var blogs = context.Blogs.ToList();
                if (blogs.Any())
                {

                }
            }

            Console.ReadKey();
        }
    }
}

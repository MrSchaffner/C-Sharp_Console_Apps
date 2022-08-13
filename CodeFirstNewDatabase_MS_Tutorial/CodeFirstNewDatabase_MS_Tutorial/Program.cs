using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstNewDatabase_MS_Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BlogContext()) //New One Each Time Run
            {
                Console.Write("Name the new Blog:");
                var blog = new Blog { Name = Console.ReadLine() };

                db.Blogs.Add(blog);
                db.SaveChanges();

                var query = from eachBlog in db.Blogs
                            orderby eachBlog.Name
                            select eachBlog;
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }

            }
            Console.WriteLine("\nProgram Over");
            Console.ReadLine();
        }// END MAIN


        
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public virtual List<Post> Posts { get; set; }
        public string URL { get; set; }
    }

    public class Post //pogo classes
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; } //lazy loading with virtual
    }

    public class User
    {
        [Key] //this data Annotation sets the following field as the primary key in the final database.
        public string UserName { get; set; } // primary key should be "UserId", so EF can find it
        public string DisplayName { get; set; }

    }

    public class BlogContext : DbContext { //All classes included in Model
    public DbSet<Blog> Blogs { get; set; } //allows us to query instances of these types
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.DisplayName)
                .HasColumnName("display_name");
            //base.OnModelCreating(modelBuilder);
        }

    }

}

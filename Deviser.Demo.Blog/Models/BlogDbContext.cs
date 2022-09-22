using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deviser.Core.Common;
using Deviser.Core.Common.DomainTypes;
using Deviser.Core.Data.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Deviser.Demo.Blog.Models
{
    public class BlogDbContext : ModuleDbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("demo_blog_Category");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Title).IsRequired();
                entity.Property(p => p.CreatedOn).IsRequired();
                
                entity.HasOne(p => p.Category).WithMany(c => c.Posts).HasForeignKey(p => p.CategoryId);
                entity.HasMany(p => p.Tags)
                    .WithMany(p => p.Posts)
                    .UsingEntity(j => j.ToTable("demo_blog_PostTag"));
                entity.ToTable("demo_blog_Post");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                entity.HasIndex(t => t.Name).IsUnique();
                entity.ToTable("demo_blog_Tag");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasOne(d => d.Post).WithMany(p => p.Comments).HasForeignKey(d => d.PostId).OnDelete(DeleteBehavior.Restrict);
                entity.ToTable("demo_blog_Comment");
            });
        }
        
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    }

    public class MySqlBlogDbContext : BlogDbContext
    {
        public MySqlBlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }
    }

    public class PostgreSqlBlogDbContext : BlogDbContext
    {
        public PostgreSqlBlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }
    }

    public class SqlLiteBlogDbContext : BlogDbContext
    {
        public SqlLiteBlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }
    }

    public class SqlServerBlogDbContext : BlogDbContext
    {
        public SqlServerBlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }
    }
    public class SqlLiteDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqlLiteBlogDbContext>
    {
        public SqlLiteBlogDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            var connectionString = configuration.GetConnectionString(Globals.ConnectionStringKeys[DatabaseProvider.SqlLite]);
            builder.UseSqlite(connectionString, b => b.MigrationsAssembly("Deviser.Demo.Blog"));
            return new SqlLiteBlogDbContext(builder.Options);
        }
    }

    public class SqlServerDbContextDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SqlServerBlogDbContext>
    {
        public SqlServerBlogDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            var connectionString = configuration.GetConnectionString(Globals.ConnectionStringKeys[DatabaseProvider.SqlServer]);
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Deviser.Demo.Blog"));
            return new SqlServerBlogDbContext(builder.Options);
        }
    }

    public class PostgreSqlDbContextDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostgreSqlBlogDbContext>
    {
        public PostgreSqlBlogDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            var connectionString = configuration.GetConnectionString(Globals.ConnectionStringKeys[DatabaseProvider.PostgreSql]);
            builder.UseNpgsql(connectionString, b => b.MigrationsAssembly("Deviser.Demo.Blog"));
            return new PostgreSqlBlogDbContext(builder.Options);
        }
    }

    public class MySqlDbContextContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MySqlBlogDbContext>
    {
        public MySqlBlogDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BlogDbContext>();
            var connectionString = configuration.GetConnectionString(Globals.ConnectionStringKeys[DatabaseProvider.MySql]);
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("Deviser.Demo.Blog"));
            return new MySqlBlogDbContext(builder.Options);
        }
    }
}

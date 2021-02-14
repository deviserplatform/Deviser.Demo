using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deviser.Admin;
using Deviser.Demo.Blog.Models;

namespace Deviser.Demo.Blog
{
    public class AdminConfigurator : IAdminConfigurator<BlogDbContext>
    {
        public void ConfigureAdmin(IAdminBuilder adminBuilder)
        {
            adminBuilder.MapperConfiguration = BlogMapper.MapperConfiguration;

            adminBuilder.Register<DTO.Post>(modelBuilder =>
            {
                modelBuilder.GridBuilder.Title = "Posts";
                modelBuilder.FormBuilder.Title = "Post";

                modelBuilder.GridBuilder
                    .AddField(p => p.Title)
                    .AddField(p => p.Category)
                    .AddField(p => p.Tags)
                    .AddField(p => p.CreatedOn, option => option.Format = "dd.MM.yyyy")
                    .AddField(p => p.CreatedBy, option => option.DisplayName = "Author");

                modelBuilder.FormBuilder
                    .AddKeyField(p => p.Id)
                    .AddField(p => p.Title)
                    .AddField(s => s.Summary)
                    .AddField(s => s.Thumbnail)
                    .AddField(s => s.Content)
                    .AddSelectField(s => s.Category, expr => expr.Name)
                    .AddInlineMultiSelectField<DTO.Tag>(s => s.Tags, expr => expr.Name)
                    .AddField(p => p.CreatedOn, option => option.Format = "dd.MM.yyyy");
                
                modelBuilder.FormBuilder
                    .Property(p => p.Tags)
                    .AddItemBy(t => t.Name);
                
                modelBuilder.AddChildConfig(s => s.Comments, (childForm) =>
                {
                    childForm.FormBuilder
                    .AddKeyField(c => c.Id)
                    .AddField(c => c.UserName)
                    .AddField(c => c.Comment)
                    .AddField(c => c.CreatedOn)
                    .AddField(c => c.IsApproved);
                });
            });

            adminBuilder.Register<DTO.Category>(modelBuilder =>
            {
                modelBuilder.GridBuilder
                    .AddField(p => p.Name);

                modelBuilder.FormBuilder
                    .AddKeyField(p => p.Id)
                    .AddField(p => p.Name);
            });

            adminBuilder.Register<DTO.Tag>(modelBuilder =>
            {
                modelBuilder.GridBuilder
                    .AddField(p => p.Name);

                modelBuilder.FormBuilder
                    .AddKeyField(p => p.Id)
                    .AddField(p => p.Name);
            });

        }
    }
}

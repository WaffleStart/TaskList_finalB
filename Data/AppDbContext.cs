using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskList.Models;
using Task = TaskList.Models.Task;

namespace TaskList.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TaskTag> TaskTags { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User { User_Id = 1, Username = "pete", Email = "pete@pete.com", Password = "peteadmin123", IsAdmin = true }); // manually seeded
            //hopefully works, but currently cant test.

            modelBuilder.Entity<TaskTag>()
                .HasKey(t => new { t.Task_Id, t.Tag_Id });

            modelBuilder.Entity<Task>()
                .HasOne(t => t.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.User_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskTag>()
                .HasOne(tt=>tt.Tag)
                .WithMany(tt=>tt.TaskTags)
                .HasForeignKey(tt=>tt.Tag_Id);

            modelBuilder.Entity<TaskTag>()
                .HasOne(tt=>tt.Task)
                .WithMany(tt=>tt.TaskTags)
                .HasForeignKey(tt=>tt.Task_Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Models;

namespace TaskFlow.API.Data
{
    public class TaskFlowDbContext : DbContext
    {
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Project>()
                .HasOne(p => p.OwnerUser)
                .WithMany(u =>  u.Projects)
                .HasForeignKey(p => p.OwnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

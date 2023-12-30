using Microsoft.EntityFrameworkCore;

namespace forum.Models
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<FollowedMessages> FollowedMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<User>()
                    .HasMany(u => u.Posts)
                    .WithMany(p => p.Users)
                    .UsingEntity(j => j.ToTable("UsersPosts"));

            modelBuilder.Entity<Theme>()
                .HasMany(t => t.Forums)
                .WithMany(f => f.Themes)
                .UsingEntity(j => j.ToTable("ThemesForums"));

            modelBuilder.Entity<Post>()
                .HasMany(p => p.ReferencingPosts)
                .WithMany(p => p.ReferencedPosts)
                .UsingEntity(j => j.ToTable("PostReferences"));

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Theme)
                .WithMany(t => t.Posts)
                .HasForeignKey(p => p.ThemeId);

            modelBuilder.Entity<FollowedMessages>()
                .HasOne(f => f.Post)
                .WithMany(p => p.FollowedMessages)
                .HasForeignKey(f => f.postId);

            modelBuilder.Entity<FollowedMessages>()
                .HasOne(f => f.User)
                .WithMany(u => u.FollowedMessages)
                .HasForeignKey(f => f.userId);


            modelBuilder.Entity<FollowedMessages>()
                .HasKey(fm => new { fm.postId, fm.userId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
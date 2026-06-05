using Microsoft.EntityFrameworkCore;
using NetflixApi.Modules.Auth.Models;
using Netflix.API.Modules.Content.Models;
using NetflixApi.Modules.Reviews.Models;
using NetflixApi.Modules.Streaming.Models;
using NetflixApi.Modules.Users.Models;
using NetflixApi.Modules.Watchlist.Models;
using NetflixApi.Modules.Payments.Models;
using NetflixApi.Modules.Subscription.Models;
using NetflixApi.Modules.Notifications.Models;
using NetflixApi.Modules.Analytics.Models;

namespace NetflixApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Auth
        public DbSet<EmailVerification> EmailVerifications { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        // Content
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Subtitle> Subtitles { get; set; }
        
        // Content Junction Tables
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        // Streaming
        public DbSet<Video> Videos { get; set; }
        public DbSet<StreamingSession> StreamingSessions { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<Download> Downloads { get; set; }

        // Watchlist
        public DbSet<WatchlistItem> WatchlistItems { get; set; }

        // Reviews
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        // Additional Modules
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AnalyticsEvent> Analytics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });
                
            modelBuilder.Entity<MovieDirector>()
                .HasKey(md => new { md.MovieId, md.DirectorId });
                
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });
                
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}

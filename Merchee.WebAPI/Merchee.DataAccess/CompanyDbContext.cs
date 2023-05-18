using Merchee.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Merchee.DataAccess
{
    public class CompanyDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<BaseEntity>();
            modelBuilder.Ignore<BaseCompanyEntity>();

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Users)
                    .WithOne()
                    .HasForeignKey(e => e.CompanyId);
                entity.HasMany(e => e.Products)
                    .WithOne()
                    .HasForeignKey(e => e.CompanyId);
                entity.HasMany(e => e.Shelves)
                    .WithOne()
                    .HasForeignKey(e => e.CompanyId);
            });

            modelBuilder.Entity<CustomerShelfAction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.ShelfProduct)
                    .WithMany()
                    .HasForeignKey(e => e.ShelfProductId);
            });

            modelBuilder.Entity<ShelfProduct>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductID);
                entity.HasOne(e => e.Shelf)
                    .WithMany()
                    .HasForeignKey(e => e.ShelfID);
            });

            modelBuilder.Entity<ExpirationWarning>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.ShelfItem)
                    .WithMany()
                    .HasForeignKey(e => e.ShelfItemId);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ReplenishmentRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.ShelfProduct)
                    .WithMany()
                    .HasForeignKey(e => e.ShelfProductId);
            });

            modelBuilder.Entity<Shelf>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ShelfItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.ShelfProduct)
                    .WithMany(e => e.Items)
                    .HasForeignKey(e => e.ShelfProductId);
            });

            modelBuilder.Entity<StockTransaction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.ShelfProduct)
                    .WithMany()
                    .HasForeignKey(e => e.ShelfProductId);
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId);
                entity.HasOne(e => e.Notification)
                    .WithMany()
                    .HasForeignKey(e => e.NotificationId);
            });

            // Restrict cascade delete
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}

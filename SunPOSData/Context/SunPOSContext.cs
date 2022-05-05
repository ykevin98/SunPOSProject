#region

using Microsoft.EntityFrameworkCore;
using SunPOSData.Models;
using Serilog;
using Microsoft.Extensions.Logging;
using SunPOSData.Common;

#endregion

namespace SunPOSData.Context
{
    public class SunPOSContext : DbContext
    {
        #region Constructor

        public SunPOSContext(DbContextOptions<SunPOSContext> options)
            : base(options)
        {
        }

        #endregion

        #region Public Members

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }

        #endregion

        #region Protected Methods

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddSerilog(); }));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => new { e.RestaurantID });
                entity.ToTable(Constants.TableName.Restaurants);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => new { e.CategoryID });
                entity.ToTable(Constants.TableName.Categories);
            });
            
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => new { e.MenuId });
                entity.Property(e => e.Description).HasColumnName("Descript");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.UserID });
                entity.ToTable(Constants.TableName.User);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.ItemId });
                entity.ToTable(Constants.TableName.ShoppingCart);
            });
        }

        #endregion
    }
}

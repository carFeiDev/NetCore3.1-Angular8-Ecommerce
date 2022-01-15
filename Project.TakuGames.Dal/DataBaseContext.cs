
using Microsoft.EntityFrameworkCore;
using Project.TakuGames.Model.Domain;

namespace Project.TakuGames.Dal
{
    public  partial class DataBaseContext : DbContext
    {
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItems> CardItems { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CustomerOrderDetails> CustomerOrderDetails { get; set; }
        public virtual DbSet<CustomerOrders> CustomerOrders { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<UserMaster> UserMaster { get; set; }
        public virtual DbSet<Favoritelist> Favoritelist { get; set; }
        public virtual DbSet<FavoritelistItems> FavoritelistItems { get; set; }


        public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameId)
                .HasColumnName("GameId");

                entity.Property(e => e.Description)
                .IsUnicode(false);

                entity.Property(e => e.Developer)
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.Publisher)
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.Platform)
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.CoverFileName)
                .IsRequired()
                .IsUnicode(false);

                entity.Property(e => e.Price)
                .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            });           

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId)
                .HasMaxLength(36)
                .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                .HasColumnName("UserId");

            });

            modelBuilder.Entity<CartItems>(entity =>
            {
                entity.HasKey(entity => entity.CartItemId)
                .HasName("PK_CarItem_488B0B0AA0297D1C");

                entity.Property(e => e.CartId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                .HasName("PK_Categori_19093A2B46B8DFC9");

                entity.Property(e => e.CategoryId)
                .HasColumnName("CategoryId");

                entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);               
            });


            modelBuilder.Entity<CustomerOrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId)
                .HasName("PK_Customer_9DD74DBD81D9221B");

                entity.Property(e => e.OrderId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false);

                entity.Property(e => e.Price)
                .HasColumnType("decimal(10,2)");

                entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10,2)");

                entity.Property(e => e.CoverFileName)
                .IsRequired()
                .IsUnicode(false);

            });

            modelBuilder.Entity<CustomerOrders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                .HasName("PK_Customer_C3905BCF96C8F1E7");

                entity.Property(e => e.OrderId)
                .HasMaxLength(36)
                .IsUnicode(false) ;

                entity.Property(e => e.CartTotal)
                .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.DateCreated)
                .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                .HasColumnName("UserId");

            });
            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(entity => entity.UserId)
                .HasName("PK_UserMast_1788CCAC2694A2ED");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false);

                entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.Property(e => e.UserTypeId)
                .HasColumnName("UserTypeId");

                entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.UserImage)
                .IsRequired()
                .IsUnicode(false);

            });

            modelBuilder.Entity<Favoritelist>(entity =>
            {
                entity.Property(e => e.FavoritelistId)
                .HasMaxLength(36)
                .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                .HasColumnName("UserID");
            });

            modelBuilder.Entity<FavoritelistItems>(entity =>
            {
                entity.HasKey(e => e.FavoritelistItemId)
                .HasName("PK__Wishlist__171E21A16A5148A4");

                entity.Property(e => e.FavoritelistId)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);     
            
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}


using Microsoft.EntityFrameworkCore;

namespace RogalskiJaroslaw
{
    public partial class OrdersContext : DbContext
    {
        public OrdersContext()
        {
        }

        public OrdersContext(DbContextOptions<OrdersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:rogala000.database.windows.net,1433;Initial Catalog=Orders;Persist Security Info=False;User ID=rogala000;Password=Fajnehaslo1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BCF4C1583DB");

                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.OrderComments).HasMaxLength(1000);

                entity.Property(e => e.OrderNumber).HasMaxLength(300);

                entity.Property(e => e.OrderOrigin).HasMaxLength(1000);
            });
        }
    }
}

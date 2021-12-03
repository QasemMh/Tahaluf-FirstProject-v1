using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Iam_Influencer.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Customeraddress> Customeraddresses { get; set; }
        public virtual DbSet<Customerpayment> Customerpayments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Orederdetail> Orederdetails { get; set; }
        public virtual DbSet<Orederitem> Orederitems { get; set; }
        public virtual DbSet<Paymentdetail> Paymentdetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shipping> Shippings { get; set; }
        public virtual DbSet<ShoppingSession> ShoppingSessions { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Stutascode> Stutascodes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TRAIN_USER144")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("CART_ITEM");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Createddate)
                    .HasPrecision(6)
                    .HasColumnName("CREATEDDATE");

                entity.Property(e => e.ProductId)
                    .HasPrecision(11)
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quntity)
                    .HasPrecision(5)
                    .HasColumnName("QUNTITY");

                entity.Property(e => e.ShoppingId)
                    .HasPrecision(11)
                    .HasColumnName("SHOPPING_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0079700");

                entity.HasOne(d => d.Shopping)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ShoppingId)
                    .HasConstraintName("SYS_C0079701");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMER");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.AddressId)
                    .HasPrecision(11)
                    .HasColumnName("ADDRESS_ID");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Midname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MIDNAME");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("SYS_FK_ADDRESS");
            });

            modelBuilder.Entity<Customeraddress>(entity =>
            {
                entity.ToTable("CUSTOMERADDRESS");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Countray)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRAY");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STREET");
            });

            modelBuilder.Entity<Customerpayment>(entity =>
            {
                entity.ToTable("CUSTOMERPAYMENT");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER(9,3)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CustomerId)
                    .HasPrecision(11)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.ExpierDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIER_DATE");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PAYMENT_TYPE");

                entity.Property(e => e.Provider)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROVIDER");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Customerpayments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0079751");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Midname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MIDNAME");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.HasIndex(e => e.AccountatntId, "SYS_C0079744")
                    .IsUnique();

                entity.HasIndex(e => e.CustomerId, "SYS_C0079745")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.AccountatntId)
                    .HasPrecision(11)
                    .HasColumnName("ACCOUNTATNT_ID");

                entity.Property(e => e.CustomerId)
                    .HasPrecision(11)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RoleId)
                    .HasPrecision(11)
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Accountatnt)
                    .WithOne(p => p.Login)
                    .HasForeignKey<Login>(d => d.AccountatntId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C0079746");

                entity.HasOne(d => d.Customer)
                    .WithOne(p => p.Login)
                    .HasForeignKey<Login>(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C0079747");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ROLE");
            });

            modelBuilder.Entity<Orederdetail>(entity =>
            {
                entity.ToTable("OREDERDETAILS");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Createddate)
                    .HasPrecision(6)
                    .HasColumnName("CREATEDDATE");

                entity.Property(e => e.CustomerId)
                    .HasPrecision(11)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.PaymentdetailsId)
                    .HasPrecision(11)
                    .HasColumnName("PAYMENTDETAILS_ID");

                entity.Property(e => e.ShippingId)
                    .HasPrecision(11)
                    .HasColumnName("SHIPPING_ID");

                entity.Property(e => e.StutascodeId)
                    .HasPrecision(11)
                    .HasColumnName("STUTASCODE_ID");

                entity.Property(e => e.Total)
                    .HasColumnType("NUMBER(7,3)")
                    .HasColumnName("TOTAL");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orederdetails)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0097405");

                entity.HasOne(d => d.Paymentdetails)
                    .WithMany(p => p.Orederdetails)
                    .HasForeignKey(d => d.PaymentdetailsId)
                    .HasConstraintName("SYS_C0079719");

                entity.HasOne(d => d.Shipping)
                    .WithMany(p => p.Orederdetails)
                    .HasForeignKey(d => d.ShippingId)
                    .HasConstraintName("SYS_C0079720");

                entity.HasOne(d => d.Stutascode)
                    .WithMany(p => p.Orederdetails)
                    .HasForeignKey(d => d.StutascodeId)
                    .HasConstraintName("SYS_C0079718");
            });

            modelBuilder.Entity<Orederitem>(entity =>
            {
                entity.ToTable("OREDERITEM");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Createddate)
                    .HasPrecision(6)
                    .HasColumnName("CREATEDDATE");

                entity.Property(e => e.OrederdetailsId)
                    .HasPrecision(11)
                    .HasColumnName("OREDERDETAILS_ID");

                entity.Property(e => e.ProductId)
                    .HasPrecision(11)
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quntity)
                    .HasPrecision(5)
                    .HasColumnName("QUNTITY");

                entity.HasOne(d => d.Orederdetails)
                    .WithMany(p => p.Orederitems)
                    .HasForeignKey(d => d.OrederdetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0079728");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orederitems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0079727");
            });

            modelBuilder.Entity<Paymentdetail>(entity =>
            {
                entity.ToTable("PAYMENTDETAILS");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER(7,3)")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Createddate)
                    .HasPrecision(6)
                    .HasColumnName("CREATEDDATE");

                entity.Property(e => e.ExpierDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIER_DATE");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PAYMENT_TYPE");

                entity.Property(e => e.Provider)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROVIDER");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId)
                    .HasPrecision(11)
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CustomerId)
                    .HasPrecision(11)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER(7,3)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Sale)
                    .HasPrecision(11)
                    .HasColumnName("SALE");

                entity.Property("Description")
                        .HasMaxLength(250)
                        .HasColumnName("DESCRIPTION");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("SYS_C0079683");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0079684");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("REVIEWS");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Starcount)
                    .HasPrecision(1)
                    .HasColumnName("STARCOUNT");

                entity.Property(e => e.Text)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLES");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("SHIPPING");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Charge)
                    .HasColumnType("NUMBER(7,3)")
                    .HasColumnName("CHARGE");

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("METHOD");
            });

            modelBuilder.Entity<ShoppingSession>(entity =>
            {
                entity.ToTable("SHOPPING_SESSION");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Createddate)
                    .HasPrecision(6)
                    .HasColumnName("CREATEDDATE");

                entity.Property(e => e.CustomerId)
                    .HasPrecision(11)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Total)
                    .HasColumnType("NUMBER(7,3)")
                    .HasColumnName("TOTAL");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShoppingSessions)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0079690");
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.ToTable("SLIDER");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Imagepath)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Text)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");
            });

            modelBuilder.Entity<Stutascode>(entity =>
            {
                entity.ToTable("STUTASCODE");

                entity.Property(e => e.Id)
                    .HasPrecision(11)
                    .HasColumnName("ID");

                entity.Property(e => e.Stutas)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STUTAS");
            });

            modelBuilder.HasSequence("DEPTID_ADD").IncrementsBy(10);

            modelBuilder.HasSequence("DEPTID_SEC").IncrementsBy(2);

            modelBuilder.HasSequence("TEST_TRG_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

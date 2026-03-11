using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mbrand> Mbrands { get; set; }

    public virtual DbSet<Mcategory> Mcategories { get; set; }

    public virtual DbSet<Mdispute> Mdisputes { get; set; }

    public virtual DbSet<MdisputeEvidence> MdisputeEvidences { get; set; }

    public virtual DbSet<Minspectionreport> Minspectionreports { get; set; }

    public virtual DbSet<Mlisting> Mlistings { get; set; }

    public virtual DbSet<Mmessage> Mmessages { get; set; }

    public virtual DbSet<Morder> Morders { get; set; }

    public virtual DbSet<Morderdetail> Morderdetails { get; set; }

    public virtual DbSet<Mpayment> Mpayments { get; set; }

    public virtual DbSet<Mproduct> Mproducts { get; set; }

    public virtual DbSet<Mrole> Mroles { get; set; }

    public virtual DbSet<Muser> Musers { get; set; }

    public virtual DbSet<Mwishlist> Mwishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-d619rrsoud1c739vkk20-a.singapore-postgres.render.com;Database=sportbicyclestore_db;Username=admin;Password=9JIgpwTbCTtygGd4HydNmDldqDcyDeWc");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mbrand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("mbrand_pkey");

            entity.ToTable("mbrand");

            entity.HasIndex(e => e.BrandName, "mbrand_brand_name_key").IsUnique();

            entity.Property(e => e.BrandId)
                .HasMaxLength(40)
                .HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(100)
                .HasColumnName("brand_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Mcategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("mcategory_pkey");

            entity.ToTable("mcategory");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(40)
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Mdispute>(entity =>
        {
            entity.HasKey(e => e.DisputeId).HasName("mdispute_pkey");

            entity.ToTable("mdispute");

            entity.Property(e => e.DisputeId)
                .HasMaxLength(40)
                .HasColumnName("dispute_id");
            entity.Property(e => e.AdminNote).HasColumnName("admin_note");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(40)
                .HasColumnName("buyer_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EvidenceUrls).HasColumnName("evidence_urls");
            entity.Property(e => e.OrderId)
                .HasMaxLength(40)
                .HasColumnName("order_id");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.RefundAmount)
                .HasPrecision(18, 2)
                .HasColumnName("refund_amount");
            entity.Property(e => e.Resolution).HasColumnName("resolution");
            entity.Property(e => e.ResolvedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("resolved_at");
            entity.Property(e => e.ResolvedBy)
                .HasMaxLength(40)
                .HasColumnName("resolved_by");
            entity.Property(e => e.SellerId)
                .HasMaxLength(40)
                .HasColumnName("seller_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Buyer).WithMany(p => p.MdisputeBuyers)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dispute_buyer");

            entity.HasOne(d => d.Order).WithMany(p => p.Mdisputes)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dispute_order");

            entity.HasOne(d => d.ResolvedByNavigation).WithMany(p => p.MdisputeResolvedByNavigations)
                .HasForeignKey(d => d.ResolvedBy)
                .HasConstraintName("fk_dispute_resolver");

            entity.HasOne(d => d.Seller).WithMany(p => p.MdisputeSellers)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_dispute_seller");
        });

        modelBuilder.Entity<MdisputeEvidence>(entity =>
        {
            entity.HasKey(e => e.EvidenceId).HasName("mdispute_evidence_pkey");

            entity.ToTable("mdispute_evidence");

            entity.Property(e => e.EvidenceId)
                .HasMaxLength(40)
                .HasColumnName("evidence_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisputeId)
                .HasMaxLength(40)
                .HasColumnName("dispute_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.SubmittedBy)
                .HasMaxLength(40)
                .HasColumnName("submitted_by");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(500)
                .HasColumnName("video_url");

            entity.HasOne(d => d.Dispute).WithMany(p => p.MdisputeEvidences)
                .HasForeignKey(d => d.DisputeId)
                .HasConstraintName("fk_evidence_dispute");

            entity.HasOne(d => d.SubmittedByNavigation).WithMany(p => p.MdisputeEvidences)
                .HasForeignKey(d => d.SubmittedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evidence_submitter");
        });

        modelBuilder.Entity<Minspectionreport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("minspectionreport_pkey");

            entity.ToTable("minspectionreport");

            entity.Property(e => e.ReportId)
                .HasMaxLength(40)
                .HasColumnName("report_id");
            entity.Property(e => e.BrakeCondition).HasColumnName("brake_condition");
            entity.Property(e => e.BrakeNotes).HasColumnName("brake_notes");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DrivetrainCondition).HasColumnName("drivetrain_condition");
            entity.Property(e => e.DrivetrainNotes).HasColumnName("drivetrain_notes");
            entity.Property(e => e.FrameCondition).HasColumnName("frame_condition");
            entity.Property(e => e.FrameNotes).HasColumnName("frame_notes");
            entity.Property(e => e.ImagesUrl).HasColumnName("images_url");
            entity.Property(e => e.InspectionDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inspection_date");
            entity.Property(e => e.InspectorId)
                .HasMaxLength(40)
                .HasColumnName("inspector_id");
            entity.Property(e => e.OverallRating)
                .HasMaxLength(10)
                .HasColumnName("overall_rating");
            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.WheelCondition).HasColumnName("wheel_condition");
            entity.Property(e => e.WheelNotes).HasColumnName("wheel_notes");

            entity.HasOne(d => d.Inspector).WithMany(p => p.Minspectionreports)
                .HasForeignKey(d => d.InspectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inspectionreport_inspector");

            entity.HasOne(d => d.Product).WithMany(p => p.Minspectionreports)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_inspectionreport_product");
        });

        modelBuilder.Entity<Mlisting>(entity =>
        {
            entity.HasKey(e => e.ListingId).HasName("mlisting_pkey");

            entity.ToTable("mlisting");

            entity.Property(e => e.ListingId)
                .HasMaxLength(40)
                .HasColumnName("listing_id");
            entity.Property(e => e.ApprovedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("approved_at");
            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(40)
                .HasColumnName("approved_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiredAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expired_at");
            entity.Property(e => e.FeaturedImage)
                .HasMaxLength(500)
                .HasColumnName("featured_image");
            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.SellerId)
                .HasMaxLength(40)
                .HasColumnName("seller_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.MlistingApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("fk_listing_approver");

            entity.HasOne(d => d.Product).WithMany(p => p.Mlistings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_listing_product");

            entity.HasOne(d => d.Seller).WithMany(p => p.MlistingSellers)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_listing_seller");
        });

        modelBuilder.Entity<Mmessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("mmessage_pkey");

            entity.ToTable("mmessage");

            entity.HasIndex(e => e.CreatedAt, "idx_message_created_at");

            entity.HasIndex(e => e.ReceiverId, "idx_message_receiver");

            entity.HasIndex(e => e.SenderId, "idx_message_sender");

            entity.Property(e => e.MessageId)
                .HasMaxLength(40)
                .HasColumnName("message_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.ReceiverId)
                .HasMaxLength(40)
                .HasColumnName("receiver_id");
            entity.Property(e => e.SenderId)
                .HasMaxLength(40)
                .HasColumnName("sender_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Morder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("morder_pkey");

            entity.ToTable("morder");

            entity.Property(e => e.OrderId)
                .HasMaxLength(40)
                .HasColumnName("order_id");
            entity.Property(e => e.BuyerId)
                .HasMaxLength(40)
                .HasColumnName("buyer_id");
            entity.Property(e => e.CancelledAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancelled_at");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.ConfirmedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("confirmed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeliveryMethod).HasColumnName("delivery_method");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.ReceiverName)
                .HasMaxLength(255)
                .HasColumnName("receiver_name");
            entity.Property(e => e.ReceiverPhone)
                .HasMaxLength(20)
                .HasColumnName("receiver_phone");
            entity.Property(e => e.SellerId)
                .HasMaxLength(40)
                .HasColumnName("seller_id");
            entity.Property(e => e.ShippingAddress)
                .HasMaxLength(500)
                .HasColumnName("shipping_address");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(18, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Buyer).WithMany(p => p.MorderBuyers)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_buyer");

            entity.HasOne(d => d.Seller).WithMany(p => p.MorderSellers)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_seller");
        });

        modelBuilder.Entity<Morderdetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("morderdetail_pkey");

            entity.ToTable("morderdetail");

            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(40)
                .HasColumnName("order_detail_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.OrderId)
                .HasMaxLength(40)
                .HasColumnName("order_id");
            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.Subtotal)
                .HasPrecision(18, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(18, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Order).WithMany(p => p.Morderdetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_orderdetail_order");

            entity.HasOne(d => d.Product).WithMany(p => p.Morderdetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderdetail_product");
        });

        modelBuilder.Entity<Mpayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("mpayment_pkey");

            entity.ToTable("mpayment");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(40)
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasPrecision(18, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.OrderId)
                .HasMaxLength(40)
                .HasColumnName("order_id");
            entity.Property(e => e.PaidAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paid_at");
            entity.Property(e => e.PaymentGateway)
                .HasMaxLength(50)
                .HasColumnName("payment_gateway");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.PaymentType).HasColumnName("payment_type");
            entity.Property(e => e.RefundedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("refunded_at");
            entity.Property(e => e.TransactionCode)
                .HasMaxLength(100)
                .HasColumnName("transaction_code");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Order).WithMany(p => p.Mpayments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_payment_order");
        });

        modelBuilder.Entity<Mproduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("mproduct_pkey");

            entity.ToTable("mproduct");

            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.BrakeType)
                .HasMaxLength(50)
                .HasColumnName("brake_type");
            entity.Property(e => e.BrandId)
                .HasMaxLength(40)
                .HasColumnName("brand_id");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(40)
                .HasColumnName("category_id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Condition).HasColumnName("condition");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeleteFlag).HasColumnName("delete_flag");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FrameMaterial).HasColumnName("frame_material");
            entity.Property(e => e.FrameSize)
                .HasMaxLength(20)
                .HasColumnName("frame_size");
            entity.Property(e => e.GearSystem)
                .HasMaxLength(100)
                .HasColumnName("gear_system");
            entity.Property(e => e.InspectionStatus).HasColumnName("inspection_status");
            entity.Property(e => e.LocationCity)
                .HasMaxLength(100)
                .HasColumnName("location_city");
            entity.Property(e => e.Price)
                .HasPrecision(18, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("published_at");
            entity.Property(e => e.SellerId)
                .HasMaxLength(40)
                .HasColumnName("seller_id");
            entity.Property(e => e.SoldAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sold_at");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StockQuantity)
                .HasDefaultValue(1)
                .HasColumnName("stock_quantity");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsageHistory)
                .HasPrecision(10, 2)
                .HasColumnName("usage_history");
            entity.Property(e => e.ViewCount)
                .HasDefaultValue(0)
                .HasColumnName("view_count");
            entity.Property(e => e.Weight)
                .HasPrecision(5, 2)
                .HasColumnName("weight");
            entity.Property(e => e.WheelSize)
                .HasMaxLength(20)
                .HasColumnName("wheel_size");
            entity.Property(e => e.YearOfManufacture).HasColumnName("year_of_manufacture");

            entity.HasOne(d => d.Brand).WithMany(p => p.Mproducts)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Mproducts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_category");

            entity.HasOne(d => d.Seller).WithMany(p => p.Mproducts)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_seller");
        });

        modelBuilder.Entity<Mrole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("mrole_pkey");

            entity.ToTable("mrole");

            entity.HasIndex(e => e.RoleName, "mrole_role_name_key").IsUnique();

            entity.Property(e => e.RoleId)
                .HasMaxLength(40)
                .HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Muser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("muser_pkey");

            entity.ToTable("muser");

            entity.HasIndex(e => e.Email, "muser_email_key").IsUnique();

            entity.HasIndex(e => e.UserName, "muser_user_name_key").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(40)
                .HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(500)
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId)
                .HasMaxLength(40)
                .HasColumnName("role_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Role).WithMany(p => p.Musers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role");
        });

        modelBuilder.Entity<Mwishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("mwishlist_pkey");

            entity.ToTable("mwishlist");

            entity.HasIndex(e => new { e.UserId, e.ProductId }, "uq_wishlist_user_product").IsUnique();

            entity.Property(e => e.WishlistId)
                .HasMaxLength(40)
                .HasColumnName("wishlist_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeleteFlag).HasColumnName("delete_flag");
            entity.Property(e => e.ListingId)
                .HasMaxLength(40)
                .HasColumnName("listing_id");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.ProductId)
                .HasMaxLength(40)
                .HasColumnName("product_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(40)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Listing).WithMany(p => p.Mwishlists)
                .HasForeignKey(d => d.ListingId)
                .HasConstraintName("fk_wishlist_listing");

            entity.HasOne(d => d.Product).WithMany(p => p.Mwishlists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_wishlist_product");

            entity.HasOne(d => d.User).WithMany(p => p.Mwishlists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_wishlist_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

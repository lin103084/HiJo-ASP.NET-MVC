using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prjHiJoMVCCore.Models;

public partial class PrjFriendShipContext : DbContext
{
    public PrjFriendShipContext()
    {
    }

    public PrjFriendShipContext(DbContextOptions<PrjFriendShipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<BatchDiscount> BatchDiscounts { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventPhoto> EventPhotos { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Interest> Interests { get; set; }

    public virtual DbSet<InterestCategory> InterestCategories { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<MatchType> MatchTypes { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberInterest> MemberInterests { get; set; }

    public virtual DbSet<MemberPreference> MemberPreferences { get; set; }

    public virtual DbSet<MemberRole> MemberRoles { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatType> StatTypes { get; set; }

    public virtual DbSet<Swipe> Swipes { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    public virtual DbSet<VerificationMethod> VerificationMethods { get; set; }

    public virtual DbSet<VipMember> VipMembers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Batch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK_商品品項");

            entity.Property(e => e.BatchId).HasColumnName("batchID");
            entity.Property(e => e.EventEndDate)
                .HasColumnType("datetime")
                .HasColumnName("eventEndDate");
            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.EventStartDate)
                .HasColumnType("datetime")
                .HasColumnName("eventStartDate");
            entity.Property(e => e.OriginalPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("originalPrice");
            entity.Property(e => e.Quota).HasColumnName("quota");
            entity.Property(e => e.RegistrationEndDate)
                .HasColumnType("datetime")
                .HasColumnName("registrationEndDate");
            entity.Property(e => e.RegistrationStartDate)
                .HasColumnType("datetime")
                .HasColumnName("registrationStartDate");

            entity.HasOne(d => d.Event).WithMany(p => p.Batches)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_梯次_活動");
        });

        modelBuilder.Entity<BatchDiscount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BatchDis__3214EC2715382F4A");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BatchId).HasColumnName("batchID");
            entity.Property(e => e.DiscountId).HasColumnName("discountID");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchDiscounts)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BatchDiscounts_Batches");

            entity.HasOne(d => d.Discount).WithMany(p => p.BatchDiscounts)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BatchDiscounts_Discounts");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chatMess__3214EC27F3CAC338");

            entity.ToTable("chatMessages");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreaTime)
                .HasColumnType("datetime")
                .HasColumnName("creaTime");
            entity.Property(e => e.IsRead).HasColumnName("isRead");
            entity.Property(e => e.MesImage).HasColumnName("mesImage");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.MessageType)
                .HasMaxLength(20)
                .HasColumnName("messageType");
            entity.Property(e => e.ReceiverId).HasColumnName("receiverID");
            entity.Property(e => e.SenderId).HasColumnName("senderID");
            entity.Property(e => e.Source).HasColumnName("source");

            entity.HasOne(d => d.Receiver).WithMany(p => p.ChatMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chatMessages_members1");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chatMessages_members");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__cities__B4BEB8BE3BC35986");

            entity.ToTable("cities");

            entity.Property(e => e.CityId).HasColumnName("cityID");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .HasColumnName("cityName");
            entity.Property(e => e.LocationLevel)
                .HasDefaultValue((byte)1)
                .HasColumnName("locationLevel");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__D2130A06F31ADED7");

            entity.Property(e => e.DiscountId).HasColumnName("discountID");
            entity.Property(e => e.DiscountInfo)
                .HasMaxLength(100)
                .HasColumnName("discountInfo");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(10)
                .HasColumnName("discountType");
            entity.Property(e => e.DiscountValue)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("discountValue");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK__district__2BAEF7705DC4F504");

            entity.ToTable("districts");

            entity.Property(e => e.DistrictId).HasColumnName("districtID");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .HasColumnName("districtName");
            entity.Property(e => e.LocationLevel)
                .HasDefaultValue((byte)2)
                .HasColumnName("locationLevel");
            entity.Property(e => e.ParentCityId).HasColumnName("parentCityID");

            entity.HasOne(d => d.ParentCity).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ParentCityId)
                .HasConstraintName("FK__districts__paren__3B75D760");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK_活動");

            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.CityId).HasColumnName("cityID");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeID");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.City).WithMany(p => p.Events)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_cities");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_活動_活動類型");
        });

        modelBuilder.Entity<EventPhoto>(entity =>
        {
            entity.HasKey(e => e.EventPhotoId).HasName("PK_活動照片");

            entity.HasIndex(e => new { e.EventId, e.SortOrder }, "UQ_活動圖片_活動編號加順序唯一").IsUnique();

            entity.Property(e => e.EventPhotoId).HasColumnName("eventPhotoID");
            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.IsCover).HasColumnName("isCover");
            entity.Property(e => e.Photo)
                .HasMaxLength(100)
                .HasColumnName("photo");
            entity.Property(e => e.SortOrder).HasColumnName("sortOrder");

            entity.HasOne(d => d.Event).WithMany(p => p.EventPhotos)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_活動照片_活動");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK_活動類型");

            entity.Property(e => e.EventTypeId).HasColumnName("eventTypeID");
            entity.Property(e => e.EventType1)
                .HasMaxLength(10)
                .HasColumnName("eventType");
        });

        modelBuilder.Entity<Interest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__interest__3214EC27B508B050");

            entity.ToTable("interests");

            entity.HasIndex(e => e.Name, "UQ__interest__72E12F1B4FC3C9FA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Interests)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Interests_Category");
        });

        modelBuilder.Entity<InterestCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__interest__3214EC270D871FD8");

            entity.ToTable("interestCategories");

            entity.HasIndex(e => e.Name, "UQ__interest__72E12F1B0E6F4DCF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__matches__3214EC27A4FA7BC7");

            entity.ToTable("matches");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MatchedAt)
                .HasColumnType("datetime")
                .HasColumnName("matchedAt");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId1).HasColumnName("userID1");
            entity.Property(e => e.UserId2).HasColumnName("userID2");

            entity.HasOne(d => d.UserId1Navigation).WithMany(p => p.MatchUserId1Navigations)
                .HasForeignKey(d => d.UserId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_matches_members");

            entity.HasOne(d => d.UserId2Navigation).WithMany(p => p.MatchUserId2Navigations)
                .HasForeignKey(d => d.UserId2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_matches_members1");
        });

        modelBuilder.Entity<MatchType>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("PK__matchTyp__004C68C3D69F644D");

            entity.ToTable("matchType");

            entity.Property(e => e.ActionId).HasColumnName("actionID");
            entity.Property(e => e.ActionName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("actionName");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__members__3214EC27EEC7BB2E");

            entity.ToTable("members");

            entity.HasIndex(e => e.UserName, "UQ__members__66DCF95C019EE2CC").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__members__AB6E6164FAA4CD8A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AvatarPath)
                .HasMaxLength(255)
                .HasColumnName("avatarPath");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.CityId).HasColumnName("cityID");
            entity.Property(e => e.DistrictId).HasColumnName("districtID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.IsVerified)
                .HasDefaultValue(false)
                .HasColumnName("isVerified");
            entity.Property(e => e.LastOnline)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("lastOnline");
            entity.Property(e => e.PassWordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passWordHash");
            entity.Property(e => e.PassWordSalt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passWordSalt");
            entity.Property(e => e.Resume).HasColumnName("resume");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sex");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userName");

            entity.HasOne(d => d.City).WithMany(p => p.Members)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Members_City");

            entity.HasOne(d => d.District).WithMany(p => p.Members)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__members__distric__440B1D61");
        });

        modelBuilder.Entity<MemberInterest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__memberIn__3214EC2758E9C988");

            entity.ToTable("memberInterests");

            entity.HasIndex(e => new { e.MemberId, e.InterestId }, "unique_member_interest").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InterestId).HasColumnName("interestID");
            entity.Property(e => e.MemberId).HasColumnName("memberID");

            entity.HasOne(d => d.Interest).WithMany(p => p.MemberInterests)
                .HasForeignKey(d => d.InterestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__memberInt__inter__68487DD7");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberInterests)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__memberInt__membe__6754599E");
        });

        modelBuilder.Entity<MemberPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__memberPr__3214EC27ED9BB89D");

            entity.ToTable("memberPreferences");

            entity.HasIndex(e => e.MemberId, "UQ__memberPr__7FD7CFF73D2FBC18").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgeRangeMax).HasColumnName("ageRangeMax");
            entity.Property(e => e.AgeRangeMin).HasColumnName("ageRangeMin");
            entity.Property(e => e.LookingForSex)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("lookingForSex");
            entity.Property(e => e.MemberId).HasColumnName("memberID");

            entity.HasOne(d => d.Member).WithOne(p => p.MemberPreference)
                .HasForeignKey<MemberPreference>(d => d.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__memberPre__membe__5BE2A6F2");
        });

        modelBuilder.Entity<MemberRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__memberRo__3214EC2748E77349");

            entity.ToTable("memberRoles");

            entity.HasIndex(e => new { e.MemberId, e.RoleId }, "UX_memberRoles_MemberIdRoleId").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.RoleId).HasColumnName("roleID");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberRoles)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__memberRol__membe__5070F446");

            entity.HasOne(d => d.Role).WithMany(p => p.MemberRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__memberRol__roleI__5165187F");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_訂單");

            entity.Property(e => e.OrderId).HasColumnName("orderID");
            entity.Property(e => e.BatchId).HasColumnName("batchID");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("orderNumber");
            entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusID");
            entity.Property(e => e.PaymentTypeId).HasColumnName("paymentTypeID");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("totalPrice");

            entity.HasOne(d => d.Batch).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Batches");

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_members");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_訂單_訂單狀態");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_訂單_付款方式");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("PK_訂單狀態");

            entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusID");
            entity.Property(e => e.OrderStatus1)
                .HasMaxLength(10)
                .HasColumnName("orderStatus");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeId).HasName("PK_付款方式");

            entity.Property(e => e.PaymentTypeId).HasColumnName("paymentTypeID");
            entity.Property(e => e.PaymentType1)
                .HasMaxLength(10)
                .HasColumnName("paymentType");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permissi__3214EC2701CF5965");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Name, "UQ__permissi__72E12F1B0A31C08D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3214EC278BB2D648");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "UQ__roles__72E12F1B8204D091").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK__rolePermi__permi__4D94879B"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__rolePermi__roleI__4CA06362"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__rolePerm__101A551D40EF2585");
                        j.ToTable("rolePermissions");
                        j.IndexerProperty<int>("RoleId").HasColumnName("roleID");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("permissionID");
                    });
        });

        modelBuilder.Entity<StatType>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__statType__A869F58B6EB52D56");

            entity.ToTable("statType");

            entity.Property(e => e.StatId).HasColumnName("statID");
            entity.Property(e => e.StatName)
                .HasMaxLength(20)
                .HasColumnName("statName");
        });

        modelBuilder.Entity<Swipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__swipes__3214EC278865642C");

            entity.ToTable("swipes");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreaTime)
                .HasColumnType("datetime")
                .HasColumnName("creaTime");
            entity.Property(e => e.SwiperId).HasColumnName("swiperID");
            entity.Property(e => e.SwipesAction).HasColumnName("swipesAction");
            entity.Property(e => e.TargetId).HasColumnName("targetID");

            entity.HasOne(d => d.Swiper).WithMany(p => p.SwipeSwipers)
                .HasForeignKey(d => d.SwiperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_swipes_members");

            entity.HasOne(d => d.Target).WithMany(p => p.SwipeTargets)
                .HasForeignKey(d => d.TargetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_swipes_members1");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__verifica__3214EC277F9E4366");

            entity.ToTable("verifications");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdTime");
            entity.Property(e => e.ExpiresTime)
                .HasColumnType("datetime")
                .HasColumnName("expiresTime");
            entity.Property(e => e.IsVerified)
                .HasDefaultValue(false)
                .HasColumnName("isVerified");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.MethodId).HasColumnName("methodID");
            entity.Property(e => e.VerificationCode)
                .HasMaxLength(50)
                .HasColumnName("verificationCode");

            entity.HasOne(d => d.Member).WithMany(p => p.Verifications)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__verificat__membe__681373AD");

            entity.HasOne(d => d.Method).WithMany(p => p.Verifications)
                .HasForeignKey(d => d.MethodId)
                .HasConstraintName("FK__verificat__metho__690797E6");
        });

        modelBuilder.Entity<VerificationMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__verifica__3214EC270AA7582B");

            entity.ToTable("verificationMethods");

            entity.HasIndex(e => e.MethodName, "UQ__verifica__DF66BADD03BC2D76").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.MethodName)
                .HasMaxLength(50)
                .HasColumnName("methodName");
        });

        modelBuilder.Entity<VipMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vipMembe__3214EC271A04FCB3");

            entity.ToTable("vipMembers");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("createdAt");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.MemberId).HasColumnName("memberID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Active")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("updatedAt");
            entity.Property(e => e.VipLevel).HasColumnName("vipLevel");

            entity.HasOne(d => d.Member).WithMany(p => p.VipMembers)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__vipMember__membe__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

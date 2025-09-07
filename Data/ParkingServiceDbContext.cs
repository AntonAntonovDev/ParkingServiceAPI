using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ParkingServiceApi.Data.Models;

namespace ParkingServiceApi.Data;

public partial class ParkingServiceDbContext : DbContext
{
    public ParkingServiceDbContext(DbContextOptions<ParkingServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<ParkingLot> ParkingLots { get; set; }

    public virtual DbSet<ParkingSpot> ParkingSpots { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Street> Streets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserEmail> UserEmails { get; set; }

    public virtual DbSet<UserPhone> UserPhones { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("ADDRESS");

            entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");
            entity.Property(e => e.HouseNumber)
                .HasMaxLength(20)
                .HasColumnName("HOUSE_NUMBER");
            entity.Property(e => e.StreetId).HasColumnName("STREET_ID");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .HasColumnName("ZIP_CODE");

            entity.HasOne(d => d.Street).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StreetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ADDRESS_STREET");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("CITY");

            entity.Property(e => e.CityId).HasColumnName("CITY_ID");
            entity.Property(e => e.CountryId).HasColumnName("COUNTRY_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CITY_COUNTRY");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("COUNTRY");

            entity.HasIndex(e => e.Name, "UNQ_COUNTRY_NAME").IsUnique();

            entity.Property(e => e.CountryId).HasColumnName("COUNTRY_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<ParkingLot>(entity =>
        {
            entity.ToTable("PARKING_LOT");

            entity.HasIndex(e => new { e.AddressId, e.Name }, "UNQ_PARKING_LOT_ADDRESS_ID_NAME").IsUnique();

            entity.Property(e => e.ParkingLotId).HasColumnName("PARKING_LOT_ID");
            entity.Property(e => e.AddressId).HasColumnName("ADDRESS_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.TotalSpots).HasColumnName("TOTAL_SPOTS");

            entity.HasOne(d => d.Address).WithMany(p => p.ParkingLots)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PARKING_LOT_ADDRESS");
        });

        modelBuilder.Entity<ParkingSpot>(entity =>
        {
            entity.ToTable("PARKING_SPOT");

            entity.HasIndex(e => new { e.ParkingLotId, e.Number }, "UNQ_PARKING_SPOT_PARKING_LOT_ID_NUMBER").IsUnique();

            entity.Property(e => e.ParkingSpotId).HasColumnName("PARKING_SPOT_ID");
            entity.Property(e => e.IsOccupied).HasColumnName("IS_OCCUPIED");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .HasColumnName("NUMBER");
            entity.Property(e => e.ParkingLotId).HasColumnName("PARKING_LOT_ID");

            entity.HasOne(d => d.ParkingLot).WithMany(p => p.ParkingSpots)
                .HasForeignKey(d => d.ParkingLotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PARKING_SPOT_PARKING_LOT");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("RESERVATION");

            entity.Property(e => e.ReservationId).HasColumnName("RESERVATION_ID");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("END_TIME");
            entity.Property(e => e.ParkingSpotId).HasColumnName("PARKING_SPOT_ID");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("START_TIME");
            entity.Property(e => e.StatusId)
                .HasDefaultValue(1)
                .HasColumnName("STATUS_ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.VehicleId).HasColumnName("VEHICLE_ID");

            entity.HasOne(d => d.ParkingSpot).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ParkingSpotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESERVATION_PARKING_SPOT");

            entity.HasOne(d => d.Status).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_RESERVATION_RESERVATION_STATUS");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESERVATION_USER");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESERVATION_VEHICLE");
        });

        modelBuilder.Entity<ReservationStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("RESERVATION_STATUS");

            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("ROLE");

            entity.HasIndex(e => e.Name, "UNQ_ROLE").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.ToTable("STREET");

            entity.Property(e => e.StreetId).HasColumnName("STREET_ID");
            entity.Property(e => e.CityId).HasColumnName("CITY_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");

            entity.HasOne(d => d.City).WithMany(p => p.Streets)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_STREET_CITY");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USER");

            entity.HasIndex(e => e.UserName, "UNQ_USER_USER_NAME").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("PASSWORD_HASH");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("USER_NAME");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_USER_ROLE_ROLE"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_USER_ROLE_USER"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("USER_ROLE");
                        j.IndexerProperty<int>("UserId").HasColumnName("USER_ID");
                        j.IndexerProperty<int>("RoleId").HasColumnName("ROLE_ID");
                    });
        });

        modelBuilder.Entity<UserEmail>(entity =>
        {
            entity.HasKey(e => e.UserEmailId).HasName("PK_EMAIL");

            entity.ToTable("USER_EMAIL");

            entity.HasIndex(e => new { e.UserId, e.Email }, "IX_USER_EMAIL_UNIQUE").IsUnique();

            entity.Property(e => e.UserEmailId).HasColumnName("USER_EMAIL_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.IsMain).HasColumnName("IS_MAIN");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.UserEmails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_EMAIL_USER");
        });

        modelBuilder.Entity<UserPhone>(entity =>
        {
            entity.ToTable("USER_PHONE");

            entity.HasIndex(e => new { e.UserId, e.Number }, "IX_USER_PHONE_UNIQUE").IsUnique();

            entity.Property(e => e.UserPhoneId).HasColumnName("USER_PHONE_ID");
            entity.Property(e => e.IsMain).HasColumnName("IS_MAIN");
            entity.Property(e => e.Number)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUMBER");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.UserPhones)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_PHONE_USER");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("VEHICLE");

            entity.HasIndex(e => e.LicensePlate, "UNQ_VEHICLE_LICENSE_PLATE").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnName("VEHICLE_ID");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("BRAND");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .HasColumnName("COLOR");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.LicensePlate)
                .HasMaxLength(20)
                .HasColumnName("LICENSE_PLATE");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("MODEL");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICLE_USER");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

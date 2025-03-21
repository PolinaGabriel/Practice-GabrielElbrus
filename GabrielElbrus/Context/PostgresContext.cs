using System;
using System.Collections.Generic;
using GabrielElbrus.Models;
using Microsoft.EntityFrameworkCore;

namespace GabrielElbrus.Context;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Enter> Enters { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleService> SaleServices { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=localhost:5432;Database=postgres;Username=postgres;Password=password123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pk");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerAddress)
                .HasColumnType("character varying")
                .HasColumnName("customer_address");
            entity.Property(e => e.CustomerBirth)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("customer_birth");
            entity.Property(e => e.CustomerCode)
                .HasColumnType("character varying")
                .HasColumnName("customer_code");
            entity.Property(e => e.CustomerLogin)
                .HasColumnType("character varying")
                .HasColumnName("customer_login");
            entity.Property(e => e.CustomerName)
                .HasColumnType("character varying")
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerPassport)
                .HasColumnType("character varying")
                .HasColumnName("customer_passport");
            entity.Property(e => e.CustomerPassword)
                .HasColumnType("character varying")
                .HasColumnName("customer_password");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pk");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("employee_id");
            entity.Property(e => e.EmployeeCode)
                .HasColumnType("character varying")
                .HasColumnName("employee_code");
            entity.Property(e => e.EmployeeImage)
                .HasColumnType("character varying")
                .HasColumnName("employee_image");
            entity.Property(e => e.EmployeeLogin)
                .HasColumnType("character varying")
                .HasColumnName("employee_login");
            entity.Property(e => e.EmployeeName)
                .HasColumnType("character varying")
                .HasColumnName("employee_name");
            entity.Property(e => e.EmployeePassword)
                .HasColumnType("character varying")
                .HasColumnName("employee_password");
            entity.Property(e => e.EmployeePosition).HasColumnName("employee_position");

            entity.HasOne(d => d.EmployeePositionNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeePosition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_position_fk");
        });

        modelBuilder.Entity<Enter>(entity =>
        {
            entity.HasKey(e => e.EnterId).HasName("enter_pk");

            entity.ToTable("enter");

            entity.Property(e => e.EnterId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("enter_id");
            entity.Property(e => e.EnterEmployee).HasColumnName("enter_employee");
            entity.Property(e => e.EnterResult).HasColumnName("enter_result");
            entity.Property(e => e.EnterTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enter_time");

            entity.HasOne(d => d.EnterEmployeeNavigation).WithMany(p => p.Enters)
                .HasForeignKey(d => d.EnterEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enter_employee_fk");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("position_pk");

            entity.ToTable("position");

            entity.Property(e => e.PositionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("position_id");
            entity.Property(e => e.PositionName)
                .HasColumnType("character varying")
                .HasColumnName("position_name");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("sale_pk");

            entity.ToTable("sale");

            entity.Property(e => e.SaleId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sale_id");
            entity.Property(e => e.SaleCode)
                .HasColumnType("character varying")
                .HasColumnName("sale_code");
            entity.Property(e => e.SaleCustomer).HasColumnName("sale_customer");
            entity.Property(e => e.SaleEmployee).HasColumnName("sale_employee");
            entity.Property(e => e.SaleEnd)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sale_end");
            entity.Property(e => e.SaleHours).HasColumnName("sale_hours");
            entity.Property(e => e.SaleStart)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("sale_start");
            entity.Property(e => e.SaleStatus).HasColumnName("sale_status");

            entity.HasOne(d => d.SaleCustomerNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SaleCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_customer_fk");

            entity.HasOne(d => d.SaleEmployeeNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SaleEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_employee_fk");

            entity.HasOne(d => d.SaleStatusNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.SaleStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_status_fk");
        });

        modelBuilder.Entity<SaleService>(entity =>
        {
            entity.HasKey(e => e.SaleServiceId).HasName("sale_service_pk");

            entity.ToTable("sale_service");

            entity.Property(e => e.SaleServiceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("sale_service_id");
            entity.Property(e => e.SaleServiceSale).HasColumnName("sale_service_sale");
            entity.Property(e => e.SaleServiceService).HasColumnName("sale_service_service");

            entity.HasOne(d => d.SaleServiceSaleNavigation).WithMany(p => p.SaleServices)
                .HasForeignKey(d => d.SaleServiceSale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_service_sale_fk");

            entity.HasOne(d => d.SaleServiceServiceNavigation).WithMany(p => p.SaleServices)
                .HasForeignKey(d => d.SaleServiceService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_service_service_fk");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("service_pk");

            entity.ToTable("service");

            entity.Property(e => e.ServiceId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("service_id");
            entity.Property(e => e.ServiceCode)
                .HasColumnType("character varying")
                .HasColumnName("service_code");
            entity.Property(e => e.ServiceName)
                .HasColumnType("character varying")
                .HasColumnName("service_name");
            entity.Property(e => e.ServicePrice).HasColumnName("service_price");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("status_pk");

            entity.ToTable("status");

            entity.Property(e => e.StatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasColumnType("character varying")
                .HasColumnName("status_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

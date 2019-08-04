using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;

namespace intermediate_agency
{
    /// <summary>
    /// Class context to work with DB
    /// </summary>
    public class AgencyDBContext : DbContext
    {
        #region DbSets

        public DbSet<Person> People { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<MerchandiseOrder> MerchandiseOrders { get; set; }
        public DbSet<MerchandiseType> MerchandiseTypes { get; set; }

        #endregion

        #region Constructor

        public AgencyDBContext() : base("DefaultConnection")
        {
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new SellerConfig());    
            modelBuilder.Configurations.Add(new OrderConfig());
            modelBuilder.Configurations.Add(new MerchandiseOrderConfig());
            modelBuilder.Configurations.Add(new OfferConfig());
            modelBuilder.Configurations.Add(new MerchandiseTypeConfig());
        }
    }

    #region Connection configurations

    public abstract class AbstractPersonConfig<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : Person
    {
       public AbstractPersonConfig()
        {
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Phone).IsRequired();
        }
    }

    public class EmployeeConfig : AbstractPersonConfig<Employee>
    {
        public EmployeeConfig()
        {
            this.ToTable("Employees");
            this.Property(e => e.Post).IsRequired();

            this.HasMany(e => e.Orders).WithOptional(o => o.Manager);
        }
    }

    public class CustomerConfig : AbstractPersonConfig<Customer>
    {
        public CustomerConfig()
        {
            this.ToTable("Customers");
            this.Property(c => c.Level).IsRequired();

            this.HasMany(c => c.Orders).WithRequired(o => o.Owner);
        }
    }

    public class SellerConfig : AbstractPersonConfig<Seller>
    {
        public SellerConfig()
        {
            this.ToTable("Sellers");
            this.Property(s => s.Reliability).IsRequired();

            this.HasMany(s => s.Offers).WithRequired(o => o.Owner);
        }
    }

    public class OrderConfig : EntityTypeConfiguration<Order>
    {
        public OrderConfig()
        {
            this.Property(o => o.Status).IsRequired();

            this.HasMany(o => o.MerchOrders).WithRequired(m => m.Order).HasForeignKey(m => m.OrderId);
        }
    }

    public class MerchandiseOrderConfig : EntityTypeConfiguration<MerchandiseOrder>
    {
        public MerchandiseOrderConfig()
        {
            this.HasKey(m => new { m.OrderId, m.MerchTypeId });
            this.Property(m => m.Amount).IsRequired();
        }
    }

    public class OfferConfig : EntityTypeConfiguration<Offer>
    {
        public OfferConfig()
        {
            this.Property(o => o.Price).IsRequired();


        }
    }

    public class MerchandiseTypeConfig : EntityTypeConfiguration<MerchandiseType>
    {
        public MerchandiseTypeConfig()
        {
            this.Property(m => m.Name).IsRequired();

            this.HasMany(m => m.Orders).WithRequired(m => m.MerchType).HasForeignKey(m => m.MerchTypeId);
            this.HasMany(m => m.Offers).WithRequired(o => o.Type).HasForeignKey(o => o.TypeId);
        }
    }

    #endregion
}

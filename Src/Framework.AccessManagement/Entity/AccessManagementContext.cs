using System;
using Framework.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Framework.AccessManagement.Entity
{
    public partial class AccessManagementContext : DbContext
    {
        public AccessManagementContext()
        {
        }

        public AccessManagementContext(DbContextOptions<AccessManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessControl> AccessControls { get; set; }
        public virtual DbSet<ApiResource> ApiResources { get; set; }
        public virtual DbSet<ElementResource> ElementResources { get; set; }
        public virtual DbSet<MenuResource> MenuResources { get; set; }
        public virtual DbSet<PageResource> PageResources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\.;Initial Catalog=Access;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<AccessControl>(entity =>
            {
                entity.ToTable("AccessControl","sec");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.ApiResource)
                    .WithMany(p => p.AccessControls)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccessControl_ApiResource");

                entity.HasOne(d => d.ElementResource)
                    .WithMany(p => p.AccessControls)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccessControl_ElementResource");

                entity.HasOne(d => d.MenuResource)
                    .WithMany(p => p.AccessControls)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccessControl_MenuResource");

                entity.HasOne(d => d.PageResource)
                    .WithMany(p => p.AccessControls)
                    .HasForeignKey(d => d.ResourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccessControl_PageResource");
            });


            modelBuilder.Entity<BaseResource>().ToTable("Resource","sec").HasDiscriminator<ResourceType>("Type")
                
                .HasValue<ApiResource>(ResourceType.Api)
                .HasValue<PageResource>(ResourceType.Page)
                .HasValue<ElementResource>(ResourceType.Element)
                .HasValue<MenuResource>(ResourceType.Menu)
                ;


            modelBuilder.Entity<ApiResource>(entity =>
            {
                entity.HasBaseType<BaseResource>();
                entity.HasIndex(e => e.Name)
                    .HasName("ResourceNameIndex")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(200);
                entity.Property(e => e.MethodType).HasMaxLength(8);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<ElementResource>(entity =>
            {
                entity.HasBaseType<BaseResource>();

                entity.Property(e => e.ElementTitleEn).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.PageResource)
                    .WithMany(p => p.ElementResources)
                    .HasForeignKey(d => d.PageResourceId)
                    .HasConstraintName("FK_ElementResource_PageResource");
            });

            modelBuilder.Entity<MenuResource>(entity =>
            {
                entity.HasBaseType<BaseResource>();

                entity.Property(e => e.MenuIcon).HasMaxLength(50);

                entity.Property(e => e.MenuTitleEn).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.MenuPage)
                    .WithMany(p => p.MenuResources)
                    .HasForeignKey(d => d.MenuPageId)
                    .HasConstraintName("FK_MenuResource_PageResource");

                entity.HasOne(d => d.ParenMenuResource)
                    .WithMany(p => p.InverseParenMenuResource)
                    .HasForeignKey(d => d.ParenMenuResourceId)
                    .HasConstraintName("FK_ParentMenu");
            });

            modelBuilder.Entity<PageResource>(entity =>
            {
                entity.HasBaseType<BaseResource>();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PageLink).HasMaxLength(128);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
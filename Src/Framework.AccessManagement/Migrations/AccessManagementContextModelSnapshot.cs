﻿// <auto-generated />
using System;
using Framework.AccessManagement.Entity;
using Framework.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Framework.AccessManagement.Migrations
{
    [DbContext(typeof(AccessManagementContext))]
    partial class AccessManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Framework.AccessManagement.Entity.AccessControl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Access");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128);

                    b.Property<string>("Ip")
                        .HasMaxLength(64);

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(128);

                    b.Property<int>("ResourceId");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("AccessControl","sec");
                });

            modelBuilder.Entity("Framework.AccessManagement.Entity.BaseResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128);

                    b.Property<string>("Ip")
                        .HasMaxLength(64);

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Resource","sec");

                    b.HasDiscriminator<int>("Type");
                });

            modelBuilder.Entity("Framework.AccessManagement.Entity.ApiResource", b =>
                {
                    b.HasBaseType("Framework.AccessManagement.Entity.BaseResource");

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<string>("MethodType")
                        .HasMaxLength(8);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("ResourceNameIndex");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Framework.AccessManagement.Entity.ElementResource", b =>
                {
                    b.HasBaseType("Framework.AccessManagement.Entity.BaseResource");

                    b.Property<string>("ElementTitleEn")
                        .HasMaxLength(128);

                    b.Property<int?>("PageResourceId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("ElementResource_Title")
                        .HasMaxLength(128);

                    b.HasIndex("PageResourceId");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("Framework.AccessManagement.Entity.MenuResource", b =>
                {
                    b.HasBaseType("Framework.AccessManagement.Entity.BaseResource");

                    b.Property<string>("MenuIcon")
                        .HasMaxLength(50);

                    b.Property<int?>("MenuOrder");

                    b.Property<int?>("MenuPageId");

                    b.Property<string>("MenuTitleEn")
                        .HasMaxLength(128);

                    b.Property<int?>("ParenMenuResourceId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("MenuResource_Title")
                        .HasMaxLength(128);

                    b.HasIndex("MenuPageId");

                    b.HasIndex("ParenMenuResourceId");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Framework.AccessManagement.Entity.PageResource", b =>
                {
                    b.HasBaseType("Framework.AccessManagement.Entity.BaseResource");

                    b.Property<string>("PageLink")
                        .HasMaxLength(128);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("PageResource_Title")
                        .HasMaxLength(128);

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Framework.AccessManagement.Entity.AccessControl", b =>
                {
                    b.HasOne("Framework.AccessManagement.Entity.ApiResource", "ApiResource")
                        .WithMany("AccessControls")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK_AccessControl_ApiResource");

                    b.HasOne("Framework.AccessManagement.Entity.ElementResource", "ElementResource")
                        .WithMany("AccessControls")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK_AccessControl_ElementResource");

                    b.HasOne("Framework.AccessManagement.Entity.MenuResource", "MenuResource")
                        .WithMany("AccessControls")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK_AccessControl_MenuResource");

                    b.HasOne("Framework.AccessManagement.Entity.PageResource", "PageResource")
                        .WithMany("AccessControls")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK_AccessControl_PageResource");
                });

            modelBuilder.Entity("Framework.AccessManagement.Entity.ElementResource", b =>
                {
                    b.HasOne("Framework.AccessManagement.Entity.PageResource", "PageResource")
                        .WithMany("ElementResources")
                        .HasForeignKey("PageResourceId")
                        .HasConstraintName("FK_ElementResource_PageResource");
                });

            modelBuilder.Entity("Framework.AccessManagement.Entity.MenuResource", b =>
                {
                    b.HasOne("Framework.AccessManagement.Entity.PageResource", "MenuPage")
                        .WithMany("MenuResources")
                        .HasForeignKey("MenuPageId")
                        .HasConstraintName("FK_MenuResource_PageResource");

                    b.HasOne("Framework.AccessManagement.Entity.MenuResource", "ParenMenuResource")
                        .WithMany("InverseParenMenuResource")
                        .HasForeignKey("ParenMenuResourceId")
                        .HasConstraintName("FK_ParentMenu");
                });
#pragma warning restore 612, 618
        }
    }
}

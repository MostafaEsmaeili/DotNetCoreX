using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pishkhan.Domain.Entity;
using ApiResource = Pishkhan.Domain.Entity.ApiResource;
using Resource = Pishkhan.Domain.Entity.Resource;

namespace Pishkhan.UserManagement
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Domain.Entity.PageResource> PageResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<MenuResource> MenuResources { get; set; }
        public DbSet<ButtonResource> ButtonResources { get; set; }
      //  public DbSet<FieldResource> FieldResources { get; set; }
      //  public DbSet<GridActionResource> GridActionResources { get; set; }

        public DbSet<Domain.Entity.AccessControl> AccessControls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ResourceApiResource>()
            //    .HasKey(bc => new { bc.ApiResourceId, bc.ResourceId });

            //builder.Entity<ResourceApiResource>()
            //    .HasOne(bc => bc.Resource)
            //    .WithMany(b => b.ApiPages)
            //    .HasForeignKey(bc => bc.ResourceId);

            //builder.Entity<ResourceApiResource>()
            //    .HasOne(bc => bc.ApiResource)
            //    .WithMany(c => c.ApiPages)
            //    .HasForeignKey(bc => bc.ApiResourceId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public ApplicationDbContextFactory()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _configuration= config;
        }
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connectionstring = _configuration.GetConnectionString("Pishkhan");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionstring, x=> x.MigrationsAssembly("Pishkhan.IdentityServer"));
           // optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Pishkhan"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
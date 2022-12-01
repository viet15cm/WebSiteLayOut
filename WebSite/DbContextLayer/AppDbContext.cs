using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models;
using WebSite.Models.Identity;

namespace WebSite.DbContextLayer
{
    public class IdentityStoreServices : IdentityDbContext<AppUser>
    {
        public IConfiguration Configuration { get; }

        public IdentityStoreServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public DbSet<DataImage> DataImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Somee
            //LocalHost
            //Freeasphosting
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("LocalHost"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var item in builder.Model.GetEntityTypes())
            {
                var tableName = item.GetTableName();

                if (tableName.StartsWith("AspNet"))
                {
                    item.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}

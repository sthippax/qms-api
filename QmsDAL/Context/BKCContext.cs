using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using QmsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDAL.Context
{
    internal class BKCContext : DbContext
    {
        public BKCContext() : base() { }
        public BKCContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.RedundantIndexRemoved));

            if (!optionsBuilder.IsConfigured)
            {
                string conn = "Data Source=BAJARVIS001;Initial Catalog=WSIV_UI;Persist Security Info=True;User ID=JPCAdmin;Password=welcome@123;TrustServerCertificate=True";
                optionsBuilder.UseSqlServer(conn);//Environment.GetEnvironmentVariable("ConnectionString"));
            }
        }
    }
}

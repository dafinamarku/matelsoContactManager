using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DB
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contact>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Log> Logs { get; set; }
    }
}

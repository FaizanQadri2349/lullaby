using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services_lullabay.Authentication;
using Services_lullabay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_lullabay.Data
{
    public class LulabayDbContext : IdentityDbContext<ApplicationUser>
    {
        public LulabayDbContext(DbContextOptions<LulabayDbContext> options) :base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Device> devices { get; set; }
    }
}

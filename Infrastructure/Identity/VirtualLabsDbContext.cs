using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class VirtualLabsDbContext : DbContext
    {
        public VirtualLabsDbContext(DbContextOptions<VirtualLabsDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}


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
        public DbSet<MeasurementLogs> MeasurementLogs { get; set; }
        public DbSet<ValuesLogs> ValuesLogs { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Position> Positions {get;set;}

    }
}


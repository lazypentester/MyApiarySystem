using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resource.Api.Entities
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {}

        public DbSet<EUser> Users { get; set; }
        public DbSet<EApiary> Apiaries { get; set; }
        public DbSet<EBeehive> Beehives { get; set; }
        public DbSet<ETariff> Tariffs { get; set; }
        public DbSet<ESensor> Sensors { get; set; }
        public DbSet<ERole> Roles { get; set; }
        public DbSet<EConfirmation> Confirmations { get; set; }
        public DbSet<EDevice> Devices { get; set; }
        public DbSet<ENotification> Notifications { get; set; }
        public DbSet<ESensorType> SensorTypes { get; set; }
        public DbSet<ESession> Sessions { get; set; }   
        public DbSet<ERrt_blacklist> Rrt_Blacklists { get; set; }
        public DbSet<EBaseStation> BaseStations { get; set; }
    }
}

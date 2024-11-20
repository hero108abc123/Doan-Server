using DA.Vehicle.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.Infrastructure
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {
        }

        public DbSet<VehicleBus> Buses { get; set; }

        public DbSet<VehicleSeat> Seats { get; set; }

        public DbSet<VehicleBusRide> BusRides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleSeat>()
                .HasOne(vb => vb.VehicleBus)
                .WithMany(vs => vs.Seats)
                .HasForeignKey(vb => vb.BusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VehicleBusRide>()
                .HasOne(vb => vb.VehicleBus)
                .WithMany(vbr => vbr.BusRides)
                .HasForeignKey(vb => vb.BusId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

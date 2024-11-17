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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleSeat>()
                .HasOne(vs => vs.VehicleBus)
                .WithMany(vb => vb.Seats)
                .HasForeignKey(vs => vs.BusId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

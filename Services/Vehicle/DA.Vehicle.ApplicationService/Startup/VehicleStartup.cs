using DA.Shared.Constant.Database;
using DA.Vehicle.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Vehicle.ApplicationService.Startup
{
    public static class VehicleStartup
    {
        public static void ConfigureVehicle(this WebApplicationBuilder builder, string? assemblyName)
        {
            builder.Services.AddDbContext<VehicleDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Vehicle 
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );
        }
    }
}

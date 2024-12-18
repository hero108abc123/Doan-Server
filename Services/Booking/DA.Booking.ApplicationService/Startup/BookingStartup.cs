﻿using DA.Booking.ApplicationService.BookingModule.Abstracts;
using DA.Booking.ApplicationService.BookingModule.Implements;
using DA.Booking.Infrastucture;
using DA.Shared.ApplicationService.Auth;
using DA.Shared.Constant.Database;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Booking.ApplicationService.Startup
{
    public static class BookingStartup
    {
        public static void ConfigureBooking(this WebApplicationBuilder builder, string? assemblyName)
        {
            builder.Services.AddDbContext<BookingDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Booking
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, VnPayService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            // Configure Hangfire
            builder.Services.AddHangfire(configuration =>
                configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("Default")));

            // Add Hangfire server
            builder.Services.AddHangfireServer();
        }
    }
}

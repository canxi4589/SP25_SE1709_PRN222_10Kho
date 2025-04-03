using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCP.Repositori.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace CCP.Service.BackgroundServices
{
    public class AppointmentStatusUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AppointmentStatusUpdateService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1); // Check every minute

        public AppointmentStatusUpdateService(IServiceProvider serviceProvider, ILogger<AppointmentStatusUpdateService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("AppointmentStatusUpdateService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateAppointmentStatuses();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating appointment statuses.");
                }

                // Wait for the next interval
                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("AppointmentStatusUpdateService is stopping.");
        }

        private async Task UpdateAppointmentStatuses()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                // Get the current date and time
                var now = DateTime.UtcNow;

                // Fetch appointments that are in the past and not yet marked as "Completed"
                var appointmentsToUpdate = await unitOfWork.Repository<Appointment>()
                    .GetAll()
                    .Where(a => a.Status != "Completed" && a.Status != "Cancelled") // Exclude already completed or cancelled appointments
                    .Where(a => a.BookingDate.Date < now.Date || // Date is in the past
                                (a.BookingDate.Date == now.Date && // Same date, check time
                                 a.EndTime < TimeSpan.FromHours(now.Hour) + TimeSpan.FromMinutes(now.Minute)))
                    .ToListAsync();

                if (appointmentsToUpdate.Any())
                {
                    _logger.LogInformation($"Found {appointmentsToUpdate.Count} appointments to update to 'Completed'.");

                    foreach (var appointment in appointmentsToUpdate)
                    {
                        appointment.Status = "Completed";
                        unitOfWork.Repository<Appointment>().Update(appointment);
                    }

                    await unitOfWork.Complete();
                    _logger.LogInformation($"Successfully updated {appointmentsToUpdate.Count} appointments to 'Completed'.");
                }
                else
                {
                    _logger.LogDebug("No appointments found to update.");
                }
            }
        }
    }
}

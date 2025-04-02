using CCP.Repositori.Data;
using CCP.Repositori.Entities;
using CCP.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCP.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentAsync()
        {
            var appointments = await _context.Appointments.ToListAsync();

            return appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                ParentId = a.ParentId,
                ChildId = a.ChildId,
                ExpertId = a.ExpertId,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                MeasurementId = a.MeasurementId,
                PhysicalActivityId = a.PhysicalActivityId,
                NutritionalIntake = a.NutritionalIntake,
                SleepPatternId = a.SleepPatternId,
                Type = a.Type,
                BookingDate = a.BookingDate,
                Status = a.Status,
                Price = a.Price
            });
        }
    }
}

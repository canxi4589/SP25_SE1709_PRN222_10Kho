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
    public class AppointmentServices : IAppointmentServices
    {
        private readonly ApplicationDbContext _context;

        public AppointmentServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppointmentHistoryDTO>> GetAllAppointmentAsync()
        {
            var appointments = await _context.Appointments.ToListAsync();

            return appointments.Select(a => new AppointmentHistoryDTO
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

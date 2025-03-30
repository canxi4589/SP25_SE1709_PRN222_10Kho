using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public AppointmentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task CreateAppointmentAsync(AppointmentDTO appointmentDTO)
        {
            await _unitOfWork.Repository<Appointment>().AddAsync(new Appointment
            {
                Id = Guid.NewGuid(),
                UserId = appointmentDTO.UserId,
                ChildId = appointmentDTO.ChildId,
                ExpertId = appointmentDTO.ExpertId,
                StartTime = appointmentDTO.StartTime,
                EndTime = appointmentDTO.EndTime,
                BookingDate = appointmentDTO.BookingDate,
                Status = appointmentDTO.Status,
                Price = appointmentDTO.Price
            });
        }
    }
}

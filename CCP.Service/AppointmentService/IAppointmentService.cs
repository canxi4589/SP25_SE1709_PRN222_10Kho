using CCP.Service.DTOs;

namespace CCP.Service.AppointmentService
{
    public interface IAppointmentService
    {
        Task CreateAppointmentAsync(AppointmentDTO appointmentDTO);
    }
}
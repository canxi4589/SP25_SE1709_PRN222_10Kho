using CCP.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCP.Service
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentAsync();
    }
}

using CCP.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCP.Service
{
    public interface IAppointmentServices
    {
        Task<IEnumerable<AppointmentHistoryDTO>> GetAllAppointmentAsync();
    }
}

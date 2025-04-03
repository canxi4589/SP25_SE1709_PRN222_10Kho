using System;
using System.Threading.Tasks;
using CCP.Service.DTOs;

namespace CCP.Service
{
    public interface IMeasurementInputService
    {
        
        Task<bool> SaveAsync(Guid childId, MeasurementInputDto input);
    }
}

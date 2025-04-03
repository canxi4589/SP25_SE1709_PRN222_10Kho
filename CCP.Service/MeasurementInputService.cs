using CCP.Repositori.Data;
using CCP.Repositori.Entities;
using CCP.Service.DTOs;
using System;
using System.Threading.Tasks;

namespace CCP.Service
{
    public class MeasurementInputService : IMeasurementInputService
    {
        private readonly ApplicationDbContext _context;

        public MeasurementInputService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync(Guid childId, MeasurementInputDto input)
        {
            try
            {
                var measurement = new Measurement
                {
                    Id = Guid.NewGuid(),
                    ChildId = childId,
                    RecordDate = DateTime.Now,
                    Height = input.Height,
                    Weight = input.Weight,
                    HeadCircumference = input.HeadCircumference,
                    Status = "New"
                };

                _context.Measurements.Add(measurement);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Saving measurement failed: {ex.Message}");
                return false;
            }
        }
    }
}

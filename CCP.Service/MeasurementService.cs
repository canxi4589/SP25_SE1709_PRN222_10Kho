using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public MeasurementService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<List<Measurement>> GetMeasurementsByChild(Guid childId)
        {
            return await _unitOfWork.Repository<Measurement>().GetAll()
                .Where(m => m.ChildId == childId)
                .OrderByDescending(m => m.RecordDate)
                .ToListAsync();
        }

        public async Task<(Child?, List<Measurement>)> GetChildWithMeasurementsAsync(Guid childId)
        {
            var result = await _unitOfWork.Repository<Child>().GetAll()
                .Include(c => c.Measurements)
                .FirstOrDefaultAsync(c => c.Id == childId);

            return (result, result?.Measurements?.ToList() ?? new List<Measurement>());
        }

    }
}

using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.PhysicalActivityService
{
    public class PhysicalActivityService : IPhysicalActivityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhysicalActivityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PhysicalActivityDTO>> GetAllPhysicalActivities()
        {
            var physicalActivities = await _unitOfWork.Repository<PhysicalActivity>().GetAll().ToListAsync();
            return physicalActivities.Select(x => new PhysicalActivityDTO
            {
                RecordDate = x.RecordDate,
                ActivityType = x.ActivityType,
                Duration = x.Duration,
                Intensity = x.Intensity
            });
        }
        public async Task<PhysicalActivityDTO> GetPhysicalActivityById(Guid id)
        {
            var physicalActivity = _unitOfWork.Repository<PhysicalActivity>().GetById(id);
            return new PhysicalActivityDTO
            {
                RecordDate = physicalActivity.RecordDate,
                ActivityType = physicalActivity.ActivityType,
                Duration = physicalActivity.Duration,
                Intensity = physicalActivity.Intensity
            };
        }
        public async Task<List<PhysicalActivityDTO>> GetPhysicalActivityByChildId(Guid childId)
        {
            var physicalActivity = await _unitOfWork.Repository<PhysicalActivity>().GetAll().Where(m => m.ChildId.Equals(childId)).ToListAsync();
            return physicalActivity.Select(x => new PhysicalActivityDTO
            {
                RecordDate = x.RecordDate,
                ActivityType = x.ActivityType,
                Duration = x.Duration,
                Intensity = x.Intensity
            }).ToList();
        }
        public async Task AddPhysicalActivity(PhysicalActivity physicalActivity)
        {
            await _unitOfWork.Repository<PhysicalActivity>().AddAsync(physicalActivity);
            await _unitOfWork.Complete();
        }
    }
}

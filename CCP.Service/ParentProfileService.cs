using CCP.Repositori.Data;
using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CCP.Service
{
    public class ParentProfileService : IParentProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public ParentProfileService(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IEnumerable<ChildDto>> GetChildrenAsync(Guid userId)
        {
            var children = await _context.Children
                .Include(c => c.PhysicalActivities)
                .Include(c => c.SleepPatterns)
                .Include(c => c.NutritionalIntakes)
                .Include(c => c.HealthMetrics)
                .Where(c => c.UserId == userId.ToString())
                .ToListAsync();

            return children.Select(MapChildToDto);
        }

        public async Task<ChildDto?> GetChildByIdAsync(Guid childId, Guid userId)
        {
            var child = await _context.Children
                .Include(c => c.PhysicalActivities)
                .Include(c => c.SleepPatterns)
                .Include(c => c.NutritionalIntakes)
                .Include(c => c.HealthMetrics)
                .FirstOrDefaultAsync(c => c.Id == childId && c.UserId == userId.ToString());

            return child == null ? null : MapChildToDto(child);
        }

        public async Task<ChildDto> CreateChildAsync(Guid userId, ChildDto dto)
        {
            var child = new Child
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                UserId = userId.ToString()
            };

            await _unitOfWork.Repository<Child>().AddAsync(child);
            await _unitOfWork.Complete();

            dto.Id = child.Id;
            return dto;
        }

        public async Task<ChildDto?> UpdateChildAsync(Guid childId, Guid userId, ChildDto dto)
        {
            var repo = _unitOfWork.Repository<Child>();
            var child = await repo.FindAsync(c => c.Id == childId && c.UserId == userId.ToString());

            if (child == null) return null;

            child.Name = dto.Name;
            child.Gender = dto.Gender;
            child.DateOfBirth = dto.DateOfBirth;

            repo.Update(child);
            await _unitOfWork.Complete();

            return dto;
        }

        public async Task<bool> DeleteChildAsync(Guid childId, Guid userId)
        {
            var repo = _unitOfWork.Repository<Child>();
            var child = await repo.FindAsync(c => c.Id == childId && c.UserId == userId.ToString());

            if (child == null) return false;

            repo.Delete(child);
            await _unitOfWork.Complete();

            return true;
        }

        public async Task<ParentDto?> GetParentProfileAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.Children)
                    .ThenInclude(c => c.PhysicalActivities)
                .Include(u => u.Children)
                    .ThenInclude(c => c.SleepPatterns)
                .Include(u => u.Children)
                    .ThenInclude(c => c.NutritionalIntakes)
                .Include(u => u.Children)
                    .ThenInclude(c => c.HealthMetrics)
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());

            if (user == null) return null;

            return MapAppUserToParentDto(user);
        }

        public async Task<bool> UpdateParentProfileAsync(Guid userId, ParentDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.DateOfBirth = dto.DateOfBirth;
            user.IsActive = dto.IsActive;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Guid?> GetParentProfileIdAsync(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return user != null && Guid.TryParse(user.Id, out var guidId) ? guidId : null;
        }

        private ParentDto MapAppUserToParentDto(AppUser user)
        {
            return new ParentDto
            {
                Id = Guid.Parse(user.Id),
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                RegistrationDate = user.RegistrationDate,
                LastLogin = user.LastLogin,
                DateOfBirth = user.DateOfBirth,
                Children = user.Children?.Select(MapChildToDto).ToList()
            };
        }

        private ChildDto MapChildToDto(Child c)
        {
            return new ChildDto
            {
                Id = c.Id,
                Name = c.Name,
                Gender = c.Gender,
                DateOfBirth = c.DateOfBirth,
                PhysicalActivities = c.PhysicalActivities?.Select(pa => new PhysicalActivityDto
                {
                    Id = pa.Id,
                    RecordDate = pa.RecordDate,
                    ActivityType = pa.ActivityType,
                    Duration = pa.Duration,
                    Intensity = pa.Intensity,
                    Status = pa.Status
                }).ToList(),
                SleepPatterns = c.SleepPatterns?.Select(sp => new SleepPatternDto
                {
                    Id = sp.Id,
                    RecordDate = sp.RecordDate,
                    Bedtime = sp.Bedtime,
                    WakeTime = sp.WakeTime,
                    NapDuration = sp.NapDuration,
                    SleepQuality = sp.SleepQuality,
                    SleepQualityRating = sp.SleepQualityRating,
                    Status = sp.Status
                }).ToList(),
                NutritionalIntakes = c.NutritionalIntakes?.Select(n => new NutritionalIntakeDto
                {
                    Id = n.Id,
                    IntakeDate = n.IntakeDate,
                    ServingSize = n.ServingSize,
                    RecordDate = n.RecordDate,
                    Status = n.Status,
                    FoodItemId = n.FoodItemId
                }).ToList(),
                HealthMetrics = c.HealthMetrics?.Select(h => new HealthMetricDto
                {
                    Id = h.Id,
                    MetricDate = h.MetricDate,
                    Temperature = h.Temperature,
                    HeartRate = h.HeartRate,
                    BloodPressure = h.BloodPressure,
                    AllergySymptoms = h.AllergySymptoms,
                    MedicationUse = h.MedicationUse
                }).ToList()
            };
        }
    }
}
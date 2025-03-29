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

        public async Task<IEnumerable<ChildDto>> GetChildrenAsync(string userId)
        {
            var repo = _unitOfWork.Repository<Child>();
            var children = await repo.FindAllAsync(c => c.UserId == userId);

            return children.Select(c => new ChildDto
            {
                Id = c.Id,
                Name = c.Name,
                Gender = c.Gender,
                DateOfBirth = c.DateOfBirth
            });
        }

        public async Task<ChildDto?> GetChildByIdAsync(Guid childId, string userId)
        {
            var child = await _unitOfWork.Repository<Child>()
                .FindAsync(c => c.Id == childId && c.UserId == userId);

            return child == null
                ? null
                : new ChildDto
                {
                    Id = child.Id,
                    Name = child.Name,
                    Gender = child.Gender,
                    DateOfBirth = child.DateOfBirth
                };
        }

        public async Task<ChildDto> CreateChildAsync(string userId, ChildDto dto)
        {
            var child = new Child
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                UserId = userId
            };

            await _unitOfWork.Repository<Child>().AddAsync(child);
            await _unitOfWork.Complete();

            dto.Id = child.Id;
            return dto;
        }

        public async Task<ChildDto?> UpdateChildAsync(Guid childId, string userId, ChildDto dto)
        {
            var repo = _unitOfWork.Repository<Child>();
            var existing = await repo.FindAsync(c => c.Id == childId && c.UserId == userId);

            if (existing == null) return null;

            existing.Name = dto.Name;
            existing.Gender = dto.Gender;
            existing.DateOfBirth = dto.DateOfBirth;

            repo.Update(existing);
            await _unitOfWork.Complete();

            return dto;
        }

        public async Task<bool> DeleteChildAsync(Guid childId, string userId)
        {
            var repo = _unitOfWork.Repository<Child>();
            var child = await repo.FindAsync(c => c.Id == childId && c.UserId == userId);

            if (child == null) return false;

            repo.Delete(child);
            await _unitOfWork.Complete();

            return true;
        }

        public async Task<ParentDto?> GetParentProfileAsync(string userId)
        {
            var user = await _context.Users
                .Include(u => u.Children)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return null;

            return new ParentDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                RegistrationDate = user.RegistrationDate,
                LastLogin = user.LastLogin,
                DateOfBirth = user.DateOfBirth,
                Children = user.Children?.Select(c => new ChildDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Gender = c.Gender,
                    DateOfBirth = c.DateOfBirth
                }).ToList()
            };
        }
        public async Task<bool> UpdateParentProfileAsync(string userId, ParentDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.DateOfBirth = dto.DateOfBirth;
            user.IsActive = dto.IsActive;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

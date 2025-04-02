using CCP.Service.DTOs;

namespace CCP.Service
{
    public interface IParentProfileService
    {
        Task<IEnumerable<ChildDto>> GetChildrenAsync(Guid userId);
        Task<ChildDto?> GetChildByIdAsync(Guid childId, Guid userId);
        Task<ChildDto> CreateChildAsync(Guid userId, ChildDto dto);
        Task<ChildDto?> UpdateChildAsync(Guid childId, Guid userId, ChildDto dto);
        Task<bool> DeleteChildAsync(Guid childId, Guid userId);
        Task<ParentDto?> GetParentProfileAsync(Guid userId);
        Task<Guid?> GetParentProfileIdAsync(string email);
        Task<bool> UpdateParentProfileAsync(Guid userId, ParentDto dto);
    }
}

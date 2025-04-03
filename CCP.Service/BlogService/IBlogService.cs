using CCP.Repositori.Entities;
using CCP.Service.DTOs;

namespace CCP.Service.BlogService
{
    public interface IBlogService
    {
        Task CreateBlogAsync(Blog blog);
        Task DeleteBlogAsync(Guid id);
        Task<BlogDTO> GetBlogByIdAsync(Guid id);
        Task<IEnumerable<BlogDTO>> GetBlogsAsync();
        Task InActiveBlogAsync(Guid id);
        Task UpdateBlogAsync(Guid id, BlogDTO blogDTO);
    }
}
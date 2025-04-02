using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.BlogService
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BlogDTO>> GetBlogsAsync()
        {
            var blogs = await _unitOfWork.Repository<Blog>().GetAll().Where(n => n.IsPublished == true).ToListAsync();
            return blogs.Select(blog => new BlogDTO
            {
                Title = blog.Title,
                Content = blog.Content,
                Author = blog.Author,
                ImageUrl = blog.ImageUrl,
                Summary = blog.Summary
            });
        }

        public async Task<BlogDTO> GetBlogByIdAsync(Guid id)
        {
            var blog = _unitOfWork.Repository<Blog>().GetById(id);
            return new BlogDTO
            {
                Title = blog.Title,
                Content = blog.Content,
                Author = blog.Author,
                ImageUrl = blog.ImageUrl,
                Summary = blog.Summary
            };
        }
        public async Task CreateBlogAsync(Blog blog)
        {
            blog.CreatedAt = DateTime.Now;
            await _unitOfWork.Repository<Blog>().AddAsync(blog);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateBlogAsync(Guid id, BlogDTO blogDTO)
        {
            var blog = _unitOfWork.Repository<Blog>().GetById(id);
            blog.UpdatedAt = DateTime.Now;
            blog.Title = blogDTO.Title;
            blog.Content = blogDTO.Content;
            blog.Author = blogDTO.Author;
            blog.ImageUrl = blogDTO.ImageUrl;
            blog.Summary = blogDTO.Summary;
            _unitOfWork.Repository<Blog>().Update(blog);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteBlogAsync(Guid id)
        {
            var blog = _unitOfWork.Repository<Blog>().GetById(id);
            _unitOfWork.Repository<Blog>().Delete(blog);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task InActiveBlogAsync(Guid id)
        {
            var blog = _unitOfWork.Repository<Blog>().GetById(id);
            blog.IsPublished = false;
            _unitOfWork.Repository<Blog>().Update(blog);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.FAQService
{
    public class FAQService : IFAQService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FAQService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<FAQDTO>> GetFAQ()
        {
            var faq = await _unitOfWork.Repository<FAQ>().GetAll().ToListAsync();
            return faq.Select(x => new FAQDTO
            {
                Question = x.Question,
                Answer = x.Answer,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            }).ToList();
        }
    }
}

using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Service.ExpertService
{
    public class ExpertService : IExpertService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpertService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ExpertDTO>> GetExpertsAsync()
        {
            var experts = await _unitOfWork.Repository<Expert>().GetAll().ToListAsync();
            return experts.Select(e => new ExpertDTO
            {
                Name = e.Name,
                Price = e.Price,
                ContactInfo = e.ContactInfo,
                Certificate = e.Certificate
            });
        }
        public async Task<List<ExpertDTO>> GetExpertBySpecialityAsync(string id)
        {
            var experts = await _unitOfWork.Repository<Expert>().GetAll().Where(n => n.Specialty.Id.ToString().Equals(id)).ToListAsync();
            return experts.Select(e => new ExpertDTO
            {
                Name = e.Name,
                Price = e.Price,
                ContactInfo = e.ContactInfo,
                Certificate = e.Certificate
            }).ToList();
        }
        public async Task<List<ExpertAvailability>> ExpertAvailabilities(string expertId)
        {
            var expertAvailabilities = await _unitOfWork.Repository<ExpertAvailability>().GetAll()
                .Where(ea => ea.Expert.Id.Equals(expertId))
                .ToListAsync();
            return expertAvailabilities.ToList();
        }

        public async Task<List<Specialty>> GetSpecialtiesAsync()
        {
            var specialties = await _unitOfWork.Repository<Specialty>().GetAll().ToListAsync();
            return specialties.ToList();
        }

    }
}

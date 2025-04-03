using CCP.Repositori.Data;
using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CCP.Services
{
    public class ExpertService1 : IExpertService1
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Random _random = new Random();
        private readonly IMeasurementInputService measurementInputService;

        public ExpertService1(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMeasurementInputService measurementInputService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            SeedInitialAvailabilityData();
            this.measurementInputService = measurementInputService;
        }
        public async Task<Expert> GetExpertByUserId(string userId)
        {
            var expert = await _unitOfWork.Repository<Expert>().FindAsync(c => c.UserId == userId);
            return expert;
        }

        public async Task<List<ExpertAvailability>> GetExpertAvailabilities(string expertId)
        {
            var expert = await _unitOfWork
                .Repository<Expert>()
                .GetAll()
                .Include(c => c.ExpertAvailabilities)
                .FirstOrDefaultAsync(cx => cx.Id == Guid.Parse(expertId));

            return expert.ExpertAvailabilities.ToList();
        }
        public async Task<Child> AddChild(Child child)
        {
           await  _unitOfWork.Repository<Child>().AddAsync(child);
            await _unitOfWork.SaveChangesAsync();
            return child;
        }

        public async Task<List<PhysicalActivity>> GetChildPhysicalActivities(Guid childId)
        {
            var lol = _unitOfWork.Repository<PhysicalActivity>().GetAll().Where(pa => pa.ChildId == childId)
;
            return lol.ToList();
        }

        public async Task<PhysicalActivity> AddPhysicalActivity(PhysicalActivity activity)
        {
            await _unitOfWork.Repository<PhysicalActivity>().AddAsync(activity);
            await _unitOfWork.SaveChangesAsync();
            return activity;
        }
        public async Task BookAppointment(Appointment activity)
        {
            await _unitOfWork.Repository<Appointment>().AddAsync(activity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Child>> GetUserChildren(string userId)
        {
            var childs = await _unitOfWork.Repository<Child>().GetAll().Where(c => c.UserId == userId)
                .Include(c => c.Measurements)
                .Include(c => c.HealthMetrics)
                .Include(c => c.PhysicalActivities)
                .Include(c => c.SleepPatterns)
                .ToListAsync();
            return childs;

                }
        public async Task<List<Measurement>> GetChildMeasurements(Guid childId)
        {
            var list = _unitOfWork.Repository<Measurement>().GetAll().Where(m => m.ChildId == childId);

            return list.ToList();
        }

        public async Task<Measurement> AddMeasurement(Measurement measurement)
        {
            await measurementInputService.SaveAsync(measurement.ChildId, new Service.DTOs.MeasurementInputDto { HeadCircumference = measurement.HeadCircumference,Height = measurement.Height,
            Weight = measurement.Weight});
            return measurement;
        }

        private void SeedInitialAvailabilityData()
        {
            var availabilityRepo = _unitOfWork.Repository<ExpertAvailability>();
            var expertRepo = _unitOfWork.Repository<Expert>();

            if (!availabilityRepo.GetAll().Any())
            {
                var experts = expertRepo.GetAll().ToList();
                if (!experts.Any()) return;

                foreach (var expert in experts)
                {
                    var availabilities = new List<ExpertAvailability>
                    {
                        new ExpertAvailability
                        {
                            Id = Guid.NewGuid(),
                            StartTime = TimeSpan.FromHours(9),
                            EndTime = TimeSpan.FromHours(12),
                            DayOfWeek = "Monday",
                            Status = "Available",
                            Expert = expert
                        },
                        new ExpertAvailability
                        {
                            Id = Guid.NewGuid(),
                            StartTime = TimeSpan.FromHours(14),
                            EndTime = TimeSpan.FromHours(17),
                            DayOfWeek = "Wednesday",
                            Status = "Available",
                            Expert = expert
                        },
                        new ExpertAvailability
                        {
                            Id = Guid.NewGuid(),
                            StartTime = TimeSpan.FromHours(10),
                            EndTime = TimeSpan.FromHours(15),
                            DayOfWeek = "Friday",
                            Status = "Available",
                            Expert = expert
                        }
                    };

                    availabilityRepo.AddRangeAsync(availabilities);
                }
                _unitOfWork.Complete();
            }
        }

    }


}
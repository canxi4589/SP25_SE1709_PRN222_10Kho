using CCP.Repositori.Entities;
using CCP.Repositori.Repository;
using CCP.Service.DTOs;
using CCP.Service.Integration.BlobStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IBlobStorageService _blobStorageService; // Add this
        private readonly UserManager<AppUser> _userManager;

        public ExpertService(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
            _userManager = userManager;
        }

        public async Task<ExpertDTO> GetExpertByIdAsync(string id)
        {
            var expert = await _unitOfWork.Repository<Expert>().GetAll().FirstOrDefaultAsync(e => e.Id.ToString().Equals(id));
            return new ExpertDTO
            {
                Id = expert.Id,
                Name = expert.Name,
                Price = expert.Price,
                Specialty = expert.Specialty.Name,
                ContactInfo = expert.ContactInfo,
                CertificateUrl = expert.Certificate
            };
        }

        public async Task<IEnumerable<ExpertDTO>> GetExpertsAsync()
        {
            var experts = await _unitOfWork.Repository<Expert>().GetAll().ToListAsync();
            return experts.Select(e => new ExpertDTO
            {
                Name = e.Name,
                Price = e.Price,
                ContactInfo = e.ContactInfo,
                CertificateUrl = e.Certificate
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
                CertificateUrl = e.Certificate
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
        public async Task<ExpertDTO> UpdateExpertAsync(ExpertDTO expertDto, IFormFile certificateFile = null)
        {
            var expert = await _unitOfWork.Repository<Expert>()
                .GetAll()
                .FirstOrDefaultAsync(e => e.Id == expertDto.Id);

            if (expert == null)
            {
                throw new Exception("Expert not found");
            }

            expert.Name = expertDto.Name;
            expert.Price = expertDto.Price;
            expert.ContactInfo = expertDto.ContactInfo;

            // Handle certificate upload if provided
            if (certificateFile != null)
            {
                string certificateUrl = await _blobStorageService.UploadFileAsync(certificateFile);
                expert.Certificate = certificateUrl; // Store the URL in the Certificate property
            }

            _unitOfWork.Repository<Expert>().Update(expert);
            await _unitOfWork.Complete();

            return new ExpertDTO
            {
                Id = expert.Id,
                Name = expert.Name,
                Price = expert.Price,
                Specialty = expert.Specialty.Name,
                ContactInfo = expert.ContactInfo,
                CertificateUrl = expert.Certificate,
                UserId = expert.UserId
            };
        }
        public async Task<ExpertDTO> GetExpertByUserIdAsync(string userId)
        {
            var expert = await _unitOfWork.Repository<Expert>()
                .GetAll()
                .Include(e => e.Specialty)
                .FirstOrDefaultAsync(e => e.UserId == userId);

            if (expert == null)
                return null;

            return new ExpertDTO
            {
                Id = expert.Id,
                Name = expert.Name,
                Price = expert.Price,
                Specialty = expert.Specialty?.Name ?? "Not specified",
                ContactInfo = expert.ContactInfo,
                CertificateUrl = expert.Certificate,
                UserId = expert.UserId
            };
        }
        public async Task<ExpertDTO> UpdateExpertAsync(ExpertDTO expertDto, byte[] fileContent, string fileName, string contentType)
        {
            var expert = await _unitOfWork.Repository<Expert>()
        .GetAll()
        .FirstOrDefaultAsync(e => e.Id == expertDto.Id);

            if (expert == null)
            {
                throw new Exception("Expert not found");
            }

            expert.Name = expertDto.Name;
            expert.Price = expertDto.Price;
            expert.ContactInfo = expertDto.ContactInfo;

            // Handle certificate upload if provided
            if (fileContent != null && !string.IsNullOrEmpty(fileName))
            {
                string certificateUrl = await _blobStorageService.UploadFileAsync(fileContent, fileName, contentType);
                expert.Certificate = certificateUrl;
            }

            _unitOfWork.Repository<Expert>().Update(expert);
            await _unitOfWork.Complete();

            return new ExpertDTO
            {
                Id = expert.Id,
                Name = expert.Name,
                Price = expert.Price,
                Specialty = expert.Specialty.Name,
                ContactInfo = expert.ContactInfo,
                CertificateUrl = expert.Certificate,
                UserId = expert.UserId
            };
        }
        public async Task<List<AppointmentHistoryDTO>> GetExpertAppointmentsAsync(Guid expertId)
        {
            var appointmentWithChild = await _unitOfWork.Repository<Appointment>()
           .GetAll()
           .Where(a => a.ExpertId == expertId)
           .Join(
               _unitOfWork.Repository<Child>().GetAll(),
               appointment => appointment.ChildId,
               child => child.Id,
               (appointment, child) => new
               {
                   Appointment = appointment,
                   ChildName = child.Name
               }
           )
       .ToListAsync();

            // Step 2: Fetch all AppUsers (parents) in one go to avoid multiple database calls
            var parentIds = appointmentWithChild.Select(a => a.Appointment.ParentId).Distinct().ToList();
            var parents = await _userManager.Users
                .Where(u => parentIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.FullName);

            // Step 3: Map to AppointmentHistoryDTO, including ParentName from the dictionary
            var appointments = appointmentWithChild.Select(combined => new AppointmentHistoryDTO
            {
                Id = combined.Appointment.Id,
                ParentId = combined.Appointment.ParentId,
                ParentName = parents.ContainsKey(combined.Appointment.ParentId) ? parents[combined.Appointment.ParentId] : "Unknown",
                ChildId = combined.Appointment.ChildId,
                ChildName = combined.ChildName,
                ExpertId = combined.Appointment.ExpertId,
                StartTime = combined.Appointment.StartTime,
                EndTime = combined.Appointment.EndTime,
                BookingDate = combined.Appointment.BookingDate,
                Status = combined.Appointment.Status,
                Price = combined.Appointment.Price
            }).ToList();

            return appointments;
        }
        public async Task<ExpertDTO> AddExpertAsync(AddExpertDTO addExpertDto)
        {
            try
            {
                // Validate SpecialtyId
                var specialty = await _unitOfWork.Repository<Specialty>()
                    .GetAll()
                    .FirstOrDefaultAsync(s => s.Id == addExpertDto.SpecialtyId);
                if (specialty == null)
                {
                    throw new Exception($"Specialty with ID '{addExpertDto.SpecialtyId}' does not exist.");
                }

                // Check if the email already exists
                var existingUser = await _userManager.FindByEmailAsync(addExpertDto.Email);
                if (existingUser != null)
                {
                    throw new Exception($"A user with the email '{addExpertDto.Email}' already exists.");
                }

                // Create a new user account for the expert
                var user = new AppUser
                {
                    UserName = addExpertDto.Email,
                    Email = addExpertDto.Email,
                    FullName = addExpertDto.FullName, // Set the FullName
                    EmailConfirmed = true,
                    IsActive = true, // Set default values for other required fields
                    RegistrationDate = DateTime.UtcNow
                    // LastLogin and DateOfBirth can remain null as they are nullable
                };

                var result = await _userManager.CreateAsync(user, addExpertDto.Password);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                await _userManager.AddToRoleAsync(user, "Expert");

                // Create the expert entity
                var expert = new Expert
                {
                    Name = addExpertDto.Name,
                    SpecialtyId = addExpertDto.SpecialtyId,
                    ContactInfo = addExpertDto.ContactInfo,
                    Price = addExpertDto.Price,
                    UserId = user.Id,
                    Certificate = null
                };

                await _unitOfWork.Repository<Expert>().AddAsync(expert);
                await _unitOfWork.Complete();

                // Add availability slots
                if (addExpertDto.Availabilities != null && addExpertDto.Availabilities.Any())
                {
                    foreach (var availabilityDto in addExpertDto.Availabilities)
                    {
                        var availability = new ExpertAvailability
                        {
                            Expert = expert,
                            DayOfWeek = availabilityDto.DayOfWeek,
                            StartTime = availabilityDto.StartTime,
                            EndTime = availabilityDto.EndTime,
                            Status = availabilityDto.Status ?? "Available"
                        };
                        await _unitOfWork.Repository<ExpertAvailability>().AddAsync(availability);
                    }
                    await _unitOfWork.Complete();
                }

                return new ExpertDTO
                {
                    Id = expert.Id,
                    Name = expert.Name,
                    Specialty = specialty.Name,
                    ContactInfo = expert.ContactInfo,
                    CertificateUrl = expert.Certificate,
                    Price = expert.Price,
                    UserId = expert.UserId
                };
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception($"Error adding expert: {ex.Message}. Inner exception: {ex.InnerException.Message}", ex);
                }
                throw;
            }
        }

    }
}

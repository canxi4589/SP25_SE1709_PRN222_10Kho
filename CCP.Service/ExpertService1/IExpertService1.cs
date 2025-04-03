using CCP.Repositori.Entities;

namespace CCP.Services
{
    public interface IExpertService1
    {
        Task<List<ExpertAvailability>> GetExpertAvailabilities(string expertId);
        Task<Expert> GetExpertByUserId(string userId);
        Task<List<Child>> GetUserChildren(string userId);
        Task<Child> AddChild(Child child);
        Task<List<PhysicalActivity>> GetChildPhysicalActivities(Guid childId);
        Task<PhysicalActivity> AddPhysicalActivity(PhysicalActivity activity);
        Task BookAppointment(Appointment activity);
        Task<List<Measurement>> GetChildMeasurements(Guid childId);
        Task<Measurement> AddMeasurement(Measurement measurement);
    }
}
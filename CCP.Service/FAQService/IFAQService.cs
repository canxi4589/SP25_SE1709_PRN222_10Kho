using CCP.Service.DTOs;

namespace CCP.Service.FAQService
{
    public interface IFAQService
    {
        Task<List<FAQDTO>> GetFAQ();
    }
}
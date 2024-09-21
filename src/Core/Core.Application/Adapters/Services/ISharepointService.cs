using System.Net.Mail;

namespace AutoMais.Ticket.Core.Application.Adapters.Services
{
    public interface ISharepointService
    {
        Task<IEnumerable<MailAddress>> GetAdminEmails(IEnumerable<string> applications = null);
        Task<string> GetRole(string application, string department, string division, string position);
    }
}

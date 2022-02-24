namespace TicketManagementSystem.Domain.Clients
{
    public interface IEmailClient
    {
        void SendEmailToAdministrator(string incidentTitle, string assignedTo);
    }
}

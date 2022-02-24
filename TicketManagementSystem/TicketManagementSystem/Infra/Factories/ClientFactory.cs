using EmailService;
using TicketManagementSystem.Domain.Clients;
using TicketManagementSystem.Infra.Clients;

namespace TicketManagementSystem.Infra.Factories
{
    public static class ClientFactory
    {
        public static IEmailClient CreateEmailClient()
        {
            return new EmailClient(new EmailServiceProxy());
        }
    }
}

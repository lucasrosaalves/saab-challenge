using EmailService;
using System;
using TicketManagementSystem.Domain.Clients;

namespace TicketManagementSystem.Infra.Clients
{
    public class EmailClient : IEmailClient
    {
        private readonly EmailServiceProxy _emailServiceProxy;

        public EmailClient(EmailServiceProxy emailServiceProxy)
        {
            _emailServiceProxy = emailServiceProxy ?? throw new ArgumentNullException(nameof(emailServiceProxy));
        }

        public void SendEmailToAdministrator(string incidentTitle, string assignedTo)
        {
            _emailServiceProxy.SendEmailToAdministrator(incidentTitle, assignedTo);
        }
    }
}

using Moq;
using System;
using TicketManagementSystem;
using TicketManagementSystem.Domain.Clients;
using TicketManagementSystem.Domain.Exceptions;
using TicketManagementSystem.Domain.Repositories;
using Xunit;

namespace TicketManagementSystemUnitTests
{
    public class TicketServiceTests
    {
        [Fact]
        public void CreateTicket_HandleInvalidParams()
        {
            var ticketRepository = new Mock<ITicketRepository>();
            var userRepository = new Mock<IUserRepository>();
            var emailClient = new Mock<IEmailClient>();

            var service = new TicketService(ticketRepository.Object, userRepository.Object, emailClient.Object);

            Assert.Throws<InvalidTicketException>(() => service.CreateTicket(null, Priority.Low, null, null, DateTime.Now, false));
        }
    }
}

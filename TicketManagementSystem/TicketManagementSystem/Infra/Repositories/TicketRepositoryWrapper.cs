using TicketManagementSystem.Domain.Entities;
using TicketManagementSystem.Domain.Repositories;

namespace TicketManagementSystem.Infra.Repositories
{
    public class TicketRepositoryWrapper : ITicketRepository
    {
        public int CreateTicket(Ticket ticket)
        {
            return TicketRepository.CreateTicket(ticket);
        }

        public Ticket GetTicket(int id)
        {
            return TicketRepository.GetTicket(id);
        }

        public void UpdateTicket(Ticket ticket)
        {
            TicketRepository.UpdateTicket(ticket);
        }
    }
}

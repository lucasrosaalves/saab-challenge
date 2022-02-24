using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Domain.Repositories
{
    public interface ITicketRepository
    {
        int CreateTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        Ticket GetTicket(int id);
    }
}

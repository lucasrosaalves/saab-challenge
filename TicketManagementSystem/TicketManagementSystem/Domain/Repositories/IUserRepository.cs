using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Domain.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username);
        User GetAccountManager();
    }
}

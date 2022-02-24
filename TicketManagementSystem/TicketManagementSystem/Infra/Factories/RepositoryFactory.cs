using TicketManagementSystem.Domain.Repositories;
using TicketManagementSystem.Infra.Repositories;

namespace TicketManagementSystem.Infra.Factories
{
    public static class RepositoryFactory
    {
        public static ITicketRepository CreateTicketRepository()
        {
            return new TicketRepositoryWrapper();
        }
        public static IUserRepository CreateUserRepository()
        {
            return new UserRepository();
        }
    }
}

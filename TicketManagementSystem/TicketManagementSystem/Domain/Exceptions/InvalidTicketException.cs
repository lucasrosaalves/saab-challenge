using System;

namespace TicketManagementSystem.Domain.Exceptions
{
    public class InvalidTicketException : Exception
    {
        public InvalidTicketException(string message) : base(message)
        {
        }
    }
}

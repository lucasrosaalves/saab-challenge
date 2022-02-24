using System;
using System.Collections.Generic;
using System.Linq;
using TicketManagementSystem.Domain.Clients;
using TicketManagementSystem.Domain.Entities;
using TicketManagementSystem.Domain.Exceptions;
using TicketManagementSystem.Domain.Repositories;
using TicketManagementSystem.Infra.Factories;

namespace TicketManagementSystem
{
    public class TicketService
    {
        private static readonly List<string> _targetTitles = new() { "Crash", "Important", "Failure" };

        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailClient _emailClient;

        public TicketService()
        {
            _ticketRepository = RepositoryFactory.CreateTicketRepository();
            _userRepository = RepositoryFactory.CreateUserRepository();
            _emailClient = ClientFactory.CreateEmailClient();
        }

        public TicketService(ITicketRepository ticketRepository, IUserRepository userRepository, IEmailClient emailClient)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
        }

        public int CreateTicket(string title, Priority priority, string assignedTo, string description, DateTime date, bool isPayingCustomer)
        {
            MakeCreateTicketValidation(title, assignedTo, description);

            User user = GetUser(assignedTo);

            priority = DefinePriority(priority, title, date);

            if (priority == Priority.High)
            {
                _emailClient.SendEmailToAdministrator(title, assignedTo);
            }

            double price = 0;
            User accountManager = null;
            if (isPayingCustomer)
            {
                // Only paid customers have an account manager.
                accountManager = _userRepository.GetAccountManager();
                price = priority == Priority.High ? 100 : 50;
            }

            return CreateTicket(title, priority, description, date, user, price, accountManager);
        }
        public void AssignTicket(int id, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new UnknownUserException("User not found");
            }

            User user = GetUser(username);
            Ticket ticket = GetTicket(id);

            ticket.AssignedUser = user;

            _ticketRepository.UpdateTicket(ticket);
        }

        private Priority DefinePriority(Priority priority , string title, DateTime date)
        {
            bool hasTargetPeriod = date < DateTime.UtcNow - TimeSpan.FromHours(1);
            bool hasTargetTitle = _targetTitles.Any(t => title.Contains(t));

            if(!hasTargetPeriod && !hasTargetTitle)
            {
                return priority;
            }
            else if (priority == Priority.Low)
            {
                return Priority.Medium;
            }
            else if (priority == Priority.Medium)
            {
                return Priority.High;
            }

            return priority;
        }

        private int CreateTicket(string title, Priority priority, string description, DateTime date, User user, double price, User accountManager)
        {
            var ticket = new Ticket()
            {
                Title = title,
                AssignedUser = user,
                Priority = priority,
                Description = description,
                Created = date,
                PriceDollars = price,
                AccountManager = accountManager
            };

            return _ticketRepository.CreateTicket(ticket);
        }

        private User GetUser(string assignedTo)
        {
            User user = _userRepository.GetUser(assignedTo); ;

            if (user == null)
            {
                throw new UnknownUserException("User not found");
            }

            return user;
        }

        private void MakeCreateTicketValidation(string title, string assignedTo, string description)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                throw new InvalidTicketException("Title or description were null");
            }

            if (assignedTo is null)
            {
                throw new UnknownUserException("AssignedTo was null");
            }
        }

        private Ticket GetTicket(int id)
        {
            var ticket = _ticketRepository.GetTicket(id);

            if (ticket is null)
            {
                throw new ApplicationException("No ticket found for id " + id);
            }

            return ticket;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DelatorreStore.Domain.StoreContext.Entities;
using DelatorreStore.Domain.StoreContext.Queries;
using DelatorreStore.Domain.StoreContext.Repositories;
using DelatorreStore.Infra.StoreContext.DataContexts;

namespace DelatorreStore.Infra.StoreContext.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DelatorreDataContext _context;

        public CustomerRepository(DelatorreDataContext context)
        {
            _context = context;
        }

        public bool CheckDocument(string document)
        {
            return _context
                .Connection
                .Query<bool>(
                    "spCheckDocument",
                    new { Document = document},
                    commandType: CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public bool CheckEmail(string email)
        {
            return _context
                .Connection
                .Query<bool>(
                    "spCheckEmail",
                    new { Email = email },
                    commandType: CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public void Save(Customer customer)
        {
            _context.Connection.Execute("spCreateCustomer",
            new
            {
                Id = customer.Id,
                FirstName = customer.Name.FirstName,
                LastName = customer.Name.LastName,
                Document = customer.Document.Number,
                Email = customer.Email.Address,
                Phone = customer.Phone
            }, commandType: CommandType.StoredProcedure);

            foreach (var address in customer.Addresses)
            {
                _context.Connection.Execute("spCreateAddress",
                new
                {
                    Id = address.Id,
                    CustomerId = customer.Id,
                    Number = address.Number,
                    Complement = address.Complement,
                    District = address.District,
                    City = address.City,
                    State = address.State,
                    Country = address.Country,
                    ZipCode = address.ZipCode,
                    Type = address.Type
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            return _context
                .Connection
                .Query<CustomerOrdersCountResult>(
                    "spGetCustomerOrdersCount",
                    new { Document = document },
                    commandType: CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return _context
                .Connection
                .Query<ListCustomerQueryResult>(
                    "SELECT [Id], CONCAT([FistName], ' ', [LastName]) AS [Name], [Document], [Email] FROM [Customer]"
                );
        }

        public GetCustomerQueryResult GetById(Guid id)
        {
            return _context
                .Connection
                .Query<GetCustomerQueryResult>(
                    "SELECT [Id], CONCAT([FistName], ' ', [LastName]) AS [Name], [Document], [Email] FROM [Customer] WHERE [Id] = @Id",
                    new { Id = id}
                ).FirstOrDefault();
        }

        public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
        {
            return _context
                .Connection
                .Query<ListCustomerOrdersQueryResult>(
                    ""
                );
        }
    }
}
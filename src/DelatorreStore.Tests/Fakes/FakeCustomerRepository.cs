using System.Collections.Generic;
using DelatorreStore.Domain.StoreContext.Entities;
using DelatorreStore.Domain.StoreContext.Queries;
using DelatorreStore.Domain.StoreContext.Repositories;

namespace DelatorreStore.Tests.Fakes
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public bool CheckDocument(string document)
        {
            return false;
        }

        public bool CheckEmail(string email)
        {
            return false;
        }

        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return null;
        }

        public CustomerOrdersCountResult GetCustomerOrdersCount(string document)
        {
            return null;
        }

        public void Save(Customer customer)
        {
            
        }
    }
}
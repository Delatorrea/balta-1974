using System;
using System.Collections.Generic;
using DelatorreStore.Domain.StoreContext.Entities;
using DelatorreStore.Domain.StoreContext.Queries;

namespace DelatorreStore.Domain.StoreContext.Repositories
{
    public interface ICustomerRepository
    {
         bool CheckDocument(string document);
         bool CheckEmail(string email);
         void Save(Customer customer);
         CustomerOrdersCountResult GetCustomerOrdersCount(string document);
         IEnumerable<ListCustomerQueryResult> Get();
         GetCustomerQueryResult GetById(Guid id);
         IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id);
    } 
}
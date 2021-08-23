using System;
using System.Collections.Generic;
using DelatorreStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using DelatorreStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using DelatorreStore.Domain.StoreContext.Entities;
using DelatorreStore.Domain.StoreContext.Handlers;
using DelatorreStore.Domain.StoreContext.Queries;
using DelatorreStore.Domain.StoreContext.Repositories;
using DelatorreStore.Domain.StoreContext.ValueObjects;
using DelatorreStore.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DelatorreStore.API.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;
        private readonly CustomerHandler _handler;


        public CustomerController(ICustomerRepository repository, CustomerHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("v1/customers")]
        [ResponseCache(Duration = 60)]
        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return _repository.Get();
        }

        [HttpGet]
        [Route("v1/customers/{id}")]
        public GetCustomerQueryResult GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        [HttpGet]
        [Route("v1/customers/{id}/orders")]
        public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
        {
            return _repository.GetOrders(id);
        }

        [HttpPost]
        [Route("v1/customers")]
        public ICommandResult Post([FromBody] CreateCustomerCommand command)
        {
            var result = (CreateCustomerCommandResult)_handler.Handle(command);
            return result;
        }

        [HttpPut]
        [Route("v1/customers/{id}")]
        public Customer Put([FromBody] CreateCustomerCommand command)
        {
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);
            var customer = new Customer(name, document, email, command.Phone);

            return customer;
        }

        [HttpDelete]
        [Route("v1/customers/{id}")]
        public object Delete()
        {
            return new { message = "Cliente removido com sucesso!"};
        }
    }
}
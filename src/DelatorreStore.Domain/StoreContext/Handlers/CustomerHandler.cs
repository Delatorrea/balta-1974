using System;
using DelatorreStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using DelatorreStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using DelatorreStore.Domain.StoreContext.Entities;
using DelatorreStore.Domain.StoreContext.Repositories;
using DelatorreStore.Domain.StoreContext.Services;
using DelatorreStore.Domain.StoreContext.ValueObjects;
using DelatorreStore.Shared.Commands;
using FluentValidator;

namespace DelatorreStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler : 
        Notifiable, 
        ICommandHandler<CreateCustomerCommand>,
        ICommandHandler<AddAddressCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly IEmailService _emailService;

        public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
        {
            _emailService = emailService;
            _repository = repository;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            // Verificar se CPF existe na base
            if (_repository.CheckDocument(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se e-mail existe na base
            if (_repository.CheckEmail(command.Email))
                AddNotification("Email", "Este Email já está em uso");

            // Crias VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            // Criar Entidades
            var customer = new Customer(name, document, email, command.Phone);

            // Validar entidades e VOs
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if(Invalid)
                return null;

            // Persistir o cliente
            _repository.Save(customer);

            // Enviar e-mail de boas vindas
            _emailService.Send(email.Address, "emerson@delatorre.dev", "Bem vindo!", "Seja bem vindo ao Delatorre Store!");

            // Retornar o resultado para a tela
            return new CreateCustomerCommandResult(customer.Id, name.ToString(), email.Address);
        }

        public ICommandResult Handle(AddAddressCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
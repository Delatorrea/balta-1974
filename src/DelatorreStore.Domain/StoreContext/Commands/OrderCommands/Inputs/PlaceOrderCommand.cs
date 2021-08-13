using System;
using System.Collections.Generic;
using DelatorreStore.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;

namespace DelatorreStore.Domain.StoreContext.Commands.OrderCommands.Inputs
{
    public class PlaceOrderCommand: Notifiable, ICommand
    {
        public PlaceOrderCommand()
        {
            OrderItems = new List<OrderItemCommand>();
        }

        public Guid Customer { get; set; }
        public IList<OrderItemCommand> OrderItems { get; set; }

        public bool IsValid()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasLen(Customer.ToString(), 36, "Customer", "Identificador do cliente inválido")
                .IsGreaterThan(OrderItems.Count, 0, "Items", "Nenhum item do pedido foi encontrado")
            );
            return Valid;
        }
    }

    public class OrderItemCommand
    {
        public Guid Product { get; set; }
        public decimal Quantity { get; set; }
    }
}
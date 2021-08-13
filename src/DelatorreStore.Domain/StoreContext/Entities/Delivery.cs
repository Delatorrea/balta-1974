using DelatorreStore.Domain.StoreContext.Enums;
using FluentValidator;
using System;

namespace DelatorreStore.Domain.StoreContext.Entities
{
    public class Delivery : Notifiable
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.Waiting;
        }

        public DateTime CreateDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public EDeliveryStatus Status { get; private set; }

        public void Ship()
        {
            // Se a data estimada de entrega for no passado não entregar
            Status = EDeliveryStatus.Shipped;
        }

        public void Cancel()
        {
            // Se status Entregue, não pode cancelar
            Status = EDeliveryStatus.Canceled;
        }
    }
}
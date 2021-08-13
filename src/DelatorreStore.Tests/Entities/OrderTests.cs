using DelatorreStore.Domain.StoreContext.Entities;
using DelatorreStore.Domain.StoreContext.Enums;
using DelatorreStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DelatorreStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Customer _customer;
        private Order _order;
        private Product _mouse;
        private Product _keyboard;
        private Product _chair;
        private Product _monitor;

        public OrderTests()
        {
            var name = new Name("Emerson", "Delatorre");
            var document = new Document("46718115533");
            var email = new Email("emerson@delatorre.dev");

            _customer = new Customer(name, document, email, "5521980231818");
            _order = new Order(_customer);

            _mouse = new Product("Mouse", "Mouse", "image.png", 100M, 10);
            _keyboard = new Product("Keyboard", "Keyboard", "image.png", 100M, 10);
            _chair = new Product("Chair", "Chair", "image.png", 100M, 10);
            _monitor = new Product("Monitor", "Monitor", "image.png", 100M, 10);
        }
        
        [TestMethod]
        public void Deve_Criar_Pedido_Quando_Valido()
        {
            Assert.AreEqual(true, _order.Valid);
        }

        [TestMethod]
        public void Deve_Ser_Status_Criado_Quando_Novo_Pedido()
        {
            Assert.AreEqual(EOrderStatus.Created, _order.Status);
        }

        [TestMethod]
        public void Deve_Retornar_Dois_Quando_Adicionado_Dois_Itens()
        {
            _order.AddItem(_monitor,5);
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(2, _order.Items.Count);
        }

        [TestMethod]
        public void Deve_Retornar_Cinco_Quando_Comprado_Cinco_Itens()
        {
            _order.AddItem(_mouse, 5);
            Assert.AreEqual(_mouse.QuantityOnHand, 5);
        }

        [TestMethod]
        public void Deve_Retornar_Numero_Quando_Confirmar_Pedido()
        {
            _order.Place();
            Assert.AreNotEqual("",_order.Number);
        }

        [TestMethod]
        public void Deve_Retornar_Pago_Quando_Pedido_For_Pago()
        {
            _order.Pay();
            Assert.AreEqual(EOrderStatus.Paid, _order.Status);
        }

        [TestMethod]
        public void Deve_Retornar_Dois_Quando_Comprado_Dez_Produtos()
        {
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.Ship();

            Assert.AreEqual(2, _order.Deliveries.Count);
        }

        [TestMethod]
        public void Deve_Retornar_Status_Cancelado_Quando_Cancelar_Pedido()
        {
            _order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        [TestMethod]
        public void Deve_Cancelar_As_Entregas_Quando_Pedido_Cancelado()
        {
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.AddItem(_monitor, 1);
            _order.Ship();

            _order.Cancel();

            foreach (var item in _order.Deliveries)
            {
                Assert.AreEqual(EDeliveryStatus.Canceled, item.Status);   
            }
        }

    }
}
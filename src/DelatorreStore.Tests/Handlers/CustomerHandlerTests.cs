using DelatorreStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using DelatorreStore.Domain.StoreContext.Handlers;
using DelatorreStore.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DelatorreStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        [TestMethod]
        public void Deve_Registrar_Usuario_Quando_Command_For_Valido()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Emerson";
            command.LastName = "Delatorre";
            command.Document = "28659170377";
            command.Email = "emerson@delatorre.dev";
            command.Phone = "2199999999";

            var handler = new CustomerHandler(new FakeCustomerRepository(), new FakeEmailService());
            var result = handler.Handle(command);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(true, handler.Valid);
        }
    }
}
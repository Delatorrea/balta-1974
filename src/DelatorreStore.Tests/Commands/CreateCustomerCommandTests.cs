using DelatorreStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DelatorreStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void Deve_Retornar_Valido_Quando_Command_For_Valido()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Emerson";
            command.LastName = "Delatorre";
            command.Document = "28659170377";
            command.Email = "emerson@delatorre.dev";
            command.Phone = "2199999999";

            Assert.AreEqual(true, command.IsValid());
        }
    }
}
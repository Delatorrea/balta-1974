using DelatorreStore.Domain.StoreContext.Services;

namespace DelatorreStore.Tests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string from, string subject, string body)
        {
            
        }
    }
}
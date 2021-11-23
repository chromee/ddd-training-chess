using Chess.Scripts.Applications.Messages;

namespace Chess.Scripts.Applications.Tests
{
    public class MockMessagePublisher : IMessagePublisher
    {
        public string Message { get; private set; }
        public void ShowMessage(string message) => Message = message;
    }
}

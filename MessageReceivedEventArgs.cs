namespace StarDelegateMiner
{
    public class MessageReceivedEventArgs
    {
        public string Message { get; }

        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }
    }
}
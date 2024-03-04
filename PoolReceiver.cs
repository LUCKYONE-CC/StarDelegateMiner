using StarDelegateMiner.Models;
using System.Net.Sockets;
using System.Text;

namespace StarDelegateMiner
{
    public class PoolReceiver
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private readonly TcpClient _client;
        private readonly NetworkStream _stream;

        public PoolReceiver(Pool pool)
        {
            //_client = new TcpClient(pool.BaseAddress, pool.Port);
            //_stream = _client.GetStream();
        }

        public async Task StartListeningAsync()
        {
            try
            {
                while (true)
                {
                    OnMessageReceived("Mining...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        protected virtual void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }
    }
}
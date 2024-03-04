using StarDelegateMiner.Miner;
using StarDelegateMiner.Models;

namespace StarDelegateMiner
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Pool pool = new Pool("pool.eu.woolypooly.com", 3124, "");

            var poolReceiver = new PoolReceiver(pool);
            poolReceiver.MessageReceived += Receiver_MessageReceived;

            var miner = new NexaMiner(pool, poolReceiver);

            miner.StartMiningTaskAsync();

            await Task.Delay(5000);
        }

        private static void Receiver_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine($"Received message: {e.Message}");
        }
    }
}
using StarDelegateMiner.Models;

namespace StarDelegateMiner.Miner
{
    /// <summary>
    /// Represents a specific implementation of a miner, for example, for a particular cryptocurrency.
    /// This class should implement the specific mining logic in StartMiningAsync and any cleanup logic in StopMiningAsync.
    /// </summary>
    public class NexaMiner : Miner
    {
        public NexaMiner(Pool pool, PoolReceiver poolReceiver) : base(pool, poolReceiver) { }

        protected override async Task<ComputationResult> Compute(MessageReceivedEventArgs messageReceivedEventArgs)
        {
            return new ComputationResult();
        }

        protected override async Task SubmitWork(ComputationResult computationResult)
        {
            Console.WriteLine("Submitted work!");
        }
    }
}
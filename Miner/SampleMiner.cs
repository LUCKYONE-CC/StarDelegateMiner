using StarDelegateMiner.Models;

namespace StarDelegateMiner.Miner
{
    /// <summary>
    /// Represents a specific implementation of a miner, for example, for a particular cryptocurrency.
    /// This class should implement the specific mining logic in StartMiningAsync and any cleanup logic in StopMiningAsync.
    /// </summary>
    public class SampleMiner : Miner
    {
        public SampleMiner(Pool pool, PoolHandler.PoolHandler poolHandler) : base(pool, poolHandler) { }

        protected override async Task<ComputationResult> Compute(MessageReceivedEventArgs messageReceivedEventArgs)
        {
            return new ComputationResult();
        }
    }
}
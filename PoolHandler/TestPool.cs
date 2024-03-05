using StarDelegateMiner.Models;

namespace StarDelegateMiner.PoolHandler
{
    public class TestPool : PoolHandler
    {
        public TestPool(Pool pool) : base(pool) { }
        public override Task OnStart()
        {
            while (true)
            {
                OnMessageReceived("Mining...");
                Thread.Sleep(1000);
            }
        }

        public override Task SubmitWorkAsync(ComputationResult computationResult)
        {
            throw new NotImplementedException();
        }
    }
}
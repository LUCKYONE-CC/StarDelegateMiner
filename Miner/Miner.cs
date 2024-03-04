using StarDelegateMiner.Models;
using System.Threading;

namespace StarDelegateMiner.Miner
{
    public abstract class Miner
    {
        protected CancellationTokenSource CancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the Miner class.
        /// </summary>
        /// <param name="pool">The mining pool with which this miner is associated.</param>
        /// <param name="poolReceiver">The receiver for handling messages from the mining pool.</param>
        public Miner(Pool pool, PoolReceiver poolReceiver)
        {
            Pool = pool;
            PoolReceiver = poolReceiver;
        }

        public Pool Pool { get; }
        public PoolReceiver PoolReceiver { get; }

        /// <summary>
        /// When implemented in a derived class, computes the result based on the message received from the pool.
        /// </summary>
        /// <param name="messageReceivedEventArgs">Event arguments containing the message received from the pool.</param>
        /// <returns>A task that represents the asynchronous computation operation. The task result contains the computation result.</returns>
        protected abstract Task<ComputationResult> Compute(MessageReceivedEventArgs messageReceivedEventArgs);

        /// <summary>
        /// When implemented in a derived class, submits the computed work back to the pool.
        /// </summary>
        /// <param name="computationResult">The result of the computation that needs to be submitted back to the pool.</param>
        /// <returns>A task that represents the asynchronous submission operation.</returns>
        protected abstract Task SubmitWork(ComputationResult computationResult);

        /// <summary>
        /// Starts the mining task asynchronously. This method sets up the message received event handler
        /// and initiates the mining process.
        /// </summary>
        public void StartMiningTaskAsync()
        {
            CancellationTokenSource = new CancellationTokenSource();
            var token = CancellationTokenSource.Token;
            Task.Run(async () =>
            {
                try
                {
                    PoolReceiver.MessageReceived += OnMessageReceived;
                    await PoolReceiver.StartListeningAsync();
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Mining operation was cancelled.");
                }
            }, token);
        }

        /// <summary>
        /// Requests cancellation of the ongoing mining task. This method triggers the cancellation token
        /// and should lead to a graceful termination of the mining process.
        /// </summary>
        public async Task RequestMiningCancellationAsync()
        {
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Handles the MessageReceived event from the pool receiver. This method is called whenever
        /// a new message is received from the mining pool. It computes the result based on the received message
        /// and submits the work back to the pool.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A MessageReceivedEventArgs that contains the event data.</param>
        private async void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var computationResult = await Compute(e);

            if (computationResult == null)
                throw new InvalidOperationException("Computation result cannot be null.");

            await SubmitWork(computationResult);
        }
    }
}
using StarDelegateMiner.Models;
using System.Net.Sockets;

namespace StarDelegateMiner.PoolHandler
{
    public abstract class PoolHandler
    {
        /// <summary>
        /// Occurs when a message is received from the pool.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// Token source for managing cancellation of the pool tasks.
        /// </summary>
        protected CancellationTokenSource CancellationTokenSource;
        protected Pool Pool;

        /// <summary>
        /// Initializes a new instance of the PoolHandler class with a specified pool.
        /// </summary>
        /// <param name="pool">The pool associated with this handler.</param>
        public PoolHandler(Pool pool)
        {
            CancellationTokenSource = new CancellationTokenSource();
            Pool = pool;
        }

        /// <summary>
        /// Raises the MessageReceived event with the provided message.
        /// </summary>
        /// <param name="message">The message received from the pool.</param>
        protected virtual void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
        }

        /// <summary>
        /// Starts the pool task asynchronously, handling pool messages and other operations.
        /// </summary>
        public void StartPoolTaskAsync()
        {
            var token = CancellationTokenSource.Token;
            Task.Run(async () =>
            {
                try
                {
                    await OnStart();
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Pool handler was cancelled.");
                }
            }, token);
        }

        /// <summary>
        /// Requests cancellation of the current pool task asynchronously.
        /// </summary>
        public void RequestMiningCancellationAsync()
        {
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// When implemented in a derived class, submits work to the pool asynchronously.
        /// </summary>
        /// <param name="computationResult">The result of the computation to submit.</param>
        /// <returns>A task representing the asynchronous operation of submitting work.</returns>
        public abstract Task SubmitWorkAsync(ComputationResult computationResult);

        /// <summary>
        /// When implemented in a derived class, starts the pool's operations asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation of starting the pool.</returns>
        public abstract Task OnStart();
    }
}
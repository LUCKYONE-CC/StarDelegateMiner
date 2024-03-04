namespace StarDelegateMiner.Models
{
    public class Pool
    {
        public Pool(string baseAddress, int port, string walletAddress)
        {
            BaseAddress = baseAddress;
            Port = port;
            WalletAddress = walletAddress;
        }
        public string BaseAddress { get; set; }
        public int Port { get; set; }
        public string WalletAddress { get; set; }
    }
}
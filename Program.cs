﻿using StarDelegateMiner.Miner;
using StarDelegateMiner.Models;
using StarDelegateMiner.PoolHandler;

namespace StarDelegateMiner
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Pool pool = new Pool("pool.eu.woolypooly.com", 3124, "");

            var poolHandler = new TestPool(pool);

            var miner = new NexaMiner(pool, poolHandler);

            miner.StartMiningTaskAsync();

            await Task.Delay(-1);
        }
    }
}
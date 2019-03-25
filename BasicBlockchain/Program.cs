using BBlockchain.Models;
using System;

namespace BBlockchain
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain(2);

            blockchain.AddBlock(blockchain.NewBlock("Hey im the coin market"));
            blockchain.AddBlock(blockchain.NewBlock("Bitcoin next"));
            blockchain.AddBlock(blockchain.NewBlock("http://google.com"));

            blockchain.AddBlock(new Block(4, DateTime.Now.Millisecond, blockchain.Blocks[3].Hash, "Block invalid"));

            Console.WriteLine("Blockchain valid: {0}", blockchain.IsBlockChainValid());
            foreach (var block in blockchain.Blocks)
            {
                Console.WriteLine(block.ToString());
            }
            Console.WriteLine(blockchain.ToString());

            Console.ReadKey();
        }
    }
}

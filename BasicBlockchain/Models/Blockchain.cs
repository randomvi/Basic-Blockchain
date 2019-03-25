using System;
using System.Collections.Generic;
using System.Text;

namespace BBlockchain.Models
{
    class Blockchain
    {
        public int Difficulty { get; set; }
        public List<Block> Blocks { get; set; }

        public Blockchain() {}

        public Blockchain(int difficulty)
        {
            Difficulty = difficulty;

            Blocks = new List<Block>();

            // create the genesis block
            Block b = new Block(0, DateTime.Now.Millisecond, null, "Genesis Block");

            b.MineBlock(difficulty);
            Blocks.Add(b);
        }

        public Block LatestBlock()
        {
            return Blocks[(Blocks.Count - 1)];
        }

        public Block NewBlock(string data)
        {
            Block latestBlock = LatestBlock();

            return new Block(latestBlock.Index + 1, DateTime.Now.Millisecond, latestBlock.Hash, data);
        }

        public void AddBlock(Block b)
        {
            if (b != null)
            {
                b.MineBlock(Difficulty);
                Blocks.Add(b);
            }
        }

        public bool IsFirstBlockValid()
        {
            Block firstBlock = Blocks[0];

            if (firstBlock.Index != 0)
            {
                return false;
            }

            if (firstBlock.PreviousHash != null)
            {
                return false;
            }

            if (firstBlock.Hash == null || !Block.CalculateHash(firstBlock).Equals(firstBlock.Hash))
            {
                return false;
            }

            return true;
        }

        public bool IsValidNewBlock(Block newBlock, Block previousBlock)
        {
            if (newBlock != null && previousBlock != null)
            {
                if (previousBlock.Index + 1 != newBlock.Index)
                {
                    return false;
                }

                if (newBlock.PreviousHash == null ||
                        !newBlock.PreviousHash.Equals(previousBlock.Hash))
                {
                    return false;
                }

                if (newBlock.Hash == null ||
                        !Block.CalculateHash(newBlock).Equals(newBlock.Hash))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool IsBlockChainValid()
        {
            if (!IsFirstBlockValid())
            {
                return false;
            }

            for (int i = 1; i < Blocks.Count; i++)
            {
                Block currentBlock = Blocks[i];
                Block previousBlock = Blocks[(i - 1)];

                if (!IsValidNewBlock(currentBlock, previousBlock))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            //return base.ToString();

            return "Blockchain{" +
                  "difficulty=" + Difficulty +
                  ", blocks=" + Blocks +
                  '}';
        }

    }
}

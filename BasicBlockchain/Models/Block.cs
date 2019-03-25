using BBlockchain.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BBlockchain.Models
{
    public class Block
    {
        public int Index { get; set; }

        public long Timestamp { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }

        public string Data { get; set; }

        public int Nonce { get; set; }

        public Block() { }

        public Block(int index, long timestamp, string previousHash, string data)
        {
            Index = index;
            Timestamp = timestamp;
            PreviousHash = previousHash;
            Data = data;
            Nonce = 0;
            Hash = Block.CalculateHash(this);
        }

        public string Str()
        {
            return Index + Timestamp + PreviousHash + Data + Nonce;
        }

        public override string ToString()
        {
            // return base.ToString();
            return "Block{" +
                         "Index=" + Index +
                         ", Timestamp=" + Timestamp +
                         ", Hash='" + Hash + '\'' +
                         ", PreviousHash='" + PreviousHash + '\'' +
                         ", Data='" + Data + '\'' +
                         ", Nonce=" + Nonce +
                         '}';
        }

        public static string CalculateHash(Block block)
        {

            if (block != null)
            {

                var digest = new SHA1CryptoServiceProvider();
  
                byte[] bytes = digest.ComputeHash(Encoding.ASCII.GetBytes(block.Str()));

                 StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    int c = 0xff & b;
                    string hex = (0xff & b).ToString("X");

                    if (hex.Length == 1)
                    {
                        builder.Append('0');
                    }

                    builder.Append(hex);
                }

                return builder.ToString();
            }

            return null;
        }

        public void MineBlock(int difficulty)
        {
            Nonce = 0;
            while (!Hash.Substring(0, difficulty).Equals(Util.Zeros(difficulty))) ;
            {
                Nonce++;
                Hash = Block.CalculateHash(this);
            }
        }
        
    }
}

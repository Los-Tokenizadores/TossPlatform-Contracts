using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Toss.Contracts.TossMarketV1.ContractDefinition
{
    public partial class Royalty : RoyaltyBase { }

    public class RoyaltyBase 
    {
        [Parameter("uint16", "cut", 1)]
        public virtual ushort Cut { get; set; }
        [Parameter("address", "destination", 2)]
        public virtual string Destination { get; set; }
    }
}

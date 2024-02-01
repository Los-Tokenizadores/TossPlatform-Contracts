using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Toss.Contracts.TossSellerV1.ContractDefinition
{
    public partial class Erc721SellInfo : Erc721SellInfoBase { }

    public class Erc721SellInfoBase 
    {
        [Parameter("uint128", "price", 1)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint8", "maxAmount", 2)]
        public virtual byte MaxAmount { get; set; }
    }
}

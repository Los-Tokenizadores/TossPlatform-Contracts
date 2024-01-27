using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Toss.Contracts.TossInvestV1.ContractDefinition
{
    public partial class ProjectInfo : ProjectInfoBase { }

    public class ProjectInfoBase 
    {
        [Parameter("uint128", "price", 1)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint32", "targetAmount", 2)]
        public virtual uint TargetAmount { get; set; }
        [Parameter("uint32", "maxAmount", 3)]
        public virtual uint MaxAmount { get; set; }
        [Parameter("uint64", "startAt", 4)]
        public virtual ulong StartAt { get; set; }
        [Parameter("uint64", "finishAt", 5)]
        public virtual ulong FinishAt { get; set; }
        [Parameter("uint64", "mintedAt", 6)]
        public virtual ulong MintedAt { get; set; }
        [Parameter("uint32", "lastIndex", 7)]
        public virtual uint LastIndex { get; set; }
        [Parameter("address", "projectWallet", 8)]
        public virtual string ProjectWallet { get; set; }
        [Parameter("bool", "confirmed", 9)]
        public virtual bool Confirmed { get; set; }
        [Parameter("address", "erc721Address", 10)]
        public virtual string Erc721Address { get; set; }
        [Parameter("string", "name", 11)]
        public virtual string Name { get; set; }
        [Parameter("string", "symbol", 12)]
        public virtual string Symbol { get; set; }
    }
}

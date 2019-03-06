using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OvChipApi.Responses
{
    public class TransactionResponse
    {
        public int TotalSize { get; set; }
        public int NextOffset { get; set; }
        public int PreviousOffset { get; set; }
        public bool TransactionsRestricted { get; set; }

        [JsonProperty("nextRequestContext", Required = Required.AllowNull)]
        public Object NextRequestContext { get; set; }

        public List<Transaction> Records { get; set; }
    }

    public class Transaction
    {
        public String CheckInInfo { get; set; }
        public String CheckInText { get; set; }
        public double? Fare { get; set; }
        public String FareCalculation { get; set; }
        public String FareText { get; set; }
        public String ModalType { get; set; }
        public String ProductInfo { get; set; }
        public String ProductText { get; set; }
        public String PTO { get; set; }
        public long TransactionDateTime { get; set; }
        public String TransctionInfo { get; set; }
        public String TransactionName { get; set; }
        public double EPurseMut { get; set; }
        public String EPurseMutInfo { get; set; }
        public String TransactionExplanation { get; set; }
        public String TransactionPriority { get; set; }
    }
}

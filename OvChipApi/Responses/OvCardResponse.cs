using System;
namespace OvChipApi.Responses
{
    public class OvCardResponse
    {
        public String Alias { get; set; }
        public String MediumId { get; set; }
        public int Balance { get; set; }
        public long BalanceDate { get; set; }
        public bool DefaultCard { get; set; }
        public String Status { get; set; }
        public long ExpiryDate { get; set; }
        public bool AutoReloadEnabled { get; set; }
        public String Type { get; set; }
        public String StatusAnnouncement { get; set; }
    }
}

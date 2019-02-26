using System.Runtime.Serialization;

namespace Warehouse
{
    [DataContract]
    public class WareHouseArticleAvailability
    {
        [DataMember]
        public int ArticleNr { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public double Availability { get; set; }
        [DataMember]
        public double PickingAmount { get; set; }
        [DataMember]
        public string Note { get; set; }

    }
}
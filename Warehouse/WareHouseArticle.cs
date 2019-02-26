using System;
using System.Runtime.Serialization;

namespace Warehouse
{
    [DataContract]
    public class WareHouseArticle
    {
        
        public int Id { get; set; }
        [DataMember]

        public int ArticleNr { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public double Availability { get; set; }
        [DataMember]
        public string Unit { get; set; }
        [DataMember]
        public string Note { get; set; }

        public int LineItemId { get; set; }
        public double LineItemAmount { get; set; }
        public string LineItemUnit { get; set; }

        internal static WareHouseArticle GetDefalt()
        {
            return new WareHouseArticle()
            {
                ArticleNr = 0,
                Name = "",
                Description = "",
                Availability = 0,
                Unit = "st",
                Note = ""
            };
        }
    }
}
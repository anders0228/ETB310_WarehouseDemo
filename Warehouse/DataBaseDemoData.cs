using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warehouse
{
    internal class DataBaseDemoData
    {
        public static IEnumerable<WareHouseArticle> GetDemoData()
        {
            return new List<WareHouseArticle>
            {
                new WareHouseArticle
                {
                ArticleNr = 100,
                Name = "Skor",
                Description = "Lågskor i vitt läder. OBS: Rekommenderas inte vid kosläpp!",
                Availability = 4,
                Unit = "par",

                LineItemId = 1100,
                LineItemAmount = 20,
                LineItemUnit = "par",
                },

                new WareHouseArticle
                {
                ArticleNr = 101,
                Name = "Byxor, blå",
                Description = "Vardagsbrallor för den som inte orkar välja.",
                Availability = 4,
                Unit = "par",

                LineItemId =1101 ,
                LineItemAmount = 100,
                LineItemUnit = "par",
                },

                new WareHouseArticle
                {
                ArticleNr = 102,
                Name = "Kaffe - fairtrade, mörkrost",
                Description = "Rätt å gött! ",
                Availability = 20,
                Unit = "pkt",

                LineItemId =1102 ,
                LineItemAmount = 100,
                LineItemUnit = "pkt",
                },

                new WareHouseArticle
                {
                ArticleNr = 103,
                Name = "Dynamit",
                Description = "Inget hem är komplett utan en liten gubbe eller två!",
                Availability = 100,
                Unit = "kg",
                
                LineItemId = 1103,
                LineItemAmount = 100,
                LineItemUnit = "kg",
                }
            };
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Warehouse
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWarehouseService
    {
        [OperationContract]
        IEnumerable<WareHouseArticle> GetAllArticles();

        // =============================================================================
        // Dessa fyra följande metoder gör CRUD (create-read-update-delete) för en artikel
        // De motsvarar också (ungefär) de fyre REST-metoderna GET-POST-PUT-DELETE:

        // GET
        [OperationContract]
        WareHouseArticle GetArticle(int articleNr); 

        // POST
        [OperationContract]
        WareHouseArticle AddArticle(WareHouseArticle article); 

        // PUT
        [OperationContract]
        WareHouseArticle UpdateArticle(WareHouseArticle article); 

        // DELETE
        [OperationContract]
        void DeleteArticle(int articleNr);


        // =============================================================================

        [OperationContract]
        void RebuildDemoDatabase();

        [OperationContract]
        WareHouseArticleAvailability SendPickingOrder(int articleNr, double amount);
    }
}

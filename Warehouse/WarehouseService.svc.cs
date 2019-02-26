using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Warehouse
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IWarehouseService
    {
        WarehouseDbContext _context;
        public Service1()
        {
            _context = new WarehouseDbContext();
            // Om ingen data finns i databasen så fyll på med demodata
            if (_context.WareHouseArticles.Count() == 0)
            {
                RebuildDemoDatabase();
            }

        }

        ~Service1()
        {
            _context.Dispose();
        }

        public IEnumerable<WareHouseArticle> GetAllArticles()
        {
            return _context.WareHouseArticles.ToList();
        }

        // =============================================================================
        // CRUD
        public WareHouseArticle GetArticle(int articleNr)
        {
            // Notering: det där med två frågetecken ?? kallas null coalescing operator
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
            // Om FirstOrDefault inte hittar någon artikel så returneras null, och då
            // kickar ?? in och hämtar ett default-objekt. 
            return _context.WareHouseArticles
                .FirstOrDefault(article => article.ArticleNr == articleNr) ?? WareHouseArticle.GetDefalt();
        }


        public WareHouseArticle AddArticle(WareHouseArticle article)
        {
            // nya artiklar ska alltid få sitt ID från databasen, så Id sätts till 0
            article.Id = 0;
            // Jag väljer att strunta i det angivna artikelnumret
            // och skapa ett nytt garanterat unikt nummer (max+1), så vi slipper 
            // hantera ev. dubblett-fel
            article.ArticleNr = _context
                .WareHouseArticles
                .OrderByDescending(a => a.ArticleNr)
                .FirstOrDefault()
                .ArticleNr + 1;
            // markera att artikeln ska läggas till
            _context.Entry(article).State = EntityState.Added;
            _context.SaveChanges();
            return article;
        }

        public WareHouseArticle UpdateArticle(WareHouseArticle article)
        {
            var dbArticle = GetArticle(article.ArticleNr);
            // om dbArticle.Id är mindre än eller lika med 0 så betyder det 
            // att artikeln inte hittades i databasen. Vi skapar då en ny atikel.
            if (dbArticle.Id <= 0)
            {
                AddArticle(article);
                return article;
            }
            dbArticle.ArticleNr = article.ArticleNr;
            // om article.ArticleNr är negativt så behåll befintlig Availability
            dbArticle.Availability = article.Availability > 0 ? article.Availability : dbArticle.Availability;
            // Notering: det där med två frågetecken ?? kallas null coalescing operator
            // den gör att om någon property är null så behålls befintligt värde i dbArticle
            dbArticle.Description = article.Description ?? dbArticle.Description;
            dbArticle.Name = article.Name ?? dbArticle.Name;
            dbArticle.Note = article.Note ?? dbArticle.Note;
            dbArticle.Unit = article.Unit ?? dbArticle.Unit;

            // markera att artikeln ska ändras
            _context.Entry(dbArticle).State = EntityState.Modified;
            _context.SaveChanges();
            return dbArticle;
        }

        public void DeleteArticle(int articleNr)
        {
            var article = GetArticle(articleNr);
            if (article.Id <= 0)
            {
                return;
            }

            // markera att artikeln ska tas bort
            _context.Entry(article).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        // =============================================================================
        // Övriga funktioner
        public void RebuildDemoDatabase()
        {
            // ta bort alla befintliga artiklar från _context
            _context.WareHouseArticles.RemoveRange(_context.WareHouseArticles);

            // hämta upp demodata
            var input = DataBaseDemoData.GetDemoData();

            // lägg till alla nya artiklar till _context
            foreach (var item in input)
            {
                _context.Entry(item).State = EntityState.Added;
            }

            // utför alla ändringar
            _context.SaveChanges();
        }
        public WareHouseArticleAvailability SendPickingOrder(int articleNr, double amount)
        {
            var note = "";
            // om amount är mindre än noll så sätt amount till noll
            // ser detta konstigt ut? det kallas för "null-conditional operator"
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-conditional-operators
            // https://stackoverflow.com/questions/28352072/what-does-question-mark-and-dot-operator-mean-in-c-sharp-6-0
            amount = amount < 0 ? 0 : amount;

            // se till att inte går att göra plock-order på mer än vad som finns i lager
            var item = _context.WareHouseArticles.First(article => article.ArticleNr == articleNr);
            if (item.Availability - amount < 0)
            {
                //tala om för butiken
                amount = item.Availability;
                note = $"det fanns bara {amount} {item.Unit} kvar i lager. Lagersaldot är dock redan återställt, så beställ igen!";
            };

            // Uppdatera lagersaldo
            item.Availability = item.Availability - amount;

            // Registrera ändringar
            _context.Entry(item).State = EntityState.Modified;

            // Om lager = 0 så beställ mera
            if (item.Availability == 0)
            {
                //beställ!
                var test = new WholesalerServiceReference.LineItem()
                {
                    Id = item.LineItemId,
                    Amount = item.LineItemAmount,
                    Unit = item.LineItemUnit,
                };
                var services = new WholesalerServiceReference.WholesalerServiceClient();
                var response = services.Order(test);
                if (response == "OrderReceived")
                {
                    note += " Beställning har skickats till grossist.";

                    // Uppdatera lagersaldot (Endast för demo. I verkligheten måste man ju invänta leverans ;-)
                    item.Availability = item.Availability + item.LineItemAmount;
                    _context.Entry(item).State = EntityState.Modified;
                    note += " Lagersaldo uppdaterat.";
                }
                else
                {
                    note += "Lyckades inte beställa...";
                }
            }
            _context.SaveChanges();


            return new WareHouseArticleAvailability()
            {
                ArticleNr = articleNr,
                Name = item.Name,
                Unit = item.Unit,
                PickingAmount = amount,
                Availability = GetArticle(articleNr).Availability,
                Note = note

            };
        }

    }
}
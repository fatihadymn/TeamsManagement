using Microsoft.EntityFrameworkCore;
using TeamsManagement.Data;

namespace TeamsManagement
{
    public class Initializer
    {
        public static void InitializeDatabase(IApplicationBuilder application)
        {
            var serviceScope = application.ApplicationServices.CreateScope();

            var provider = serviceScope.ServiceProvider;

            var db = provider.GetService<ApplicationContext>();

            if (db != null)
            {
                db.Database.Migrate();
            }
        }

        public static void InitializeCurrencies(IApplicationBuilder application)
        {
            var serviceScope = application.ApplicationServices.CreateScope();

            var provider = serviceScope.ServiceProvider;

            var db = provider.GetService<ApplicationContext>();

            if (db != null)
            {
                //Random rnd = new Random();

                //var result = new List<DailyRate>();

                //var currencies = Enum.GetNames(typeof(Codes)).ToList();

                //for (int i = 0; i < 5; i++)
                //{
                //    foreach (var currency in currencies)
                //    {
                //        result.Add(new DailyRate()
                //        {
                //            Code = currency,
                //            CreatedOn = new DateTime(2021, 10, 19 + i, 10, 0, 0),
                //            CurrencyName = $"DUMMY - {currency}",
                //            Id = Guid.NewGuid(),
                //            Name = $"DUMMY - {currency}",
                //            Rate = rnd.Next(200, 250),
                //            UpdatedOn = new DateTime(2021, 10, 19 + i, 10, 0, 0),
                //        });
                //    }
                //}

                //db.DailyRates.AddRange(result);

                db.SaveChanges();
            }
        }
    }
}

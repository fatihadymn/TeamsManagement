using Microsoft.EntityFrameworkCore;
using TeamsManagement.Data;
using TeamsManagement.Items.Entities;

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
                List<Team> teams = new List<Team>()
                {
                    new Team()
                    {
                        Id = Guid.NewGuid(),
                        Country = "Netherlands",
                        Name = "AFC Ajax",
                        CreatedOn = DateTime.Now,
                        Players = new List<Player>()
                    },

                    new Team()
                    {
                        Id = Guid.NewGuid(),
                        Country = "Spain",
                        Name = "Real Madrid CF",
                        CreatedOn = DateTime.Now,
                        Players = new List<Player>()
                    }
                };

                db.Teams.AddRange(teams);

                db.SaveChanges();
            }
        }
    }
}

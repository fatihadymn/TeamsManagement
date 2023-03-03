using Microsoft.EntityFrameworkCore;
using TeamsManagement.Data;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Api
{
    public static class Initializer
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

        public static void InitializeTeamsAndPlayers(IApplicationBuilder application)
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
                        {
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Geronimo Rulli",
                                DateOfBirth = DateTime.Parse("05.20.1992"),
                                Height = 189,
                                CreatedOn = DateTime.Now
                            },
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Jurrien Timber",
                                DateOfBirth = DateTime.Parse("06.17.2001"),
                                Height = 182,
                                CreatedOn = DateTime.Now
                            },
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Dusan Tadic",
                                DateOfBirth = DateTime.Parse("11.20.1988"),
                                Height = 181,
                                CreatedOn = DateTime.Now
                            },
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Lorenzo Lucca",
                                DateOfBirth = DateTime.Parse("09.10.2000"),
                                Height = 201,
                                CreatedOn = DateTime.Now
                            },
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Davy Klaassen",
                                DateOfBirth = DateTime.Parse("02.21.1993"),
                                Height = 179,
                                CreatedOn = DateTime.Now
                            }
                        }
                    },

                    new Team()
                    {
                        Id = Guid.NewGuid(),
                        Country = "Spain",
                        Name = "Real Madrid CF",
                        CreatedOn = DateTime.Now,
                        Players = new List<Player>()
                        {
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Thibaut Courtoid",
                                DateOfBirth = DateTime.Parse("05.11.1992"),
                                Height = 200,
                                CreatedOn = DateTime.Now
                            },
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "David Alaba",
                                DateOfBirth = DateTime.Parse("06.24.1992"),
                                Height = 189,
                                CreatedOn = DateTime.Now
                            },
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Luka Modric",
                                DateOfBirth = DateTime.Parse("09.09.1985"),
                                Height = 172,
                                CreatedOn = DateTime.Now
                            },
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Karim Benzema",
                                DateOfBirth = DateTime.Parse("12.19.1987"),
                                Height = 185,
                                CreatedOn = DateTime.Now
                            },
                            new Player()
                            {
                                Id= Guid.NewGuid(),
                                Name = "Vinicius Junior",
                                DateOfBirth = DateTime.Parse("12.07.2000"),
                                Height = 176,
                                CreatedOn = DateTime.Now
                            }
                        }
                    }
                };

                db.Teams.AddRange(teams);

                db.SaveChanges();
            }
        }
    }
}

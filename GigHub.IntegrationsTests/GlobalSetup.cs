using GigHub.Core.Models;
using GigHub.Persistence;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationsTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any()) return;            

            context.Users.Add(new ApplicationUser {UserName = "User1", Name = "User1", Email = "-", PasswordHash = "-"});
            context.Users.Add(new ApplicationUser {UserName = "User2", Name = "User2", Email = "-", PasswordHash = "-"});
            context.SaveChanges();            
        }
    }
}

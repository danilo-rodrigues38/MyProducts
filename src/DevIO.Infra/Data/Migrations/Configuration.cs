using System.Data.Entity.Migrations;
using DevIO.Infra.Data.Context;

namespace DevIO.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}

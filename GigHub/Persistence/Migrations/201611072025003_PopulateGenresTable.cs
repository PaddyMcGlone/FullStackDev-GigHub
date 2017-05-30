namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (1, 'JAZZ')");
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (2, 'BLUES')");
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (3, 'ROCK')");
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (4, 'POP')");
            Sql("INSERT INTO GENRES(ID, NAME) VALUES (5, 'DANCE')");
        }

        public override void Down()
        {
            Sql("DELETE FROM GENRES WHERE ID IN(1,2,3,4)");
        }
    }
}

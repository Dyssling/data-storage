using ConsoleApp.Entities;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ConsoleApp.Repositories
{
    public abstract class Repository<TEntity> where TEntity : class
    {
        public void Create(TEntity entity, string query)
        {
            using var connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Education\data-storage\exercises\c-sharp-sql-client\exercise_database.mdf; Integrated Security = True; Connect Timeout = 30");
            connection.Execute(query, entity);
        }
    }
}

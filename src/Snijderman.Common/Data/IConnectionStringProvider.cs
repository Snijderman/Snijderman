using System.Data;
using System.Threading.Tasks;

namespace Snijderman.Common.Data
{
   public interface IConnectionStringProvider
   {
      /// <summary>
      /// Gets the connection.
      /// </summary>
      /// <returns></returns>
      IDbConnection GetConnection();

      /// <summary>
      /// Gets the connection asynchronous.
      /// </summary>
      /// <returns></returns>
      Task<IDbConnection> GetConnectionAsync();
   }
}

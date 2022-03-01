using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Snijderman.Common.Data;

/// <summary>
/// Base functionality for a query
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseRepository<T> where T : class
{
   /// <summary>
   /// Inserts the specified item.
   /// </summary>
   /// <param name="item">The item.</param>
   /// <returns></returns>
   void Insert(T item);

   /// <summary>
   /// Inserts the specified item async.
   /// </summary>
   /// <param name="item">The item.</param>
   /// <returns></returns>
   Task InsertAsync(T item);

   /// <summary>
   /// Updates the specified item.
   /// </summary>
   /// <param name="item">The item.</param>
   /// <returns></returns>
   void Update(T item);

   /// <summary>
   /// Updates the specified item async.
   /// </summary>
   /// <param name="item">The item.</param>
   /// <returns></returns>
   Task UpdateAsync(T item);

   /// <summary>
   /// Inserts the specified items.
   /// </summary>
   /// <param name="items">The items.</param>
   /// <returns></returns>
   void Insert(IList<T> items);

   /// <summary>
   /// Inserts the specified items async.
   /// </summary>
   /// <param name="items">The items.</param>
   /// <returns></returns>
   Task InsertAsync(IList<T> items);

   /// <summary>
   /// Execute a sql command and return the number of rows affected.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns>The number of rows affected.</returns>
   int ExecuteSql(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Execute a command asynchronously using Task and return the number of rows affected.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns>The number of rows affected.</returns>
   Task<int> ExecuteSqlAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Execute a sql command and return the result in a list of T.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   IList<T> Query(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Execute a sql command asynchronously using Task and return the result in a list of T.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<IList<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query, returning the data typed as T.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   T QuerySingle(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query asynchronously using Task, returning the data typed as T.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<T> QuerySingleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query, returning the data typed as T.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   T QuerySingleOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query asynchronously using Task, returning the data typed as T.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<T> QuerySingleOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query, returning the data typed as T.
   /// </summary>
   /// <param name="cnn"></param>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   T QueryFirst(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query asynchronously using Task, returning the data typed as T.
   /// </summary>
   /// <param name="cnn"></param>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<T> QueryFirstAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query, returning the data typed as T.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   T QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query asynchronously using Task, returning the data typed as T.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<T> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Execute parameterized SQL that selects a single value.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns>The first cell returned, as T.</returns>
   T2 ExecuteScalar<T2>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Execute parameterized SQL that selects a single value asynchronously.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns>The first cell returned, as T.</returns>
   Task<T2> ExecuteScalarAsync<T2>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Insert a stream to a binary column.
   /// A custom command is needed that included the mandatory parameter '@DataStream'
   /// If there is no value specified for parameter '@DataStream' then the stream will automatically be assigned to that parameter
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="stream"></param>
   /// <param name="param"></param>
   void WriteStream(string sql, Stream stream, object param = null);


   /// <summary>
   /// Insert a stream to a binary column asynchronously.
   /// A custom command is needed that included the mandatory parameter '@DataStream'
   /// If there is no value specified for parameter '@DataStream' then the stream will automatically be assigned to that parameter
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="stream"></param>
   /// <param name="param"></param>
   Task WriteStreamAsync(string sql, Stream stream, object param = null);

   /// <summary>
   /// Asynchronously retrieve a value from the database as a stream.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="readCallback">The callback action to process the stream</param>
   /// <returns></returns>
   void GetStream(string sql, object param, Action<Stream> readCallback);

   /// <summary>
   /// Asynchronously retrieve a value from the database as a stream.
   /// </summary>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="readCallback">The callback function to process the stream</param>
   /// <returns></returns>
   Task GetStreamAsync(string sql, object param, Func<Stream, Task> readCallback);

   /// <summary>
   /// Execute an action with an already created connection.
   /// </summary>
   /// <param name="connection"></param>
   /// <param name="action"></param>
   void ExecuteWithExistingConnection(IDbConnection connection, Action<IDbConnection> action);

   /// <summary>
   /// Execute an action with an already created connection asynchronously.
   /// </summary>
   /// <param name="connection"></param>
   /// <param name="action"></param>
   /// <returns></returns>
   Task ExecuteWithExistingConnectionAsync(IDbConnection connection, Func<IDbConnection, Task> action);

   /// <summary>
   /// Execute a sql command and return the result in a list of X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   IList<TCustom> QueryCustom<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Execute a sql command asynchronously using Task and return the result in a list of X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<IList<TCustom>> QueryCustomAsync<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);


   /// <summary>
   /// Executes a single-row query, returning the data typed as X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   TCustom QueryCustomSingle<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query asynchronously using Task, returning the data typed as X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<TCustom> QueryCustomSingleAsync<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query, returning the data typed as X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   TCustom QueryCustomSingleOrDefault<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query asynchronously using Task, returning the data typed as X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<TCustom> QueryCustomSingleOrDefaultAsync<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query, returning the data typed as X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="cnn"></param>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   TCustom QueryCustomFirst<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query asynchronously using Task, returning the data typed as X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="cnn"></param>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<TCustom> QueryCustomFirstAsync<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query, returning the data typed as X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   TCustom QueryCustomFirstOrDefault<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);

   /// <summary>
   /// Executes a single-row query asynchronously using Task, returning the data typed as X.
   /// </summary>
   /// <typeparam name="TCustom"></typeparam>
   /// <param name="sql"></param>
   /// <param name="param"></param>
   /// <param name="transaction"></param>
   /// <param name="commandTimeout"></param>
   /// <param name="commandType"></param>
   /// <returns></returns>
   Task<TCustom> QueryCustomFirstOrDefaultAsync<TCustom>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = CommandType.Text);
}

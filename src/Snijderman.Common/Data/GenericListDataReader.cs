using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Snijderman.Common.Data;

[CodeCoverage.ExcludeFromCodeCoverage("")]
public class GenericListDataReader<T> : IDataReader
{
   private readonly IEnumerator<T> _list;
   private readonly List<PropertyInfo> _properties = new();

   public GenericListDataReader(IEnumerable<T> list)
   {
      if (list == null)
      {
         throw new ArgumentNullException(nameof(list));
      }

      this._list = list.GetEnumerator();

      this._properties.AddRange(
                       typeof(T)
                       .GetProperties(
                           BindingFlags.GetProperty |
                           BindingFlags.Instance |
                           BindingFlags.Public |
                           BindingFlags.DeclaredOnly
                           ));
   }

   #region IDataReader Members

   public void Close() => this._list.Dispose();

   public int Depth => throw new NotImplementedException();

   public DataTable GetSchemaTable() => throw new NotImplementedException();

   public bool IsClosed => throw new NotImplementedException();

   public bool NextResult() => throw new NotImplementedException();

   public bool Read() => this._list.MoveNext();

   public int RecordsAffected => throw new NotImplementedException();

   #endregion

   #region IDisposable Members
   private bool _disposedValue; // To detect redundant calls

   protected virtual void Dispose(bool disposing)
   {
      if (!this._disposedValue)
      {
         if (disposing)
         {
            // dispose managed state (managed objects).
            this.Close();

         }

         this._disposedValue = true;
      }
   }

   public void Dispose()
   {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      this.Dispose(true);
      GC.SuppressFinalize(this);
   }

   ~GenericListDataReader()
   {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      this.Dispose(false);
   }

   #endregion

   #region IDataRecord Members

   public int FieldCount => this._properties.Count;

   public bool GetBoolean(int i) => (bool)this.GetValue(i);

   public byte GetByte(int i) => (byte)this.GetValue(i);

   public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => throw new NotImplementedException();

   public char GetChar(int i) => (char)this.GetValue(i);

   public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => throw new NotImplementedException();

   public IDataReader GetData(int i) => throw new NotImplementedException();

   public string GetDataTypeName(int i) => throw new NotImplementedException();

   public DateTime GetDateTime(int i) => (DateTime)this.GetValue(i);

   public decimal GetDecimal(int i) => (decimal)this.GetValue(i);

   public double GetDouble(int i) => (double)this.GetValue(i);

   public Type GetFieldType(int i) => this._properties[i].PropertyType;

   public float GetFloat(int i) => (float)this.GetValue(i);

   public Guid GetGuid(int i) => (Guid)this.GetValue(i);

   public short GetInt16(int i) => (short)this.GetValue(i);

   public int GetInt32(int i) => (int)this.GetValue(i);

   public long GetInt64(int i) => (long)this.GetValue(i);

   public string GetName(int i) => this._properties[i].Name;

   public int GetOrdinal(string name) => this._properties.FindIndex(prop => string.Equals(prop.Name, name, StringComparison.OrdinalIgnoreCase));

   public string GetString(int i) => (string)this.GetValue(i);

   public object GetValue(int i) => this._properties[i].GetValue(this._list.Current, null);

   public int GetValues(object[] values)
   {
      if (values == null)
      {
         throw new ArgumentNullException(nameof(values));
      }

      var getValues = Math.Max(this.FieldCount, values.Length);
      for (var i = 0; i < getValues; i++)
      {
         values[i] = this.GetValue(i);
      }

      return getValues;
   }

   public bool IsDBNull(int i) => this.GetValue(i) == null;

   public object this[string name] => this.GetValue(this.GetOrdinal(name));

   public object this[int i] => this.GetValue(i);

   #endregion
}

namespace Snijderman.Common.Data.Paging
{
   public enum MetaDataType
   {
      /// <summary>
      /// data with dateTime type
      /// </summary>
      DateTime,
      /// <summary>
      /// data with date type
      /// </summary>
      Date,
      /// <summary>
      /// represent string
      /// </summary>
      String,
      /// <summary>
      /// data with boolean type
      /// </summary>
      Boolean,
      /// <summary>
      /// data with integer type
      /// </summary>
      Int,
      /// <summary>
      /// list with single select 
      /// </summary>
      SingleSelectEnum,
      /// <summary>
      /// money
      /// </summary>
      Amount,
      /// <summary>
      /// data with decimal type
      /// </summary>
      Decimal,
      /// <summary>
      /// key data (id)
      /// </summary>
      Key,
      /// <summary>
      /// item with action (export, show extra info)
      /// </summary>
      Actions,
      /// <summary>
      /// row with checkbox (available multiple selection in table)
      /// </summary>
      Select,
      /// <summary>
      /// clickable row
      /// </summary>
      SingleSelect,
      /// <summary>
      /// list with multiple select 
      /// </summary>
      MultipleSelectEnum
   }
}

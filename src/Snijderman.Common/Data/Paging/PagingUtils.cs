using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Snijderman.Common.ExtensionMethods;

namespace Snijderman.Common.Data.Paging;

public static class PagingUtils
{
   public static string CreateWhereString(Info info, MetaDataDefinitionPaging metadata)
   {
      var whereString = new StringBuilder();
      if (info?.Filters.ListCount() == 0 || metadata == null)
      {
         return whereString.ToString();
      }

      var first = true;
      foreach (var infoFilter in info?.Filters)
      {
         var condition = CreateCondition(infoFilter, metadata);

         if (first)
         {
            whereString.Append("WHERE " + condition);
            first = false;
         }
         else
         {
            whereString.Append($" {infoFilter.AndOr} " + condition);
         }
      }

      return whereString.ToString();
   }

   public static string CreateSortString(Info info, MetaDataDefinitionPaging metaDataDefinition)
   {
      var sort = string.Empty;
      if (string.IsNullOrEmpty(info?.SortName) || metaDataDefinition == null)
      {
         return sort;
      }

      sort = $"ORDER BY {metaDataDefinition.PropertyDefinitions.First(p => string.Equals(p.Name, info.SortName, StringComparison.OrdinalIgnoreCase)).SqlName}";
      if (!string.IsNullOrEmpty(info.SortDirection))
      {
         sort += $" {info.SortDirection}";
      }

      return sort;
   }

   private static string CreateCondition(Filter infoFilter, MetaDataDefinitionPaging metadata)
   {
      var propertyDefinition = metadata.PropertyDefinitions.First(p => string.Equals(p.Name, infoFilter.Column, StringComparison.OrdinalIgnoreCase));
      var @operator = (Operator)int.Parse(infoFilter.Operator, CultureInfo.InvariantCulture);
      var condition = "";

      if (@operator == Operator.IsEmpty)
      {
         condition = $"{ConvertSqlName(propertyDefinition)} {ConvertOperator(@operator)}";
         if (propertyDefinition.MetaDataType.Equals(MetaDataType.String))
         {
            condition += $" OR {ConvertSqlName(propertyDefinition)} = ''";
            condition = $"({condition})";
         }
      }
      else
      {
         if (infoFilter.Value != null)
         {
            condition = $"{ConvertSqlName(propertyDefinition)} {ConvertOperator(@operator)} {FormatInputValue(infoFilter.Value.ToString(), @operator, propertyDefinition.MetaDataType)}";
         }
         else
         {
            throw new ArgumentException("Using an empty value is not allowed for operators other than 'IsEmpty'!");
         }
      }

      return condition;
   }

   private static string ConvertSqlName(MetaDataPropertyDefinition metaDataPropertyDefinition)
   {
      var result = metaDataPropertyDefinition.SqlName;

      if (metaDataPropertyDefinition.MetaDataType == MetaDataType.DateTime || metaDataPropertyDefinition.MetaDataType == MetaDataType.Date)
      {
         result = $" convert(date, {metaDataPropertyDefinition.SqlName} ,101) ";
      }

      return result;
   }

   public static string ConvertOperator(Operator @operator) =>
      @operator switch
      {
         Operator.Equals => " = ",
         Operator.NotEquals => " != ",
         Operator.Greater => " > ",
         Operator.Less => " < ",
         Operator.GreaterEqual => " >= ",
         Operator.LessEqual => " <= ",
         Operator.Contains => " LIKE ",
         Operator.NotContains => " NOT LIKE ",
         Operator.In => " IN ",
         Operator.IsEmpty => " IS NULL",
         _ => throw new NotSupportedException(@operator.ToString()),
      };

   private static string FormatInputValue(string inputValue, Operator @operator, MetaDataType propertyDefinitionMetaDataType)
   {
      var result = @operator switch
      {
         Operator.Equals or Operator.NotEquals or Operator.Greater or Operator.Less or Operator.GreaterEqual or Operator.LessEqual => propertyDefinitionMetaDataType switch
         {
            MetaDataType.DateTime or MetaDataType.Date => $"{{d '{inputValue}'}}",
            MetaDataType.String => $"'{inputValue}'",
            MetaDataType.Boolean or MetaDataType.Int or MetaDataType.SingleSelectEnum or MetaDataType.MultipleSelectEnum or MetaDataType.Amount or MetaDataType.Decimal => inputValue,
            _ => throw new NotSupportedException($"propertyDefinitionMetaDataType: {propertyDefinitionMetaDataType}"),
         },
         Operator.Contains or Operator.NotContains => $"'%{inputValue}%'",
         Operator.In => inputValue,
         _ => throw new NotSupportedException($"operator: {nameof(@operator)}"),
      };
      return result;
   }
}

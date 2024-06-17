namespace Snijderman.Common.Tests;

public class ReflectionTests
{
   [Fact]
   public void Test_MakeGenericMethodAndInvoke_With_Single_Generic_Null_Instance_Expecting_ArgumentNullException()
   {
      Assert.Throws<ArgumentNullException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(null, null, typeof(string));
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_With_Multiple_Generics_Null_Instance_Expecting_ArgumentNullException()
   {
      Assert.Throws<ArgumentNullException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(null, null, [typeof(string), typeof(int)]);
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_With_Single_Generic_Null_Generic_Type_Expecting_ArgumentNullException()
   {
      Assert.Throws<ArgumentNullException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, "test", default(Type));
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_With_Multiple_Generics_Null_Generic_Type_Expecting_ArgumentNullException()
   {
      Assert.Throws<ArgumentNullException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, "test", default(Type[]));
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_With_Multiple_Generics_Empty_Generic_Type_Expecting_ArgumentNullException()
   {
      Assert.Throws<ArgumentNullException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, "test", []);
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_With_Single_Generic_Null_MethodName_Expecting_ArgumentException()
   {
      Assert.Throws<ArgumentException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, null, typeof(string));
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_With_Multiple_Generics_Null_MethodName_Expecting_ArgumentException()
   {
      Assert.Throws<ArgumentException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, null, [typeof(string), typeof(int)]);
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_Calling_Not_Existing_Method_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, "not here", typeof(string));
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_Calling_Not_Existing_Method_With_Multiple_Generics_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, "not here", [typeof(string), typeof(int)]);
      });
   }

   private string TestWithoutGenerics()
   {
      return "No generics here";
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_Calling_Method_Without_Generic_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.TestWithoutGenerics), typeof(string));
      });
   }

   [Fact]
   public void Test_MakeGenericMethodAndInvoke_Calling_Method_Without_Generics_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.TestWithoutGenerics), [typeof(string), typeof(int)]);
      });
   }

#pragma warning disable S2326 // Unused type parameters should be removed
   private void Method_With_One_Generic_No_Parameter<T>()
#pragma warning restore S2326 // Unused type parameters should be removed
   {
      // empty on purpose
   }

   [Fact]
   public void Test_Calling_Method_With_One_Generic_No_Parameters_Providing_One_Generic_No_Parameters_Expecting_Valid_Processing()
   {
      _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_Generic_No_Parameter), typeof(string));
      Assert.True(true);
   }

   [Fact]
   public void Test_Calling_Method_With_One_Generic_No_Parameters_Providing_One_Generic_And_Parameters_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_Generic_No_Parameter), typeof(string), ["test"]);
      });
   }

#pragma warning disable S2326 // Unused type parameters should be removed
   private void Method_With_One_UnusedGeneric_One_Parameter<T>(string input)
#pragma warning restore S2326 // Unused type parameters should be removed
   {
      // empty on purpose
   }

   [Fact]
   public void Test_Calling_Method_With_One_UnusedGeneric_One_Parameter_Providing_One_Generic_No_Parameters_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_UnusedGeneric_One_Parameter), typeof(string));
      });
   }

   [Fact]
   public void Test_Calling_Method_With_One_UnusedGeneric_One_Parameter_Providing_One_Generic_And_Parameters_Expecting_Valid_Processing()
   {
      _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_UnusedGeneric_One_Parameter), typeof(string), ["test"]);
      Assert.True(true);
   }

   [Fact]
   public void Test_Calling_Method_With_One_UnusedGeneric_One_Parameter_Providing_One_Generic_And_Parameter_But_Wrong_Type_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_UnusedGeneric_One_Parameter), typeof(string), [1512]);
      });
   }

   [Fact]
   public void Test_Calling_Method_With_One_UnusedGeneric_One_Parameter_Providing_One_Generic_And_Parameter_With_Null_Value_Expecting_Valid_Processing()
   {
      _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_UnusedGeneric_One_Parameter), typeof(string), [default]);
      Assert.True(true);
   }

   private void Method_With_One_Generic_One_Parameter<T>(T input)
   {
      // empty on purpose
   }

   [Fact]
   public void Test_Calling_Method_With_One_Generic_One_Parameter_Providing_One_Generic_No_Parameters_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_Generic_One_Parameter), typeof(string));
      });
   }

   [Fact]
   public void Test_Calling_Method_With_One_Generic_One_Parameter_Providing_One_Generic_And_Parameters_Expecting_Valid_Processing()
   {
      _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_Generic_One_Parameter), typeof(string), ["test"]);
      Assert.True(true);
   }

   [Fact]
   public void Test_Calling_Method_With_One_Generic_One_Parameter_Providing_One_Generic_And_Parameters_But_Wrong_Type_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_Generic_One_Parameter), typeof(int), ["test"]);
      });
   }

   [Fact]
   public void Test_Calling_Method_With_One_Generic_One_Parameter_Providing_One_Generic_And_Parameter_With_Null_Value_Expecting_Valid_Processing()
   {
      _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_One_Generic_One_Parameter), typeof(string), [default]);
      Assert.True(true);
   }

#pragma warning disable S2326 // Unused type parameters should be removed
   private string Method_Overloaded_With_One_Generic_One_Parameter<T>(string input)
#pragma warning restore S2326 // Unused type parameters should be removed
   {
      return $"Method with string input: {input}";
   }

   private string Method_Overloaded_With_One_Generic_One_Parameter<T>(T input) => $"Method with generic input: {input}";

   [Fact]
   public void Test_Calling_Overloaded_Method_2_Possibilities_Expecting_Valid_Processing()
   {
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_Overloaded_With_One_Generic_One_Parameter), typeof(string), ["test"]);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // when 2 methods that can be invoked are found, the one with generics is used
      Assert.True(resultAsString.Contains("Method with generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public void Test_Generic_Calling_Overloaded_Method_2_Possibilities_Expecting_Valid_Processing()
   {
      var resultAsString = Helpers.Reflection.MakeGenericMethodAndInvoke<string>(this, nameof(Method_Overloaded_With_One_Generic_One_Parameter), typeof(string), ["test"]);
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // when 2 methods that can be invoked are found, the one with generics is used
      Assert.True(resultAsString.Contains("Method with generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public void Test_Calling_Overloaded_Method_1_Possibility_Expecting_Valid_Processing()
   {
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_Overloaded_With_One_Generic_One_Parameter), typeof(int), ["test"]);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      Assert.True(resultAsString.Contains("Method with string input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public void Test_Calling_Overloaded_Method_1_Generic_Possibilitiy_Expecting_Valid_Processing()
   {
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_Overloaded_With_One_Generic_One_Parameter), typeof(int), [1512]);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      Assert.True(resultAsString.Contains("Method with generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public void Test_Calling_Overloaded_Method_0_Possibilities_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_Overloaded_With_One_Generic_One_Parameter), typeof(int), [false]);
      });
   }

   private string Method_With_One_Generic_Two_Parameters<T>(string input1, T input2)
   {
      return $"Method with string and generic input: {input1} - {input2}";
   }

   private string Method_With_One_Generic_Two_Parameters<T>(T input1, string input2)
   {
      return $"Method with generic and string input: {input1} - {input2}";
   }

   [Fact]
   public void Test_Calling_Overloaded_Method_With_Multiple_Parameters_2_Possibilities_Expecting_Valid_Processing()
   {
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_One_Generic_Two_Parameters), typeof(string), ["test", "input"]);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with string and generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public void Test_Calling_Overloaded_Method_With_Multiple_Parameters_1_Possibility_Scenario1_Expecting_Valid_Processing()
   {
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_One_Generic_Two_Parameters), typeof(int), [1512, "input"]);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with generic and string input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public void Test_Calling_Overloaded_Method_With_Multiple_Parameters_1_Possibility_Scenario2_Expecting_Valid_Processing()
   {
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_One_Generic_Two_Parameters), typeof(int), ["input", 1512]);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with string and generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public void Test_Calling_Overloaded_Method_With_Multiple_Parameters_0_Possibilities_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_One_Generic_Two_Parameters), typeof(int), [1512, 1973]);
      });
   }

   // Now the async versions
   [Fact]
   public async Task Test_Calling_Overloaded_Method_With_Multiple_Parameters_2_Possibilities_Expecting_Valid_Processing_Async()
   {
      var result = await Helpers.Reflection.MakeGenericMethodAndInvokeAsync(this, nameof(Method_With_One_Generic_Two_ParametersAsync), typeof(string), ["test", "input"]).ConfigureAwait(true);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with string and generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public async Task Test_Calling_Overloaded_Method_With_Multiple_Parameters_1_Possibility_Scenario1_Expecting_Valid_Processing_Async()
   {
      var result = await Helpers.Reflection.MakeGenericMethodAndInvokeAsync(this, nameof(Method_With_One_Generic_Two_ParametersAsync), typeof(int), [1512, "input"]).ConfigureAwait(true);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with generic and string input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public async Task Test_Calling_Overloaded_Method_With_Multiple_Parameters_1_Possibility_Scenario2_Expecting_Valid_Processing_Async()
   {
      var result = await Helpers.Reflection.MakeGenericMethodAndInvokeAsync(this, nameof(Method_With_One_Generic_Two_ParametersAsync), typeof(int), ["input", 1512]).ConfigureAwait(true);
      Assert.NotNull(result);
      var resultAsString = result as string;
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with string and generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public async Task Test_Calling_Overloaded_Method_With_Multiple_Parameters_0_Possibilities_Expecting_InvalidOperationException_Async()
   {
      await Assert.ThrowsAsync<InvalidOperationException>(() =>
      {
         return Helpers.Reflection.MakeGenericMethodAndInvokeAsync(this, nameof(Method_With_One_Generic_Two_ParametersAsync), typeof(int), [1512, 1973]);
      }).ConfigureAwait(true);
   }

   // Async with generic type
   [Fact]
   public async Task Test_Generic_Calling_Overloaded_Method_With_Multiple_Parameters_2_Possibilities_Expecting_Valid_Processing_Async()
   {
      var resultAsString = await Helpers.Reflection.MakeGenericMethodAndInvokeAsync<string>(this, nameof(Method_With_One_Generic_Two_ParametersAsync), typeof(string), ["test", "input"]).ConfigureAwait(true);
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with string and generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public async Task Test_Generic_Calling_Overloaded_Method_With_Multiple_Parameters_1_Possibility_Scenario1_Expecting_Valid_Processing_Async()
   {
      var resultAsString = await Helpers.Reflection.MakeGenericMethodAndInvokeAsync<string>(this, nameof(Method_With_One_Generic_Two_ParametersAsync), typeof(int), [1512, "input"]).ConfigureAwait(true);
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with generic and string input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public async Task Test_Generic_Calling_Overloaded_Method_With_Multiple_Parameters_1_Possibility_Scenario2_Expecting_Valid_Processing_Async()
   {
      var resultAsString = await Helpers.Reflection.MakeGenericMethodAndInvokeAsync<string>(this, nameof(Method_With_One_Generic_Two_ParametersAsync), typeof(int), ["input", 1512]).ConfigureAwait(true);
      Assert.NotNull(resultAsString);
      Assert.False(string.IsNullOrWhiteSpace(resultAsString));
      // The 2 methods are exactly the same when the generic type is a string, so the first declared is returned
      Assert.True(resultAsString.Contains("Method with string and generic input", StringComparison.OrdinalIgnoreCase));
   }

   [Fact]
   public async Task Test_Generic_Calling_Overloaded_Method_With_Multiple_Parameters_0_Possibilities_Expecting_InvalidOperationException_Async()
   {
      await Assert.ThrowsAsync<InvalidOperationException>(() =>
      {
         return Helpers.Reflection.MakeGenericMethodAndInvokeAsync<string>(this, nameof(Method_With_One_Generic_Two_ParametersAsync), typeof(int), [1512, 1973]);
      }).ConfigureAwait(true);
   }

   private async Task<string> Method_With_One_Generic_Two_ParametersAsync<T>(string input1, T input2)
   {
      await Task.Delay(TimeSpan.FromMilliseconds(500)).ConfigureAwait(true);
      return $"Method with string and generic input: {input1} - {input2}";
   }

   private async Task<string> Method_With_One_Generic_Two_ParametersAsync<T>(T input1, string input2)
   {
      await Task.Delay(TimeSpan.FromMilliseconds(500)).ConfigureAwait(true);
      return $"Method with generic and string input: {input1} - {input2}";
   }

   private void Method_With_Two_Generics<T1, T2>(T1 input1, T2 input2)
   {
      // empty on purpose
   }

   [Fact]
   public void Test_Method_With_Two_Generics_Expecting_Valid_processing()
   {
      var typeArguments = new[]
      {
         typeof(string), typeof(int)
      };
      _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_Two_Generics), typeArguments, ["input", 1512]);
      Assert.True(true);
   }

   [Fact]
   public void Test_Method_With_Two_Generics_Wrong_Arguments_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         var typeArguments = new[]
         {
            typeof(string), typeof(int)
         };
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_Two_Generics), typeArguments, [1512, "input"]);
      });
   }

#pragma warning disable S1172 // Unused method parameters should be removed
   private T2 Method_With_Two_Generics_Returning_Value<T1, T2>(T1 input)
   {
      return default;
   }

   private T1 Method_With_Two_Generics_Returning_Value<T1, T2>(T2 input)
   {
      return default;
   }
#pragma warning restore S1172 // Unused method parameters should be removed

   [Fact]
   public void Test_Method_With_Two_Generics_Returning_Int_Value_Expecting_Vaiid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(string), typeof(int)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Two_Generics_Returning_Value), typeArguments, ["input"]);
      if (result is not int intResult)
      {
         Assert.Fail("Response is not an int");
      }
      else
      {
         Assert.True(intResult == default);
      }
   }

   [Fact]
   public void Test_Method_With_Two_Generics_Returning_Boolean_Value_Expecting_Vaiid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(string), typeof(bool)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Two_Generics_Returning_Value), typeArguments, ["input"]);
      if (result is not bool boolResult)
      {
         Assert.Fail("Response is not a boolean");
      }
      else
      {
         Assert.True(boolResult == default);
      }
   }

   [Fact]
   public void Test_Method_With_Two_Generics_Returning_Value_Wrong_Input_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         var typeArguments = new[]
         {
            typeof(int), typeof(bool)
         };
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Two_Generics_Returning_Value), typeArguments, ["input"]);
      });
   }

   [Fact]
   public void Test_Overloaded_Method_With_Two_Generics_Returning_String_Value_Expecting_Valid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(string), typeof(int)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Two_Generics_Returning_Value), typeArguments, [1512]);
      Assert.Null(result);
   }

   [Fact]
   public void Test_Overloaded_Method_With_Two_Generics_Returning_Boolean_Value_Expecting_Valid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(bool), typeof(int)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Two_Generics_Returning_Value), typeArguments, [1512]);
      if (result is not bool boolResult)
      {
         Assert.Fail("Response is not a boolean");
      }
      else
      {
         Assert.True(boolResult == default);
      }
   }

   [Fact]
   public void Test_Overloaded_Method_With_Two_Generics_Returning_With_Wrong_Parameters_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         var typeArguments = new[]
         {
            typeof(int), typeof(bool)
         };
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Two_Generics_Returning_Value), typeArguments, ["input"]);
      });
   }

#pragma warning disable S1172 // Unused method parameters should be removed
   private T3 Method_With_Three_Generics<T1, T2, T3>(T1 input, T2 input2, string input3)
   {
      return default;
   }

   private T3 Method_With_Three_Generics<T1, T2, T3>(string input1, T1 input2, T2 input3)
   {
      return default;
   }

   private T3 Method_With_Three_Generics<T1, T2, T3>(T2 input1, string input2, T1 input3)
   {
      return default;
   }
#pragma warning restore S1172 // Unused method parameters should be removed

   [Fact]
   public void Test_Method_With_Three_Generics_Providing_2_Input_Types_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         var typeArguments = new[]
         {
            typeof(string), typeof(bool)
         };
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Three_Generics), typeArguments, ["input"]);
      });
   }

   [Fact]
   public void Test_Method_With_Three_Generics_Providing_4_Input_Types_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         var typeArguments = new[]
         {
            typeof(string), typeof(bool), typeof(int), typeof(DateTime)
         };
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Three_Generics), typeArguments, ["input"]);
      });
   }

   [Fact]
   public void Test_Overloaded_Method_With_Three_Generics_Expecting_Valid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(string), typeof(int), typeof(bool)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Three_Generics), typeArguments, ["input", 1512, "test"]);
      Assert.NotNull(result);
   }

   [Fact]
   public void Test_Overloaded_Method_With_Three_Generics_With_No_Matching_Signature_Expecting_InvalidOperationException()
   {
      Assert.Throws<InvalidOperationException>(() =>
      {
         var typeArguments = new[]
         {
            typeof(string), typeof(int), typeof(bool)
         };
         _ = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(Method_With_Three_Generics), typeArguments, ["input", true, "test"]);
      });
   }

   private string Method_With_Argument_Array_As_Parameter<T>(T inputString, int inputInt, object[] args)
   {
      return $"{inputString}_{inputInt}: {args?.Length} items";
   }

   [Fact]
   public void Test_Argument_Array_As_Parameter_Passing_1_Argument_In_Array_Expecting_Valid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(string)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_Argument_Array_As_Parameter), typeArguments, [
         "input", 1512, new object[]
         {
            "test"
         }
      ]);
      Assert.NotNull(result);
      if (result is not string stringResult)
      {
         Assert.Fail("Response is not a string");
      }
      else
      {
         Assert.False(string.IsNullOrWhiteSpace(stringResult));
         Assert.Equal("input_1512: 1 items", stringResult, ignoreCase: true);
      }
   }

   [Fact]
   public void Test_Argument_Array_As_Parameter_Passing_3_Arguments_In_Array_Expecting_Valid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(string)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_Argument_Array_As_Parameter), typeArguments, [
         "input", 1512, new object[]
         {
            "test1", "test2", "test3"
         }
      ]);
      Assert.NotNull(result);
      if (result is not string stringResult)
      {
         Assert.Fail("Response is not a string");
      }
      else
      {
         Assert.False(string.IsNullOrWhiteSpace(stringResult));
         Assert.Equal("input_1512: 3 items", stringResult, ignoreCase: true);
      }
   }

   private string Method_With_Params_As_Parameter<T>(T inputString, int inputInt, params object[] args)
   {
      return $"{inputString}_{inputInt}: {args?.Length} items";
   }

   [Fact]
   public void Test_With_Params_As_Parameter_Passing_1_Argument_In_Params_Expecting_Valid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(string)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_Params_As_Parameter), typeArguments, [
         "input", 1512, new object[]
         {
            "test"
         }
      ]);
      Assert.NotNull(result);
      if (result is not string stringResult)
      {
         Assert.Fail("Response is not a string");
      }
      else
      {
         Assert.False(string.IsNullOrWhiteSpace(stringResult));
         Assert.Equal("input_1512: 1 items", stringResult, ignoreCase: true);
      }
   }

   [Fact]
   public void Test_A_With_Params_As_Parameter_Passing_3_Arguments_In_Params_Expecting_Valid_Processing()
   {
      var typeArguments = new[]
      {
         typeof(string)
      };
      var result = Helpers.Reflection.MakeGenericMethodAndInvoke(this, nameof(this.Method_With_Params_As_Parameter), typeArguments, [
         "input", 1512, new object[]
         {
            "test1", "test2", "test3"
         }
      ]);
      Assert.NotNull(result);
      if (result is not string stringResult)
      {
         Assert.Fail("Response is not a string");
      }
      else
      {
         Assert.False(string.IsNullOrWhiteSpace(stringResult));
         Assert.Equal("input_1512: 3 items", stringResult, ignoreCase: true);
      }
   }

   private async Task Method_With_One_Generic_And_Func_With_Delegate<T>(T input, Func<T, object, Task> callbackHandler) => await callbackHandler(input, DateTime.Now.Date).ConfigureAwait(false);

   private async Task Method_With_One_Generic_And_Func_With_Delegate<T>(T input, Func<T, string, Task> callbackHandler) => await callbackHandler(input, "Test").ConfigureAwait(false);

   private async Task Method_With_One_Generic_And_Func_With_Delegate<T>(T input, Func<T, int, Task> callbackHandler) => await callbackHandler(input, 1512).ConfigureAwait(false);

   private async Task Method_With_One_Generic_And_Func_With_Delegate<T>(T input, Func<T, bool, Task> callbackHandler) => await callbackHandler(input, true).ConfigureAwait(false);

   [Fact]
   public async Task Test_With_Boolean_Delegate_Handler()
   {
      var inputString = "DitIsDeInput";
      var typeArguments = new[]
      {
         typeof(string)
      };
      var testSuccessful = false;
      Func<string, bool, Task> callback = (input, callbackResult) =>
      {
         testSuccessful = string.Equals(input, inputString) && callbackResult;
         return Task.CompletedTask;
      };

      await Helpers.Reflection.MakeGenericMethodAndInvokeAsync(this, nameof(this.Method_With_One_Generic_And_Func_With_Delegate), typeArguments, [inputString, callback]).ConfigureAwait(true);

      Assert.True(testSuccessful);
   }

   [Fact]
   public async Task Test_With_Int32_Delegate_Handler()
   {
      var inputString = "DitIsDeInput";
      var typeArguments = new[]
      {
         typeof(string)
      };
      var testSuccessful = false;
      Func<string, int, Task> callback = (input, callbackResult) =>
      {
         testSuccessful = string.Equals(input, inputString) && callbackResult == 1512;
         return Task.CompletedTask;
      };

      await Helpers.Reflection.MakeGenericMethodAndInvokeAsync(this, nameof(this.Method_With_One_Generic_And_Func_With_Delegate), typeArguments, [inputString, callback]).ConfigureAwait(true);

      Assert.True(testSuccessful);
   }

   [Fact]
   public async Task Test_With_String_Delegate_Handler()
   {
      var inputString = "DitIsDeInput";
      var typeArguments = new[]
      {
         typeof(string)
      };
      var testSuccessful = false;
      Func<string, string, Task> callback = (input, callbackResult) =>
      {
         testSuccessful = string.Equals(input, inputString) && string.Equals("Test", callbackResult);
         return Task.CompletedTask;
      };

      await Helpers.Reflection.MakeGenericMethodAndInvokeAsync(this, nameof(this.Method_With_One_Generic_And_Func_With_Delegate), typeArguments, [inputString, callback]).ConfigureAwait(true);

      Assert.True(testSuccessful);
   }
}

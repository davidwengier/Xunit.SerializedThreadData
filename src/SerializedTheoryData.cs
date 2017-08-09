﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;

namespace Xunit
{
	/// <summary>
	/// Provides data for theories based on collection initialization syntax.
	/// </summary>
	public class SerializedTheoryData : IEnumerable<object[]>
	{
		private readonly List<object[]> data = new List<object[]>();

		/// <summary>
		/// Adds a row to the theory.
		/// </summary>
		/// <param name="values">The values to be added.</param>
		protected void AddRow(params object[] values)
		{
			MakeSerializable(values);
			data.Add(values);
		}

		private void MakeSerializable(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (IsSerializable(values)) continue;

				object value = values[i];
				Type valueType = value.GetType();
				Type wrapperType;
				if (valueType.GetTypeInfo().IsGenericType && (valueType.GetGenericTypeDefinition() == typeof(List<>)))
				{
					wrapperType = typeof(SerializedList<>).MakeGenericType(valueType.GenericTypeArguments[0]);
				}
				else
				{
					wrapperType = typeof(SerializedWrapper<>).MakeGenericType(valueType);
				}
				values[i] = Activator.CreateInstance(wrapperType, value);
			}
		}

		private bool IsSerializable(object arg)
		{
			// the CanSerializeObject method from xunit is internal, and the class its on is private, so sadly.....
			//Type t = typeof(Xunit.Sdk.XunitTest).GetTypeInfo().Assembly.GetType("Xunit.Serialization.XunitSerializationInfo", true, true);
			//MethodInfo method = t.GetRuntimeMethod("CanSerializeObject", Type.EmptyTypes);
			return arg is IXunitSerializable;
		}

		/// <inheritdoc/>
		public IEnumerator<object[]> GetEnumerator()
		{
			foreach (object[] item in data)
			{
				yield return item.Select(i => (i as SerializedWrapper)?.WrappedValue ?? i).ToArray();
			}
		}

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	/// <summary>
	/// Represents a set of data for a theory with a single parameter. Data can
	/// be added to the data set using the collection initializer syntax.
	/// </summary>
	/// <typeparam name="T">The parameter type.</typeparam>
	public class SerializedTheoryData<T> : SerializedTheoryData
	{
		/// <summary>
		/// Adds data to the theory data set.
		/// </summary>
		/// <param name="p">The data value.</param>
		public void Add(T p)
		{
			AddRow(p);
		}
	}

	/// <summary>
	/// Represents a set of data for a theory with 2 parameters. Data can
	/// be added to the data set using the collection initializer syntax.
	/// </summary>
	/// <typeparam name="T1">The first parameter type.</typeparam>
	/// <typeparam name="T2">The second parameter type.</typeparam>
	public class SerializedTheoryData<T1, T2> : SerializedTheoryData
	{
		/// <summary>
		/// Adds data to the theory data set.
		/// </summary>
		/// <param name="p1">The first data value.</param>
		/// <param name="p2">The second data value.</param>
		public void Add(T1 p1, T2 p2)
		{
			AddRow(p1, p2);
		}
	}

	/// <summary>
	/// Represents a set of data for a theory with 3 parameters. Data can
	/// be added to the data set using the collection initializer syntax.
	/// </summary>
	/// <typeparam name="T1">The first parameter type.</typeparam>
	/// <typeparam name="T2">The second parameter type.</typeparam>
	/// <typeparam name="T3">The third parameter type.</typeparam>
	public class SerializedTheoryData<T1, T2, T3> : SerializedTheoryData
	{
		/// <summary>
		/// Adds data to the theory data set.
		/// </summary>
		/// <param name="p1">The first data value.</param>
		/// <param name="p2">The second data value.</param>
		/// <param name="p3">The third data value.</param>
		public void Add(T1 p1, T2 p2, T3 p3)
		{
			AddRow(p1, p2, p3);
		}
	}

	/// <summary>
	/// Represents a set of data for a theory with 4 parameters. Data can
	/// be added to the data set using the collection initializer syntax.
	/// </summary>
	/// <typeparam name="T1">The first parameter type.</typeparam>
	/// <typeparam name="T2">The second parameter type.</typeparam>
	/// <typeparam name="T3">The third parameter type.</typeparam>
	/// <typeparam name="T4">The fourth parameter type.</typeparam>
	public class SerializedTheoryData<T1, T2, T3, T4> : SerializedTheoryData
	{
		/// <summary>
		/// Adds data to the theory data set.
		/// </summary>
		/// <param name="p1">The first data value.</param>
		/// <param name="p2">The second data value.</param>
		/// <param name="p3">The third data value.</param>
		/// <param name="p4">The fourth data value.</param>
		public void Add(T1 p1, T2 p2, T3 p3, T4 p4)
		{
			AddRow(p1, p2, p3, p4);
		}
	}

	/// <summary>
	/// Represents a set of data for a theory with 5 parameters. Data can
	/// be added to the data set using the collection initializer syntax.
	/// </summary>
	/// <typeparam name="T1">The first parameter type.</typeparam>
	/// <typeparam name="T2">The second parameter type.</typeparam>
	/// <typeparam name="T3">The third parameter type.</typeparam>
	/// <typeparam name="T4">The fourth parameter type.</typeparam>
	/// <typeparam name="T5">The fifth parameter type.</typeparam>
	public class SerializedTheoryData<T1, T2, T3, T4, T5> : SerializedTheoryData
	{
		/// <summary>
		/// Adds data to the theory data set.
		/// </summary>
		/// <param name="p1">The first data value.</param>
		/// <param name="p2">The second data value.</param>
		/// <param name="p3">The third data value.</param>
		/// <param name="p4">The fourth data value.</param>
		/// <param name="p5">The fifth data value.</param>
		public void Add(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
		{
			AddRow(p1, p2, p3, p4, p5);
		}
	}
}
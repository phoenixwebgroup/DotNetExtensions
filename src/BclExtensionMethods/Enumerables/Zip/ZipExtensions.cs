﻿namespace BclExtensionMethods.Enumerables.Zip
{
	using System;
	using System.Collections.Generic;

	﻿#region License and Terms
	//
	// MoreLINQ - Extensions to LINQ to Objects
	// Copyright (c) 2008-9 Jonathan Skeet. All rights reserved.
	//
	// Licensed under the Apache License, Version 2.0 (the "License");
	// you may not use this file except in compliance with the License.
	// You may obtain a copy of the License at
	//
	//    http://www.apache.org/licenses/LICENSE-2.0
	//
	// Unless required by applicable law or agreed to in writing, software
	// distributed under the License is distributed on an "AS IS" BASIS,
	// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	// See the License for the specific language governing permissions and
	// limitations under the License.
	//
	#endregion


	/// <summary>
	/// Complete rip off of, so please defer to them for licensing
	///  http://code.google.com/p/morelinq/source/browse/trunk/MoreLinq/Zip.cs
	/// </summary>
	public static class ZipExtensions
	{
		/// <summary>
		///     Returns a projection of tuples, where each tuple contains the N-th element 
		///     from each of the argument sequences.
		/// </summary>
		/// <remarks>
		///     If the two input sequences are of different lengths, the result sequence 
		///     is terminated as soon as the shortest input sequence is exhausted.
		///     This operator uses deferred execution and streams its results.
		/// </remarks>
		/// <example>
		///     <code>
		///         int[] numbers = { 1, 2, 3 };
		///         string[] letters = { "A", "B", "C", "D" };
		///         var zipped = numbers.Zip(letters, (n, l) => n + l);
		///     </code>
		///     The <c>zipped</c> variable, when iterated over, will yield "1A", "2B", "3C", in turn.
		/// </example>
		/// <typeparam name = "TFirst">Type of elements in first sequence</typeparam>
		/// <typeparam name = "TSecond">Type of elements in second sequence</typeparam>
		/// <typeparam name = "TResult">Type of elements in result sequence</typeparam>
		/// <param name = "first">First sequence</param>
		/// <param name = "second">Second sequence</param>
		/// <param name = "resultSelector">Function to apply to each pair of elements</param>
		public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
																		 IEnumerable<TSecond> second,
																		 Func<TFirst, TSecond, TResult> resultSelector)
		{
			first.ThrowIfNull("first");
			second.ThrowIfNull("second");
			resultSelector.ThrowIfNull("resultSelector");

			return ZipImplementation(first, second, resultSelector, ImbalancedZipStrategy.Truncate);
		}

		/// <summary>
		///     Returns a projection of tuples, where each tuple contains the N-th element 
		///     from each of the argument sequences.
		/// </summary>
		/// <remarks>
		///     If the two input sequences are of different lengths then 
		///     <see cref = "InvalidOperationException" /> is thrown.
		///     This operator uses deferred execution and streams its results.
		/// </remarks>
		/// <example>
		///     <code>
		///         int[] numbers = { 1, 2, 3, 4 };
		///         string[] letters = { "A", "B", "C", "D" };
		///         var zipped = numbers.ZipWithFailure(letters, (n, l) => n + l);
		///     </code>
		///     The <c>zipped</c> variable, when iterated over, will yield "1A", "2B", "3C", "4D" in turn.
		/// </example>
		/// <typeparam name = "TFirst">Type of elements in first sequence</typeparam>
		/// <typeparam name = "TSecond">Type of elements in second sequence</typeparam>
		/// <typeparam name = "TResult">Type of elements in result sequence</typeparam>
		/// <param name = "first">First sequence</param>
		/// <param name = "second">Second sequence</param>
		/// <param name = "resultSelector">Function to apply to each pair of elements</param>
		public static IEnumerable<TResult> ZipWithFailure<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
																			 IEnumerable<TSecond> second,
																			 Func<TFirst, TSecond, TResult> resultSelector)
		{
			first.ThrowIfNull("first");
			second.ThrowIfNull("second");
			resultSelector.ThrowIfNull("resultSelector");

			return ZipImplementation(first, second, resultSelector, ImbalancedZipStrategy.Fail);
		}

		/// <summary>
		///     Returns a projection of tuples, where each tuple contains the N-th element 
		///     from each of the argument sequences.
		/// </summary>
		/// <remarks>
		///     If the two input sequences are of different lengths then the result 
		///     sequence will always be as long as the longer of the two input sequences.
		///     The default value of the shorter sequence element type is used for padding.
		///     This operator uses deferred execution and streams its results.
		/// </remarks>
		/// <example>
		///     <code>
		///         int[] numbers = { 1, 2, 3 };
		///         string[] letters = { "A", "B", "C", "D" };
		///         var zipped = numbers.ZipWithFailure(letters, (n, l) => n + l);
		///     </code>
		///     The <c>zipped</c> variable, when iterated over, will yield "1A", "2B", "3C", "0D" in turn.
		/// </example>
		/// <typeparam name = "TFirst">Type of elements in first sequence</typeparam>
		/// <typeparam name = "TSecond">Type of elements in second sequence</typeparam>
		/// <typeparam name = "TResult">Type of elements in result sequence</typeparam>
		/// <param name = "first">First sequence</param>
		/// <param name = "second">Second sequence</param>
		/// <param name = "resultSelector">Function to apply to each pair of elements</param>
		public static IEnumerable<TResult> ZipPadded<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
																				IEnumerable<TSecond> second,
																				Func<TFirst, TSecond, TResult> resultSelector)
		{
			first.ThrowIfNull("first");
			second.ThrowIfNull("second");
			resultSelector.ThrowIfNull("resultSelector");

			return ZipImplementation(first, second, resultSelector, ImbalancedZipStrategy.Pad);
		}

		private static IEnumerable<TResult> ZipImplementation<TFirst, TSecond, TResult>(
			IEnumerable<TFirst> first,
			IEnumerable<TSecond> second,
			Func<TFirst, TSecond, TResult> resultSelector,
			ImbalancedZipStrategy imbalanceStrategy)
		{
			using (var firstEnumerator = first.GetEnumerator())
			{
				using (var secondEnumerator = second.GetEnumerator())
				{
					while (firstEnumerator.MoveNext())
					{
						if (secondEnumerator.MoveNext())
						{
							yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
						}
						else
						{
							switch (imbalanceStrategy)
							{
								case ImbalancedZipStrategy.Fail:
									throw new InvalidOperationException("Second sequence ran out before first");
								case ImbalancedZipStrategy.Truncate:
									yield break;
								case ImbalancedZipStrategy.Pad:
									do
									{
										yield return resultSelector(firstEnumerator.Current, default(TSecond));
									} while (firstEnumerator.MoveNext());
									yield break;
							}
						}
					}
					if (secondEnumerator.MoveNext())
					{
						switch (imbalanceStrategy)
						{
							case ImbalancedZipStrategy.Fail:
								throw new InvalidOperationException("First sequence ran out before second");
							case ImbalancedZipStrategy.Truncate:
								yield break;
							case ImbalancedZipStrategy.Pad:
								do
								{
									yield return resultSelector(default(TFirst), secondEnumerator.Current);
								} while (secondEnumerator.MoveNext());
								yield break;
						}
					}
				}
			}
		}

		/// <summary>
		///     Strategy determining the handling of the case where the inputs are of
		///     unequal lengths.
		/// </summary>
		internal enum ImbalancedZipStrategy
		{
			/// <summary>
			///     The result sequence ends when either input sequence is exhausted.
			/// </summary>
			Truncate = 0,
			/// <summary>
			///     The result sequence ends when both sequences are exhausted. The 
			///     shorter sequence is effectively "padded" at the end with the default
			///     value for its element type.
			/// </summary>
			Pad = 1,
			/// <summary>
			///     <see cref = "InvalidOperationException" /> is thrown if one sequence
			///     is exhausted but not the other.
			/// </summary>
			Fail = 2
		}

		internal static void ThrowIfNull<T>(this T argument, string name) where T : class
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
		}
	}
}
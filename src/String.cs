#region License, Terms and Author(s)
//
// Mannex - Extension methods for .NET
// Copyright (c) 2009 Atif Aziz. All rights reserved.
//
//  Author(s):
//
//      Atif Aziz, http://www.raboof.com
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

namespace Mannex
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using IO;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>

    static partial class StringExtensions
    {
        /// <summary>
        /// Masks an empty string with a given mask such that the result
        /// is never an empty string. If the str string is null or
        /// empty then it is masked, otherwise the original is returned.
        /// </summary>
        /// <remarks>
        /// Use this method to guarantee that you never get an empty
        /// string. Bear in mind, however, that if the mask itself is an
        /// empty string then this method could yield an empty string!
        /// </remarks>

        [DebuggerStepThrough]
        public static string MaskEmpty(this string str, string mask)
        {
            return !string.IsNullOrEmpty(str) ? str : mask;
        }

        /// <summary>
        /// Determines if a string has another non-empty string as its
        /// prefix and longer by it by at least one character.
        /// </summary>

        public static bool HasPrefix(this string str, string prefix, StringComparison comparison)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));

            return !string.IsNullOrEmpty(prefix)
                && str.Length > prefix.Length
                && str.StartsWith(prefix, comparison);
        }

        /// <summary>
        /// Determines if a string has another non-empty string as its
        /// suffix and longer by it by at least one character.
        /// </summary>

        public static bool HasSuffix(this string str, string suffix, StringComparison comparison)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));

            return !string.IsNullOrEmpty(suffix)
                && str.Length > suffix.Length
                && str.EndsWith(suffix, comparison);
        }

        /// <summary>
        /// Returns a section of a string from a give starting point on.
        /// </summary>
        /// <remarks>
        /// If <paramref name="start"/> is negative, it is  treated as
        /// <c>length</c> + <paramref name="start" /> where <c>length</c>
        /// is the length of the string. If <paramref name="start"/>
        /// is greater or equal to the length of the string then
        /// no characters are copied to the new string.
        /// </remarks>

        [DebuggerStepThrough]
        public static string Slice(this string str, int start)
        {
            return Slice(str, start, null);
        }

        /// <summary>
        /// Returns a section of a string.
        /// </summary>
        /// <remarks>
        /// This method copies up to, but not including, the element
        /// indicated by <paramref name="end"/>. If <paramref name="start"/>
        /// is negative, it is  treated as <c>length</c> + <paramref name="start" />
        /// where <c>length</c> is the length of the string. If
        /// <paramref name="end"/> is negative, it is treated as <c>length</c> +
        /// <paramref name="end"/> where <c>length</c> is the length of the
        /// string. If <paramref name="end"/> occurs before <paramref name="start"/>,
        /// no characters are copied to the new string.
        /// </remarks>

        [DebuggerStepThrough]
        public static string Slice(this string str, int start, int? end)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return SliceImpl(str, start, end ?? str.Length);
        }

        static string SliceImpl(this string str, int start, int end)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            var length = str.Length;

            if (start < 0)
            {
                start = length + start;
                if (start < 0)
                    start = 0;
            }
            else
            {
                if (start > length)
                    start = length;
            }

            if (end < 0)
            {
                end = length + end;
                if (end < 0)
                    end = 0;
            }
            else
            {
                if (end > length)
                    end = length;
            }

            var sliceLength = end - start;

            return sliceLength > 0 ?
                str.Substring(start, sliceLength) : string.Empty;
        }

        /// <summary>
        /// Embeds string into <paramref name="target"/>, using {0}
        /// within <paramref name="target"/> as the point of embedding.
        /// </summary>

        public static string Embed(this string str, string target)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (target == null) throw new ArgumentNullException(nameof(target));
            return string.Format(target, str);
        }

        /// <summary>
        /// Wraps string between two other string where the first
        /// indicates the left side and the second indicates the
        /// right side.
        /// </summary>

        public static string Wrap(this string str, string lhs, string rhs)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return lhs + str + rhs;
        }

        /// <summary>
        /// Enquotes string with <paramref name="quote"/>, escaping occurences
        /// of <paramref name="quote"/> itself with <paramref name="escape"/>.
        /// </summary>

        public static string Quote(this string str, string quote, string escape)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            StringBuilder sb = null;
            var start = 0;
            int index;
            while ((index = str.IndexOf(quote, start, StringComparison.Ordinal)) >= 0)
            {
                if (sb == null)
                    sb = new StringBuilder(str.Length + 10).Append(quote);
                sb.Append(str, start, index - start);
                sb.Append(escape);
                start = index + quote.Length;
            }
            return sb != null
                 ? sb.Append(str, start, str.Length - start).Append(quote).ToString()
                 : str.Wrap(quote, quote);
        }

        /// <summary>
        /// Format string using <paramref name="args"/> as sources for
        /// replacements and a function, <paramref name="binder"/>, that
        /// determines how to bind and resolve replacement tokens.
        /// </summary>

        public static string FormatWith(this string format,
            Func<string, object[], IFormatProvider, string> binder, params object[] args)
        {
            return format.FormatWith(null, binder, args);
        }

        /// <summary>
        /// Format string using <paramref name="args"/> as sources for
        /// replacements and a function, <paramref name="binder"/>, that
        /// determines how to bind and resolve replacement tokens. In
        /// addition, <paramref name="provider"/> is used for cultural
        /// formatting.
        /// </summary>

        public static string FormatWith(this string format,
            IFormatProvider provider, Func<string, object[], IFormatProvider, string> binder, params object[] args)
        {
            if (format == null) throw new ArgumentNullException(nameof(format));
            if (binder == null) throw new ArgumentNullException(nameof(binder));

            var result = new StringBuilder(format.Length * 2);
            var token = new StringBuilder();

            using (var e = format.GetEnumerator())
            while (e.MoveNext())
            {
                var ch = e.Current;
                if (ch == '{')
                {
                    while (true)
                    {
                        if (!e.MoveNext())
                            throw new FormatException();

                        ch = e.Current;
                        if (ch == '}')
                        {
                            if (token.Length == 0)
                                throw new FormatException();

                            result.Append(binder(token.ToString(), args, provider));
                            token.Length = 0;
                            break;
                        }
                        if (ch == '{')
                        {
                            result.Append(ch);
                            break;
                        }
                        token.Append(ch);
                    }
                }
                else if (ch == '}')
                {
                    if (!e.MoveNext() || e.Current != '}')
                        throw new FormatException();
                    result.Append('}');
                }
                else
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Splits a string into a pair using a specified character to
        /// separate the two.
        /// </summary>
        /// <remarks>
        /// Neither half in the resulting pair is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, char separator, Func<string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));
            return SplitRemoving(str, str.IndexOf(separator), 1, resultFunc);
        }

        /// <summary>
        /// Splits a string into three parts using any of a specified set of
        /// characters to separate the three.
        /// </summary>
        /// <remarks>
        /// None of the resulting parts is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, char separator, Func<string, string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));
            return str.Split(separator, (a, rest) => rest.Split(separator, (b, c) => resultFunc(a, b, c)));
        }

        /// <summary>
        /// Splits a string into four parts using any of a specified set of
        /// characters to separate the four.
        /// </summary>
        /// <remarks>
        /// None of the resulting parts is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, char separator, Func<string, string, string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));
            return str.Split(separator, (a, b, rest) => rest.Split(separator, (c, d) => resultFunc(a, b, c, d)));
        }

        /// <summary>
        /// Splits a string into a pair using any of a specified set of
        /// characters to separate the two.
        /// </summary>
        /// <remarks>
        /// Neither half in the resulting pair is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, char[] separators, Func<string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));

            return separators == null || separators.Length == 0
                 ? resultFunc(str, string.Empty)
                 : SplitRemoving(str, str.IndexOfAny(separators), 1, resultFunc);
        }

        /// <summary>
        /// Splits a string into three parts using any of a specified set of
        /// characters to separate the three.
        /// </summary>
        /// <remarks>
        /// None of the resulting parts is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, char[] separators, Func<string, string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));
            return str.Split(separators, (a, rest) => rest.Split(separators, (b, c) => resultFunc(a, b, c)));
        }

        /// <summary>
        /// Splits a string into four parts using any of a specified set of
        /// characters to separate the four.
        /// </summary>
        /// <remarks>
        /// None of the resulting parts is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, char[] separators, Func<string, string, string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));
            return str.Split(separators, (a, b, rest) => rest.Split(separators, (c, d) => resultFunc(a, b, c, d)));
        }

        /// <summary>
        /// Splits a string into a pair using a specified string to
        /// separate the two. An additional parameter specifies comparison
        /// rules used to find the separator string.
        /// </summary>
        /// <remarks>
        /// Neither half in the resulting pair is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, string separator, StringComparison comparison, Func<string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));
            return SplitRemoving(str, str.IndexOf(separator, comparison), separator.Length, resultFunc);
        }

        /// <summary>
        /// Splits a string into three parts using a specified string to
        /// separate the three.  An additional parameter specifies comparison
        /// rules used to find the separator string.
        /// </summary>
        /// <remarks>
        /// None of the resulting parts is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, string separator, StringComparison comparison, Func<string, string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));
            return str.Split(separator, comparison, (a, rest) => rest.Split(separator, comparison, (b, c) => resultFunc(a, b, c)));
        }

        /// <summary>
        /// Splits a string into four parts using a specified string to
        /// separate the four. An additional parameter specifies comparison
        /// rules used to find the separator string.
        /// </summary>
        /// <remarks>
        /// None of the resulting parts is ever <c>null</c>.
        /// </remarks>

        public static T Split<T>(this string str, string separator, StringComparison comparison, Func<string, string, string, string, T> resultFunc)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultFunc == null) throw new ArgumentNullException(nameof(resultFunc));
            return str.Split(separator, comparison, (a, b, rest) => rest.Split(separator, comparison, (c, d) => resultFunc(a, b, c, d)));
        }

        /// <summary>
        /// Splits a string into a pair by removing a portion of the string.
        /// </summary>
        /// <remarks>
        /// Neither half in the resulting pair is ever <c>null</c>.
        /// </remarks>

        static T SplitRemoving<T>(string str, int index, int count, Func<string, string, T> resultFunc)
        {
            Debug.Assert(str != null);
            Debug.Assert(count > 0);
            Debug.Assert(resultFunc != null);

            var a = index < 0
                  ? str
                  : str.Substring(0, index);

            var b = index < 0 || index + 1 >= str.Length
                  ? string.Empty
                  : str.Substring(index + count);

            return resultFunc(a, b);
        }

#if NETSTANDARD2_0

        /// <summary>
        /// Splits a string in two given a separator.
        /// </summary>

        public static (string, string) Split2(this string str, char separator)
        {
            return str.Split(separator, (a, b) => (a, b));
        }

        /// <summary>
        /// Splits a string into three parts where a given separator appears
        /// in the string.
        /// </summary>

        public static (string, string, string) Split3(this string str, char separator)
        {
            return str.Split(separator, (a, b, c) => (a, b, c));
        }

        /// <summary>
        /// Splits a string into four parts where a given separator appears
        /// in the string.
        /// </summary>

        public static (string, string, string, string) Split4(this string str, char separator)
        {
            return str.Split(separator, (a, b, c, d) => (a, b, c, d));
        }

#endif

        /// <summary>
        /// Splits string into lines where a line is terminated
        /// by CR and LF, or just CR or just LF. White space is trimmed off
        /// each line and any resulting blank lines are skipped.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution.
        /// </remarks>

        public static IEnumerable<string> SplitIntoNonBlankLines(this string str)
        {
            return from line in str.SplitIntoLines()
                   select line.Trim() into line
                   where line.Length > 0
                   select line;
        }

        /// <summary>
        /// Splits string into lines where a line is terminated
        /// by CR and LF, or just CR or just LF.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution.
        /// </remarks>

        public static IEnumerable<string> SplitIntoLines(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));

            return _(); IEnumerable<string> _()
            {
                using (var reader = str.Read())
                using (var line = reader.ReadLines())
                    while (line.MoveNext())
                        yield return line.Current;
            }
        }

        /// <summary>
        /// Collapses all sequences of white space (as defined by Unicode
        /// and identified by <see cref="char.IsWhiteSpace(char)"/>) to a
        /// single space and trims all leading and trailing white space.
        /// </summary>

        public static string NormalizeWhiteSpace(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return Regex.Replace(str, @"\s+", " ").Trim();
        }

        /// <summary>
        /// Retrieves left, middle and right substrings from this instance
        /// given the character position and length of the middle substring.
        /// </summary>
        /// <returns>
        /// Returns a zero-base, single-dimension, array of three elements
        /// containing the left, middle and right substrings, respectively.
        /// </returns>
        /// <remarks>
        /// This function never returns <c>null</c> for any of the
        /// substrings. For example, even when <paramref name="index"/> is
        /// zero, the first substring will be an empty string, but not null.
        /// </remarks>

        public static string[] Substrings(this string str, int index, int length)
        {
            return Substrings(str, index, length, (left, mid, right) => new[] { left, mid, right });
        }

        /// <summary>
        /// Retrieves left, middle and right substrings from this instance
        /// given the character position and length of the middle substring.
        /// An additional parameter specifies a function that is used to
        /// project the final result.
        /// </summary>
        /// <remarks>
        /// This function never supplies <c>null</c> for any of the
        /// substrings. For example, even when <paramref name="index"/> is
        /// zero, the first substring will be an empty string, but not
        /// <c>null</c>.
        /// </remarks>

        public static T Substrings<T>(this string str, int index, int length, Func<string, string, string, T> resultor)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (resultor == null) throw new ArgumentNullException(nameof(resultor));

            return resultor(str.Substring(0, index),
                            str.Substring(index, length),
                            str.Substring(index + length));
        }

        static readonly Regex TruthinessRegex = new Regex(@"\A\s*(?i:true|1|on|yes)\s*\z",
                                                    RegexOptions.CultureInvariant);

        /// <summary>
        /// Returns <c>true</c> if the string value reads (excluding leading
        /// and trailing white space) either <c>"true"</c>, <c>"1"</c>,
        /// <c>"on"</c> or <c>"yes"</c>. Otherwise it returns <c>false</c>.
        /// </summary>

        public static bool IsTruthy(this string value)
        {
            return value != null && TruthinessRegex.IsMatch(value);
        }

        /// <summary>
        /// Gets the character at the given index in the string otherwise
        /// <c>null</c> if the index falls beyond last character of the
        /// string. If the index is negative it is used to look up
        /// the character from the end of the string where -1 yields that
        /// last character. Again, <c>null</c> is returned if the negative
        /// index goes beyond the first character of the string.
        /// </summary>

        public static char? TryCharAt(this string str, int index)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            return index < str.Length
                 ? index < 0
                 ? str.Length + index < 0
                 ? (char?) null
                 : str[str.Length + index]
                 : str[index]
                 : null;
        }

        /// <summary>
        /// Splits the string into lines and then removes all leading spaces
        /// that are common in all lines and returns those lines back as a
        /// single string.
        /// </summary>
        /// <remarks>
        /// Empty lines are not removed in the returned string.
        /// </remarks>

        public static string TrimCommonLeadingSpace(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));

            var output =
                from lines in new[] { str.SplitIntoLines() }
                let indents =
                    from line in lines
#if NET40 || NET45
                    where !string.IsNullOrWhiteSpace(line)
#else
                    where !string.IsNullOrEmpty(line) && line.Trim().Length > 0
#endif
                    select line.TakeWhile(ch => ch == ' ').Count() into indent
                    select (int?) indent
                let indent = indents.Min() ?? 0
                from line in lines
                select line.Slice(indent);

            return string.Join(Environment.NewLine,
#if NET40 || NET45
                       output.ToArray()
#else
                       output.ToArray()
#endif
                );
        }

        /// <summary>
        /// Partitions a string into equally sized parts (except possibly
        /// the last one) and returns a sequence containing each partition.
        /// than the given size.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution semantics.
        /// </remarks>

        public static IEnumerable<string> Partition(this string str, int size)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));

            return _(); IEnumerable<string> _()
            {
                var length = str.Length;
                for (var i = 0; i < length; i += size)
                    yield return str.Substring(i, Math.Min(size, length - i));
            }
        }

        /// <summary>
        /// A string that is equivalent to the current string except that all
        /// instances of <paramref name="oldValue"/> are replaced with
        /// <paramref name="newValue"/>. If <paramref name="oldValue"/> is
        /// not found in the current instance, the method returns the current
        /// instance unchanged. An additional parameter specifies the
        /// comparison rule when looking for <paramref name="oldValue"/>.
        /// </summary>

        public static string Replace(this string str, string oldValue, string newValue, StringComparison comparison)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (oldValue == null) throw new ArgumentNullException(nameof(oldValue));
            if (oldValue.Length == 0) throw new ArgumentException("String cannot be of zero length.", nameof(oldValue));

            StringBuilder sb = null;
            var previousIndex = 0;
            int index;
            while ((index = str.IndexOf(oldValue, previousIndex, comparison)) >= 0)
            {
                (sb ?? (sb = new StringBuilder()))
                    .Append(str.Substring(previousIndex, index - previousIndex))
                    .Append(newValue);
                previousIndex = index + oldValue.Length;
            }
            return sb != null
                 ? sb.Append(str.Substring(previousIndex)).ToString()
                 : str;
        }

        /// <summary>
        /// Repeats the string <paramref name="count"/> number of times.
        /// </summary>
        /// <remarks>
        /// If <paramref name="count"/> is zero then then the result is
        /// an empty string. If it is one then string is return verbatim.
        /// </remarks>

        public static string Repeat(this string str, int count)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), count, null);
            return count == 0 || str.Length == 0
                 ? string.Empty
                 : count == 1
                 ? str
                 : str.Length == 1
                 ? new string(str[0], count)
                 : new StringBuilder(str.Length * count).Insert(0, str, count).ToString();
        }
    }
}

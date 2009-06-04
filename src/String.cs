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
    using System.Diagnostics;
    using System.Text;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>

    static partial class StringExtensions
    {
        /// <summary>
        /// Masks an empty string with a given mask such that the result
        /// is never an empty string. If the input string is null or
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
            if (str == null) throw new ArgumentNullException("str");
            return SliceImpl(str, start, end ?? str.Length);
        }

        private static string SliceImpl(this string str, int start, int end)
        {
            if (str == null) throw new ArgumentNullException("str");
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
            if (str == null) throw new ArgumentNullException("str");
            if (target == null) throw new ArgumentNullException("target");
            return string.Format(target, str);
        }

        /// <summary>
        /// Wraps string between two other string where the first
        /// indicates the left side and the second indicates the
        /// right side.
        /// </summary>

        public static string Wrap(this string str, string lhs, string rhs)
        {
            if (str == null) throw new ArgumentNullException("str");
            return lhs + str + rhs;
        }

        /// <summary>
        /// Enquotes string with <paramref name="quote"/>, escaping occurences
        /// of <paramref name="quote"/> itself with <paramref name="escape"/>.
        /// </summary>

        public static string Quote(this string str, string quote, string escape)
        {
            if (str == null) throw new ArgumentNullException("str");
            StringBuilder sb = null;
            var start = 0;
            int index;
            while ((index = str.IndexOf(quote, start)) >= 0)
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
        /// <remarks>
        /// This method implements most of what is described in
        /// <a href="http://www.python.org/dev/peps/pep-3101/">PEP 3101 (Advanced String Formatting)</a> 
        /// from Python.
        /// </remarks>

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
        /// <remarks>
        /// This method implements most of what is described in
        /// <a href="http://www.python.org/dev/peps/pep-3101/">PEP 3101 (Advanced String Formatting)</a> 
        /// from Python.
        /// </remarks>

        public static string FormatWith(this string format,
            IFormatProvider provider, Func<string, object[], IFormatProvider, string> binder, params object[] args)
        {
            if (format == null) throw new ArgumentNullException("format");
            if (binder == null) throw new ArgumentNullException("binder");

            Debug.Assert(binder != null);

            var result = new StringBuilder(format.Length * 2);
            var token = new StringBuilder();

            var e = format.GetEnumerator();
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
    }
}
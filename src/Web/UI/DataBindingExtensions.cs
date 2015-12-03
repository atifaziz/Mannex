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

namespace Mannex.Web.UI
{
    #region Imports

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;

    #endregion

    /// <summary>
    /// Provides data expression evaluation facilites similar to 
    /// <see cref="DataBinder"/> in ASP.NET.
    /// </summary>

    static partial class DataBindingExtensions
    {
        /// <summary>
        /// Evaluates data-binding expressions at run time using an expression
        /// syntax that is similar to C# and Visual Basic for accessing
        /// properties or indexing into collections.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method performs late-bound evaluation, using run-time reflection
        /// and therefore can cause performance less than optimal.</para>
        /// <para>
        /// In addition to <see cref="DataBinder.Eval(object,string)"/>, this
        /// method converts <see cref="DBNull"/> values to a null reference.</para>
        /// </remarks>

        public static T DataBind<T>(this object obj, string expression)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            return (T) obj.DataBind(expression);
        }

        /// <summary>
        /// Evaluates data-binding expressions at run time using an expression
        /// syntax that is similar to C# and Visual Basic for accessing
        /// properties or indexing into collections.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method performs late-bound evaluation, using run-time reflection
        /// and therefore can cause performance less than optimal.</para>
        /// <para>
        /// Unlike <see cref="DataBinder.Eval(object,string)"/>, this method
        /// allows <paramref name="expression"/> to be <c>null</c> or an
        /// empty string and in which case it simply returns the value of
        /// <paramref name="obj"/>.</para>
        /// <para>
        /// In addition to <see cref="DataBinder.Eval(object,string)"/>, this
        /// method converts <see cref="DBNull"/> values to a null reference.</para>
        /// </remarks>

        public static object DataBind(this object obj, string expression)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            //
            // The ASP.NET DataBinder.Eval method does not like an empty or null
            // expression. Rather than making it an unnecessary exception, we
            // turn a nil-expression to mean, "evaluate to obj."
            //

            if (string.IsNullOrEmpty(expression))
                return obj;
            var value = DataBinder.Eval(obj, expression);
            return Convert.IsDBNull(value) ? null : value;
        }

        /// <summary>
        /// Format string using <paramref name="args"/> as sources for
        /// data-binding replacements.
        /// </summary>
        /// <remarks>
        /// This method implements most of what is described in
        /// <a href="http://www.python.org/dev/peps/pep-3101/">PEP 3101 (Advanced String Formatting)</a> 
        /// from Python.
        /// </remarks>

        public static string FormatWith(this string format, params object[] args)
        {
            return format.FormatWith(null, args);
        }

        /// <summary>
        /// Format string using <paramref name="args"/> as sources for
        /// data-binding replacements and <paramref name="provider"/> 
        /// for cultural formatting.
        /// </summary>
        /// <remarks>
        /// This method implements most of what is described in
        /// <a href="http://www.python.org/dev/peps/pep-3101/">PEP 3101 (Advanced String Formatting)</a> 
        /// from Python.
        /// </remarks>

        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            return format.FormatWith(provider, FormatTokenBinder, args);
        }

        static string FormatTokenBinder(string token, object[] args, IFormatProvider provider)
        {
            Debug.Assert(token != null);

            var source = args[0];
            var dotIndex = token.IndexOf('.');
            int sourceIndex;
            if (dotIndex > 0 && int.TryParse(token.Substring(0, dotIndex), NumberStyles.None, CultureInfo.InvariantCulture, out sourceIndex))
            {
                source = args[sourceIndex];
                token = token.Substring(dotIndex + 1);
            }

            var format = string.Empty;

            var colonIndex = token.IndexOf(':');
            if (colonIndex > 0)
            {
                format = "{0:" + token.Substring(colonIndex + 1) + "}";
                token = token.Substring(0, colonIndex);
            }

            if (int.TryParse(token, NumberStyles.None, CultureInfo.InvariantCulture, out sourceIndex))
            {
                source = args[sourceIndex];
                token = null;
            }

            object result;

            try
            {
                result = source.DataBind(token) ?? string.Empty;
            }
            catch (HttpException e)
            {
                throw new FormatException(e.Message, e);
            }

            return !string.IsNullOrEmpty(format)
                 ? string.Format(provider, format, result)
                 : result.ToString();
        }
    }
}
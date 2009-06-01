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
    }
}
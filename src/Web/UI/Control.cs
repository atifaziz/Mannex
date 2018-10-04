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

#if ASPNET

namespace Mannex.Web.UI
{
    #region Imports

    using System;
    using System.Linq;
    using System.Web.UI;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Control"/>.
    /// </summary>

    static partial class ControlExtensions
    {
        /// <summary>
        /// Similar to <see cref="Page.GetDataItem"/> but provides strong-typed result.
        /// </summary>

        public static T GetDataItem<T>(this Control control)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            var page = control.Page;
            if (page == null)
                throw new InvalidOperationException("Data-binding methods such as Eval(), XPath(), and Bind() can only be used in controls contained in a page.");
            return (T) page.GetDataItem();
        }

        /// <summary>
        /// Similar to <see cref="Page.GetDataItem"/>.
        /// </summary>

        public static object GetDataItem(this Control control)
        {
            return control.GetDataItem<object>();
        }

        /// <summary>
        /// Similar to <see cref="TemplateControl.Eval(string)"/> but 
        /// provides strong-typed result.
        /// </summary>

        public static T Eval<T>(this Control control, string expression)
        {
            return (T) DataBinder.Eval(control.GetDataItem(), expression);
        }

        /// <summary>
        /// Similar to <see cref="TemplateControl.Eval(string)"/>.
        /// </summary>

        public static object Eval(this Control control, string expression)
        {
            return control.Eval<object>(expression);
        }

        /// <summary>
        /// Similar to <see cref="TemplateControl.Eval(string)"/> but 
        /// returns HTML-encoded text.
        /// </summary>

        public static string EvalText(this Control control, string expression)
        {
            return (control.Eval(expression) ?? string.Empty).ToString().HtmlEncode();
        }

        /// <summary>
        /// Similar to <see cref="TemplateControl.Eval(string,string)"/> but 
        /// formats the expression and returns it as HTML-encode text.
        /// </summary>
        
        public static string EvalText(this Control control, string expression, string format)
        {
            return DataBinder.Eval(control.GetDataItem(), expression, format);
        }

        /// <summary>
        /// Evaluates a series of data-binding expressions (like
        /// <see cref="TemplateControl.Eval(string)"/> and formats them into a single
        /// string (similar to <see cref="string.Format(string,object[])"/>).
        /// </summary>
        /// <remarks>
        /// The returned string is <em>not</em> HTML-encoded.
        /// </remarks>

        public static string EvalMany(this Control control, string format, params string[] expressions)
        {
            if (expressions == null) throw new ArgumentNullException(nameof(expressions));
            return string.Format(format, expressions.Select(control.Eval).ToArray());
        }
    }
}

#endif // ASPNET

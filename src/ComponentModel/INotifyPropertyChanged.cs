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

namespace Mannex.ComponentModel
{
    #region Imports

    using System;
    using System.ComponentModel;
#if !NO_LINQ_EXPRESSIONS
    using System.Linq.Expressions;
#endif

    #endregion

    /// <summary>
    /// Extension methods for <see cref="INotifyPropertyChanged"/>.
    /// </summary>

    public static partial class INotifyPropertyChangedExtensions
    {
#if !NO_LINQ_EXPRESSIONS

        /// <summary>
        /// Subscribes to <see cref="INotifyPropertyChanged.PropertyChanged"/> 
        /// event of the soure object and calls <paramref name="handler"/> only
        /// when the property identified by <paramref name="expression"/>
        /// has changed.
        /// </summary>
        /// <returns>
        /// Returns the <see cref="PropertyChangedEventHandler"/> object that
        /// can be used for unsubscribing.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="expression"/> does not represent a simple
        /// property/field access type of expression (see <see cref="MemberExpression"/>).
        /// </exception>
        /// <remarks>
        /// If <paramref name="expression"/> is <c>null</c> or empty then 
        /// <paramref name="handler"/> is called when any property changes.
        /// </remarks>

        public static PropertyChangedEventHandler OnPropertyChanged<T, TValue>(this T source, Expression<Func<T, TValue>> expression, PropertyChangedEventHandler handler)
            where T : INotifyPropertyChanged
        {
            string propertyName = null;

            if (expression != null)
            {
                var member = expression.Body as MemberExpression;
                if (member == null)
                    throw new ArgumentException(null, "expression");
                propertyName = member.Member.Name;
            }

            return OnPropertyChanged(source, propertyName, handler);
        }

#endif

        /// <summary>
        /// Subscribes to <see cref="INotifyPropertyChanged.PropertyChanged"/> 
        /// event of the soure object and calls <paramref name="handler"/> only
        /// when the property identified by <paramref name="propertyName"/>
        /// (case-insensitive) has changed.
        /// </summary>
        /// <returns>
        /// Returns the <see cref="PropertyChangedEventHandler"/> object that
        /// can be used for unsubscribing.
        /// </returns>
        /// <remarks>
        /// If <paramref name="propertyName"/> is <c>null</c> or empty then 
        /// <paramref name="handler"/> is called when any property changes.
        /// </remarks>

        public static PropertyChangedEventHandler OnPropertyChanged(this INotifyPropertyChanged source, string propertyName, PropertyChangedEventHandler handler)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (handler == null) throw new ArgumentNullException("handler");

            PropertyChangedEventHandler onChanged = (sender, args) =>
            {
                if (string.IsNullOrEmpty(propertyName) 
                    || args.PropertyName.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
                    handler(sender, args);
            };

            source.PropertyChanged += onChanged;
            return onChanged;
        }
    }
}

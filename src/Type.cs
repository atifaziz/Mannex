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
    using System.IO;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Type"/>.
    /// </summary>

    static partial class TypeExtensions
    {
        /// <summary>
        /// Determines if type is a constructed type of <see cref="System.Nullable{T}"/>.
        /// </summary>

        public static bool IsConstructionOfNullable(this Type type)
        {
            return type.IsConstructionOfGenericTypeDefinition(typeof(Nullable<>));
        }

        /// <summary>
        /// Determines if type is a constructed type of generic type definition.
        /// For example, this method can be used to test if <see cref="System.Nullable{T}"/> 
        /// of <see cref="int" /> is indeed a construction of the generic type definition 
        /// <see cref="System.Nullable{T}"/>.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Either <paramref name="type"/> or <paramref name="genericTypeDefinition"/> 
        /// is a null reference.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The type identified by <paramref name="genericTypeDefinition"/> is not
        /// a generic type definition.
        /// </exception>

        public static bool IsConstructionOfGenericTypeDefinition(this Type type, Type genericTypeDefinition)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (genericTypeDefinition == null) throw new ArgumentNullException("genericTypeDefinition");

            if (!genericTypeDefinition.IsGenericTypeDefinition)
                throw new ArgumentException(string.Format("{0} is not a generic type definition.", genericTypeDefinition), "genericTypeDefinition");
            
            return type.IsGenericType
                && !type.IsGenericTypeDefinition
                && type.GetGenericTypeDefinition() == genericTypeDefinition;
        }

        /// <summary>
        /// Finds and returns <see cref="MethodInfo"/> for a method called
        /// <c>Parse</c> on this type. The <c>Parse</c> must accept two
        /// arguments typed <see cref="String"/> and <see cref="IFormatProvider"/>,
        /// respectively, and return a value of the same type as represented
        /// by this object.
        /// </summary>

        public static MethodInfo FindParseMethod(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            var method = type.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, ParseParameterTypes, null);
            return method != null && method.ReturnType == type ? method : null;
        }

        static readonly Type[] ParseParameterTypes = { typeof (string), typeof (IFormatProvider) };

        /// <summary>
        /// Returns a function capable of parsing a string into a value of
        /// the type represented by this object.
        /// </summary>
        /// <remarks>
        /// A type is parsable if has a static <c>Parse</c> method that
        /// accepts two arguments typed <see cref="String"/> and
        /// <see cref="IFormatProvider"/>, respectively, and returns a value
        /// of the same type as represented by this object.
        /// </remarks>

        public static Func<string, IFormatProvider, object> GetParser(this Type type)
        {
            return type.GetParseExpression().Compile();
        }

        /// <summary>
        /// Returns a function capable of parsing a string into a value of
        /// the type represented by this object if the type appears to
        /// support parsing.
        /// </summary>
        /// <remarks>
        /// A type is parsable if has a static <c>Parse</c> method that
        /// accepts two arguments typed <see cref="String"/> and
        /// <see cref="IFormatProvider"/>, respectively, and returns a value
        /// of the same type as represented by this object.
        /// </remarks>

        public static Func<string, IFormatProvider, object> TryGetParser(this Type type)
        {
            var expression = type.TryGetParseExpression();
            return expression != null ? expression.Compile() : null;
        }

        /// <summary>
        /// Creates and returns a lambda expression to parse a string into a
        /// value of the type represented by this object.
        /// </summary>
        /// <remarks>
        /// A type is parsable if has a static <c>Parse</c> method that
        /// accepts two arguments typed <see cref="String"/> and
        /// <see cref="IFormatProvider"/>, respectively, and returns a value
        /// of the same type as represented by this object.
        /// </remarks>

        public static Expression<Func<string, IFormatProvider, object>> GetParseExpression(this Type type)
        {
            var expression = type.TryGetParseExpression();
            if (expression == null)
                throw new Exception(string.Format("{0} does not appear to support parsing.", type.FullName));
            return expression;
        }

        /// <summary>
        /// Attempts to create and return a lambda expression to parse a
        /// string into a  value of the type represented by this object.
        /// </summary>
        /// <remarks>
        /// A type is parsable if has a static <c>Parse</c> method that
        /// accepts two arguments typed <see cref="String"/> and
        /// <see cref="IFormatProvider"/>, respectively, and returns a value
        /// of the same type as represented by this object.
        /// </remarks>

        public static Expression<Func<string, IFormatProvider, object>> TryGetParseExpression(this Type type)
        {
            var method = FindParseMethod(type);
            if (method == null)
                return null;
            var input = Expression.Parameter(typeof(string), "input");
            var formatProvider = Expression.Parameter(typeof(IFormatProvider), "formatProvider");
            return Expression.Lambda<Func<string, IFormatProvider, object>>(Expression.Convert(Expression.Call(method, input, formatProvider), typeof(object)), input, formatProvider);
        }

        /// <summary>
        /// Gets the default value for the type.
        /// </summary>

        public static object GetDefaultValue(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (type.IsGenericTypeDefinition || type.IsGenericParameter) throw new ArgumentException(null, "type");
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}

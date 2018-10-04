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

namespace Mannex.Reflection
{
    #region Imports

    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="PropertyInfo"/>.
    /// </summary>

    static partial class PropertyInfoExtensions
    {
        /// <summary>
        /// Creates a <see cref="LambdaExpression"/> that takes an instance of
        /// the declaring type of the property and accesses the property
        /// through it.
        /// </summary>
        /// <remarks>
        /// Indexer and static properties are not supported.
        /// </remarks>

        public static LambdaExpression CreateGetterLambda(this PropertyInfo info)
        {
            return CreateGetterLambda(info, false);
        }

        /// <summary>
        /// Creates a <see cref="LambdaExpression"/> that takes an instance of
        /// the declaring type of the property and accesses the property
        /// through it. An additional property specifies whether to box the
        /// return value or not.
        /// </summary>
        /// <remarks>
        /// Indexer and static properties are not supported.
        /// </remarks>

        public static LambdaExpression CreateGetterLambda(this PropertyInfo info, bool boxed)
        {
            ValidateGetterLambdaThis(info);
            var resultType = info.PropertyType.IsValueType && boxed
                           ? typeof(object)
                           : info.PropertyType;
            return CreateGetterLambda(info, info.DeclaringType, resultType);
        }

        /// <summary>
        /// Creates a <see cref="LambdaExpression"/> that takes an instance of
        /// the declaring type of the property and accesses the property
        /// through, converting the result to the type
        /// <typeparamref name="T"/> (if a conversion exists).
        /// </summary>
        /// <remarks>
        /// Indexer and static properties are not supported.
        /// </remarks>

        public static LambdaExpression CreateGetterLambda<T>(this PropertyInfo info)
        {
            ValidateGetterLambdaThis(info);
            return CreateGetterLambda(info, info.DeclaringType, typeof(T));
        }

        static void ValidateGetterLambdaThis(PropertyInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            if (!info.CanRead) throw new ArgumentException(null, nameof(info));
            if (info.GetIndexParameters().Length > 0) throw new ArgumentException(null, nameof(info));
            if (info.GetGetMethod().IsStatic) throw new ArgumentException(null, nameof(info));
        }

        static LambdaExpression CreateGetterLambda(PropertyInfo info, Type declaringType, Type resultType)
        {
            var self = Expression.Parameter(declaringType, "self");
            var delegateType = typeof (Func<,>).MakeGenericType(declaringType, resultType);
            Expression body = Expression.MakeMemberAccess(self, info);
            return Expression.Lambda(delegateType, resultType != info.PropertyType ? Expression.Convert(body, resultType) : body, self);
        }
    }
}

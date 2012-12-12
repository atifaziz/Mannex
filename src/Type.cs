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
    using System.Reflection;
    using System.Text;

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
    }
}

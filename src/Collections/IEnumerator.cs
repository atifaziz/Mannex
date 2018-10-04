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

namespace Mannex.Collections
{
    #region Imports

    using System;
    using System.Collections;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Extension methods for types implementing <see cref="IEnumerator{T}"/>.
    /// </summary>

    static partial class IEnumeratorExtensions
    {
        /// <summary>
        /// Creates an array containing the remaining items of an enumerator
        /// and then disposes the enumerator.
        /// </summary>

        public static T[] ToArray<T>(this IEnumerator enumerator)
        {
            return enumerator.ToList().ToArray<T>();
        }

        /// <summary>
        /// Creates a list containing the remaining items of an enumerator
        /// and then disposes the enumerator if it implements
        /// <see cref="IDisposable"/>.
        /// </summary>

        public static ArrayList ToList(this IEnumerator enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException(nameof(enumerator));
            var list = new ArrayList(4);
            try
            {
                while (enumerator.MoveNext())
                    list.Add(enumerator.Current);
                return list;
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null) disposable.Dispose();
            }
        }
    }
}
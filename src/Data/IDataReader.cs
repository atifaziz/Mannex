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

namespace Mannex.Data
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="IDataReader"/>.
    /// </summary>

    static partial class IDataReaderExtensions
    {
        /// <summary>
        /// Projects each record of the reader into a new form.
        /// </summary>

        public static IEnumerator<T> Select<T>(this IDataReader reader, Func<IDataRecord, T> selector)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (selector == null) throw new ArgumentNullException("selector");
            
            return SelectImpl(reader, selector);
        }
        
        static IEnumerator<T> SelectImpl<T>(IDataReader reader, Func<IDataRecord, T> selector)
        {
            using (reader)
            {
                var e = new DbEnumerator(reader, true);
                while (e.MoveNext())
                    yield return selector((IDataRecord) e.Current);
            }
        }
    }
}

#if NET45

namespace Mannex.Data
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Reflection;

    static partial class IDataReaderExtensions
    {
        static Func<object, Func<CancellationToken, Task<bool>>> _readAsyncBinder;

        static Func<object, Func<CancellationToken, Task<bool>>> ReadAsyncBinder
        {
            get
            {
                return _readAsyncBinder ?? (_readAsyncBinder = Reflector.GetMethodBinderFromCallTemplate<DbDataReader, CancellationToken, Task<bool>>(dr => dr.ReadAsync(CancellationToken.None)));
            }
        }

        /// <summary>
        /// Binds and returns a delegate to a method on the reader called 
        /// <c>ReadAsync</c> and which shares the same signature as 
        /// <see cref="DbDataReader.ReadAsync(CancellationToken)"/>. If no 
        /// such method is found then an implementation based on 
        /// <see cref="IDataReader.Read"/> is created instead.
        /// </summary>

        public static Func<CancellationToken, Task<bool>> BindReadAsync(this IDataReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            var dbReader = reader as DbDataReader;
            return dbReader == null
                 ? (ReadAsyncBinder(reader) ?? (token => Task.FromResult(reader.Read())))
                 : dbReader.ReadAsync;
        }
    }
}

#endif // NET45

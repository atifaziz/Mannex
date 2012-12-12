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
    /// Extension methods for <see cref="DataTable"/>.
    /// </summary>

    static partial class DataTableExtensions
    {
        /// <summary>
        /// Gets <see cref="IDataRecord"/> objects for each row of this table.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution.
        /// </remarks>

        public static IEnumerable<IDataRecord> GetRecords(this DataTable table)
        {
            if (table == null) throw new ArgumentNullException("table");
            return GetRecordsImpl(table);
        }
        
        static IEnumerable<IDataRecord> GetRecordsImpl(DataTable table)
        {
            using (var reader = new DataTableReader(table))
            {
                var e = new DbEnumerator(reader);
                while (e.MoveNext())
                    yield return (IDataRecord) e.Current;
            }
        }
    }
}
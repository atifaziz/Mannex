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

    #endregion

    /// <summary>
    /// Extension methods for <see cref="DataRow"/>.
    /// </summary>

    static partial class DataRowExtensions
    {
        /// <summary>
        /// Attempts to set the column of the <see cref="DataRow"/> to a
        /// given value if the named column exists.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the value was set and <c>false</c> otherwise (when
        /// the named column does not exist).
        /// </returns>
        /// <remarks>
        /// If the value is a nullable or reference type and the
        /// <paramref name="value"/> is <c>null</c> then the set value is
        /// <see cref="DBNull.Value"/>.
        /// </remarks>

        public static bool TrySetField<T>(this DataRow row, string name, T value)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));
            var column = row.Table.Columns[name];
            if (column == null)
                return false;
            row[column] = (Nullable.GetUnderlyingType(typeof(T)) != null
                          && EqualityComparer<T>.Default.Equals(value, default(T)))
                          || (typeof(T).IsClass && (object) value == null)
                        ? (object) DBNull.Value
                        : value;
            return true;
        }
    }
}

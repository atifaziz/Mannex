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

namespace Mannex.Data.Common
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    #endregion
    
    /// <summary>
    /// Extension methods for <see cref="DbConnection"/>.
    /// </summary>

    static partial class DbConnectionExtensions
    {
        /// <summary>
        /// Retrieves the <c>Columns</c> schema collection information for 
        /// the data source of this <see cref="DbConnection"/>.
        /// </summary>

        public static IEnumerable<DataRow> GetColumnsSchema(this DbConnection connection)
        {
            return GetColumnsSchema(connection, null);
        }

        /// <summary>
        /// Retrieves the <c>Columns</c> schema collection information for 
        /// the data source of this <see cref="DbConnection"/> given a 
        /// table name restriction.
        /// </summary>
        /// <remarks>
        /// The table name restriction is used in the third slot of the
        /// resetriction values.
        /// </remarks>

        public static IEnumerable<DataRow> GetColumnsSchema(this DbConnection connection, string tableNameRestriction)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            return from DataRow row in connection.GetSchema("Columns", new[] { null, null, tableNameRestriction, null }).Rows
                   select row;
        }
    }
}

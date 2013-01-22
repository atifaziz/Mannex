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
    using System.IO;
    using System.Linq;
    using Collections.Generic;
    using IO;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="TextReader"/>.
    /// </summary>

    static partial class TextReaderExtensions
    {
        /// <summary>
        /// Parses delimited text like CSV (command-separated values) into
        /// a <see cref="DataTable"/> object given a set of columns to bind
        /// to the source.
        /// </summary>

        public static DataTable ParseXsvAsDataTable(
            this TextReader reader, string delimiter, bool quoted,
            params DataColumn[] columns)
        {
            return reader.ParseXsvAsDataTable(delimiter, quoted, 
                       columns.Select(c => c.AsKeyTo(new Func<string, object>(s => s)))
                              .ToArray());
        }

        /// <summary>
        /// Parses delimited text like CSV (command-separated values) into
        /// a <see cref="DataTable"/> object given a set of columns to bind
        /// to the source and functions to convert source text values to
        /// required column types.
        /// </summary>

        public static DataTable ParseXsvAsDataTable(
            this TextReader reader, string delimiter, bool quoted, 
            params KeyValuePair<DataColumn, Func<string, object>>[] columns)
        {
            if (columns == null) throw new ArgumentNullException("columns");
            if (columns.Any(e => e.Key.Table != null || e.Value == null))
                throw new ArgumentException(null, "columns");

            var table = new DataTable();
            table.Columns.AddRange(columns.Select(e => e.Key).ToArray());
            
            using (var e = reader.ParseXsv(delimiter, quoted, 
                               hs =>
                               {
                                   var bindings = 
                                       columns.Length == 0
                                       ? from i in Enumerable.Range(0, hs.Length)
                                           select new
                                           {
                                               Index = i, 
                                               Name = hs[i],
                                               Converter = new Func<string, object>(s => s)
                                           }
                                       : from col in columns
                                           select new
                                           {
                                               Index     = Array.FindIndex(hs, h => col.Key.ColumnName.Equals(h, StringComparison.Ordinal)),
                                               Name      = col.Key.ColumnName,
                                               Converter = col.Value,
                                           } 
                                           into col
                                           select new
                                           {
                                               Index = col.Index >= 0 
                                                   ? col.Index 
                                                   : Array.FindIndex(hs, h => col.Name.Equals(h, StringComparison.OrdinalIgnoreCase)),
                                               col.Name,
                                               col.Converter,
                                           };
                               
                                   bindings = bindings.ToArray();
                                   if (columns.Length == 0 && bindings.Any())
                                       table.Columns.AddRange(bindings.Select(b => new DataColumn(b.Name)).ToArray());
                               
                                   return bindings;
                               }, 
                               (bs, vs) => 
                                   from b in bs
                                   select b.Converter(b.Index >= 0 ? vs[b.Index] : null)))
            {
                while (e.MoveNext())                     // ReSharper disable CoVariantArrayConversion
                    table.Rows.Add(e.Current.ToArray()); // ReSharper restore CoVariantArrayConversion
            }

            return table;
        }
    }
}

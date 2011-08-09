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
#if NET4
    using System.Diagnostics;
    using System.Dynamic;
#endif

    #endregion

    /// <summary>
    /// Extension methods for <see cref="IDataRecord"/>.
    /// </summary>

    static partial class IDataRecordExtensions
    {
#if NET4
        /// <summary>
        /// Converts the record into a dyamic object (specifically 
        /// <see cref="ExpandoObject"/>) where each field and its
        /// value become member of the object.
        /// </summary>

        public static ExpandoObject ToDynamicObject(this IDataRecord record)
        {
            return ToDynamicObject(record, f => f, (f, v) => v);
        }

        /// <summary>
        /// Converts the record into a dyamic object (specifically 
        /// <see cref="ExpandoObject"/>) where each field and its
        /// value become member of the object. Two additional
        /// parameters specify functions that determine how to
        /// map field names to member names on the dynamic object
        /// as well as their values.
        /// </summary>

        public static ExpandoObject ToDynamicObject(
            this IDataRecord record, 
            Func<string, string> nameMapper, 
            Func<string, object, object> valueMapper)
        {
            if (record == null) throw new ArgumentNullException("record");
            if (nameMapper == null) throw new ArgumentNullException("nameMapper");
            if (valueMapper == null) throw new ArgumentNullException("valueMapper");
            
            return Map(record, new ExpandoObject(), nameMapper, valueMapper);
        }

        private static T Map<T>(
            this IDataRecord record, 
            T target, 
            Func<string, string> nameMapper, 
            Func<string, object, object> valueMapper)
            where T : IDictionary<string, object>
        {
            Debug.Assert(record != null);
            Debug.Assert(nameMapper != null);
            Debug.Assert(valueMapper != null);

            for (var i = 0; i < record.FieldCount; i++)
            {
                var key = nameMapper(record.GetName(i));
                target.Add(key, valueMapper(key, record.GetValue(i)));
            }

            return target;
        }
#endif
    }
}

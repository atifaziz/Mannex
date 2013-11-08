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
    using System.Diagnostics;
#if NET4
    using System.Dynamic;
#endif
    using System.Linq;
    using System.Reflection;
    using Collections.Generic;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="IDataRecord"/>.
    /// </summary>

    static partial class IDataRecordExtensions
    {
        /// <summary>
        /// Gets an ordered sequence of field ordinals of this record.
        /// </summary>

        public static IEnumerable<int> GetOrdinals(this IDataRecord record)
        {
            return Enumerable.Range(0, record.FieldCount);
        }

        /// <summary>
        /// Gets an ordered sequence of field names of this record.
        /// </summary>

        public static IEnumerable<string> GetNames(this IDataRecord record)
        {
            if (record == null) throw new ArgumentNullException("record");
            return from i in record.GetOrdinals() select record.GetName(i);
        }

        /// <summary>
        /// Gets an ordered sequence of field value of this record.
        /// </summary>

        public static IEnumerable<object> GetValues(this IDataRecord record)
        {
            if (record == null) throw new ArgumentNullException("record");
            return from i in record.GetOrdinals()
                   select record.GetValue(i);
        }

        /// <summary>
        /// Gets an ordered sequence of fields of this record as 
        /// key-value pairs.
        /// </summary>

        public static IEnumerable<KeyValuePair<string, object>> GetFields(this IDataRecord record)
        {
            if (record == null) throw new ArgumentNullException("record");
            return from i in record.GetOrdinals()
                   select record.GetName(i).AsKeyTo(record.GetValue(i));
        }

        /// <summary>
        /// Provides strongly-typed access to the value of the field
        /// identified by its name.
        /// </summary>
        /// <remarks>
        /// If <typeparamref name="T"/> is a reference type and the field
        /// value is <see cref="DBNull.Value"/> then the returned value is
        /// a null reference. If <typeparamref name="T"/> is a value type 
        /// and the field value is <see cref="DBNull.Value"/> then 
        /// <see cref="InvalidCastException"/> is raised unless 
        /// <typeparamref name="T"/> is nullable (<see cref="Nullable{T}"/>).
        /// </remarks>

        public static T GetValue<T>(this IDataRecord record, string name)
        {
            if (record == null) throw new ArgumentNullException("record");
            return record.GetValue<T>(record.GetOrdinal(name));
        }

        /// <summary>
        /// Provides strongly-typed access to the value of the field
        /// identified by ordinal position.
        /// </summary>
        /// <remarks>
        /// If <typeparamref name="T"/> is a reference type and the field
        /// value is <see cref="DBNull.Value"/> then the returned value is
        /// a null reference. If <typeparamref name="T"/> is a value type 
        /// and the field value is <see cref="DBNull.Value"/> then 
        /// <see cref="InvalidCastException"/> is raised unless 
        /// <typeparamref name="T"/> is nullable (<see cref="Nullable{T}"/>).
        /// </remarks>

        public static T GetValue<T>(this IDataRecord record, int i)
        {
            if (record == null) throw new ArgumentNullException("record");
            return GetValueImpl<T>.Impl(record[i]);
        }

        static class GetValueImpl<T>
        {
            public static readonly Func<object, T> Impl;

            static GetValueImpl()
            {
                var type = typeof(T);
                Impl = !type.IsValueType
                     ? ReferenceImpl
                     : type.IsConstructionOfNullable()
                     ? MakeNullableImpl()
                     : ValueImpl;
            }

            static T ReferenceImpl(object value)
            {
                return !Convert.IsDBNull(value) ? (T) value : default(T);
            }

            static T ValueImpl(object value)
            {
                if (Convert.IsDBNull(value))
                { 
                    throw new InvalidCastException(string.Format(
                        @"Cannot cast DBNull to type '{0}'. Use '{1}' instead.", 
                        typeof(T), typeof(Nullable<>).MakeGenericType(typeof(T))));
                }
                return (T) value;
            }

            static Func<object, T> MakeNullableImpl()
            {
                Debug.Assert(typeof(T).IsConstructionOfNullable());
                var thisType = typeof(GetValueImpl<T>);
                var md = thisType.GetMethod("NullableImpl", BindingFlags.Static | BindingFlags.NonPublic);
                var method = md.MakeGenericMethod(typeof(T).GetGenericArguments()[0]);
                return (Func<object, T>) Delegate.CreateDelegate(typeof(Func<object, T>), method);
            }

            static TValue? NullableImpl<TValue>(object value) where TValue : struct
            {
                return !Convert.IsDBNull(value) 
                     ? new TValue?((TValue) value) 
                     : new TValue?();
            }
        }

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

            foreach (var field in record.GetFields())
            {
                var key = nameMapper(field.Key);
                target.Add(key, valueMapper(key, field.Value));
            }

            return target;
        }
#endif
    }
}

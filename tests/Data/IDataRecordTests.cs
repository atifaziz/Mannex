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

namespace Mannex.Tests.Data
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Mannex.Data;
    using Xunit;

    #endregion

    public class IDataRecordTests
    {
        [Fact]
        public void GetValueByIndexFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        IDataRecordExtensions.GetValue<object>(null, 0));
            Assert.Equal("record", e.ParamName);
        }

        [Fact]
        public void GetValueByNameFailsWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        IDataRecordExtensions.GetValue<object>(null, string.Empty));
            Assert.Equal("record", e.ParamName);
        }

        [Fact]
        public void GetValueByIndexForReferences()
        {
            var records = CreateTable("foo", null).GetRecords().ToArray();
            Assert.Equal("foo", records[0].GetValue<string>(0));
            Assert.Null(records[1].GetValue<string>(0));
        }

        [Fact]
        public void GetValueByIndexForStructs()
        {
            var record = CreateTable(42).GetRecords().Single();
            Assert.Equal(42, record.GetValue<int>(0));
        }

        [Fact]
        public void GetValueByIndexForStructsFailsWithDBNull()
        {
            var record = CreateTable(0).GetRecords().Single();
            Assert.Throws<InvalidCastException>(() => record.GetValue<int>(0));
        }

        [Fact]
        public void GetValueByIndexForNullableFailsWithDBNull()
        {
            var record = CreateTable(0).GetRecords().Single();
            Assert.Null(record.GetValue<int?>(0));
        }

        static DataTable CreateTable<T>(params T[] values)
        {
            var comparer = EqualityComparer<T>.Default;
            return CreateTable(v => comparer.Equals(v, default(T)), values);
        }

        static DataTable CreateTable<T>(Predicate<T> nullPredicate, params T[] values)
        {
            var table = new DataTable();
            table.Columns.Add("Value", typeof(T));

            foreach (var value in from value in values
                                  select nullPredicate(value)
                                       ? (object) DBNull.Value
                                       : value)
            {
                table.Rows.Add(value);
            }

            return table;
        }

        [Fact]
        public void GetNamesExecutesImmediatelyWithReader()
        {
            ImmediateExecutionSemanticsWithReader(false, r => r.GetNames());
        }

        [Fact]
        public void GetNamesExecutesLaterWithRecord()
        {
            DeferredExecutionSemanticsWithRecord(r => r.GetNames());
        }

        [Fact]
        public void GetFieldsExecutesImmediatelyWithReader()
        {
            ImmediateExecutionSemanticsWithReader(true, r => r.GetFields());
        }

        [Fact]
        public void GetFieldsExecutesLaterWithRecord()
        {
            DeferredExecutionSemanticsWithRecord(r => r.GetFields());
        }

        [Fact]
        public void GetValuesExecutesImmediatelyWithReader()
        {
            ImmediateExecutionSemanticsWithReader(true, r => r.GetValues());
        }

        [Fact]
        public void GetValuesExecutesLaterWithRecord()
        {
            DeferredExecutionSemanticsWithRecord(r => r.GetValues());
        }

        static void ImmediateExecutionSemanticsWithReader<T>(bool read, Func<IDataRecord, IEnumerable<T>> f)
        {
            var table = CreateTable(1, 2, 3);
            using (var reader = table.CreateDataReader())
            {
                if (read) Assert.True(reader.Read());
                Assert.True(f(reader) is T[]);
            }
        }

        static void DeferredExecutionSemanticsWithRecord<T>(Func<IDataRecord, IEnumerable<T>> f)
        {
            var table = CreateTable(1, 2, 3);
            using (var reader = table.CreateDataReader())
            using (var e = reader.Select(r => r))
            {
                Assert.True(e.MoveNext());
                Assert.False(f(e.Current) is T[]);
            }
        }
    }
}

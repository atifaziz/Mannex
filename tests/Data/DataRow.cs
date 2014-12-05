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
    using System.Web.UI.WebControls;
    using Mannex.Data;
    using Xunit;

    #endregion

    public class DataRowTests
    {
        static DataTable CreateSampleDataTable()
        {
            var table = new DataTable();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Str", typeof(string));
            return table;
        }

        [Fact]
        public void TrySetFieldWithNullThis()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                        DataRowExtensions.TrySetField(null, "Monty", "Python"));
            Assert.Equal("row", e.ParamName);
        }

        [Fact]
        public void TrySetFieldWithValue()
        {
            var table = CreateSampleDataTable();
            var row = table.NewRow();
            const int value = 42;
            Assert.True(row.TrySetField("Num", value));
            Assert.Equal(value, row["Num"]);
        }

        [Fact]
        public void TrySetFieldNull()
        {
            var table = CreateSampleDataTable();
            var row = table.NewRow();
            Assert.True(row.TrySetField("Str", "foobar"));
            Assert.True(row.TrySetField("Str", (string) null));
            Assert.Equal(DBNull.Value, row["Str"]);
        }

        [Fact]
        public void TrySetFieldNullable()
        {
            var table = CreateSampleDataTable();
            var row = table.NewRow();
            Assert.True(row.TrySetField("Num", (int?) 42));
            Assert.True(row.TrySetField("Num", (int?) null));
            Assert.Equal(DBNull.Value, row["Num"]);
        }

        [Fact]
        public void TrySetFieldForNonExistingColumn()
        {
            var table = CreateSampleDataTable();
            var row = table.NewRow();
            Assert.False(row.TrySetField("Foo", (object) null));
        }
    }
}